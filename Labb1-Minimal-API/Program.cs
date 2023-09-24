using AutoMapper;
using FluentValidation;
using Labb1_Minimal_API;
using Labb1_Minimal_API.Data;
using Labb1_Minimal_API.Models;
using Labb1_Minimal_API.Models.DTOs;
using Labb1_Minimal_API.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// This is dependency injection for the AppDbContext class + telling the application about coonection string
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Decided to produce a normal book object here so we can see the id
app.MapGet("/api/book", async ([FromServices] IBookRepository _bookRepo) => 
{
    APIResponse response = new APIResponse();
    var books = await _bookRepo.GetAllBooks();
    response.IsSuccess = true;
    response.Result = books;

    return Results.Ok(response);

}).WithName("GetAllBooks").Produces(200);

app.MapGet("/api/book/{id:int}", async ([FromServices] IBookRepository _bookRepo,
    [FromServices] IMapper _mapper,
    int id) =>
{
    APIResponse response = new APIResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
    var SingleBook = await _bookRepo.GetById(id);
    if (SingleBook != null)
    {
        response.Result = _mapper.Map<BookDTO>(SingleBook);
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;

        return Results.Ok(response);
    }
    return Results.NotFound();
}).WithName("GetBookById").Produces<APIResponse>(200).Produces(404);

app.MapGet("/api/book/{content}", async ([FromServices] IBookRepository _bookRepo,
    [FromServices] IMapper _mapper,
    string content) =>
{
    APIResponse response = new APIResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };
    var find = await _bookRepo.SearchBook(content);

    if(find.Any())
    {
        response.Result = _mapper.Map<IEnumerable<BookDTO>>(find); 
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;

        return Results.Ok(response);
    }

    return Results.NotFound(response);
}).WithName("SearchInLibrary").Produces<APIResponse>(200).Produces(404);

app.MapPost("/api/book", async ([FromServices] IBookRepository _bookRepo,
    [FromServices] IMapper _mapper,
    [FromServices] IValidator<BookCreateDTO> _validator,
    [FromBody] BookCreateDTO Book_C_DTO) => 
{
    APIResponse response = new APIResponse { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    var validationResult = await _validator.ValidateAsync(Book_C_DTO);

    if (!validationResult.IsValid)
    {
        foreach (var failure in validationResult.Errors)
        {
            string error = $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}";
            response.ErrorMessages.Add(error);
        }
        response.StatusCode = System.Net.HttpStatusCode.BadRequest;

        return Results.BadRequest(response);
    }

    var book = _mapper.Map<Book>(Book_C_DTO);
    var NewBook = await _bookRepo.NewBook(book);

    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;
    response.Result = NewBook;
    return Results.Created("NewBook", response);

}).WithName("NewBook").Accepts<BookCreateDTO>("application/json").Produces<BookDTO>(201).Produces(404);

app.MapPut("/api/book", async ([FromServices] IBookRepository _bookRepo,
    [FromServices] IMapper _mapper,
    [FromServices] IValidator<BookUpdateDTO> _validator,
    [FromBody] BookUpdateDTO Book_U_DTO) =>
{
    APIResponse response = new APIResponse { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    var validationResult = await _validator.ValidateAsync(Book_U_DTO);

    if (!validationResult.IsValid)
    {
        foreach (var failure in validationResult.Errors)
        {
            string error = $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}";
            response.ErrorMessages.Add(error);
        }

        return Results.BadRequest(response);
    }

    var book = _mapper.Map<Book>(Book_U_DTO);
    var result = await _bookRepo.UpdateBook(book);

    var updatedBook = _mapper.Map<BookDTO>(result);

    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;
    response.Result = updatedBook;

    return Results.Ok(response);

}).WithName("UpdateBook").Produces<APIResponse>(200).Produces(400);

app.MapDelete("/api/book/{id:int}", async ([FromServices] IBookRepository _bookRepo, int id) =>
{
    var BookToDelete = await _bookRepo.DeleteBook(id);
    if(BookToDelete != null)
    {
        return Results.Ok();
    }
    return Results.BadRequest();

}).WithName("DeleteBookById").Produces(200).Produces(400);

app.MapGet("/api/book/genre", async ([FromServices] IMapper _mapper,
    [FromServices] IBookRepository _bookRepo,
    string genre) =>
{
    var book = await _bookRepo.SearchByGenre(genre.ToLower());
    if(book.Any())
    {
        return Results.Ok(_mapper.Map<List<BookDTO>>(book));
    }
    return Results.NotFound($"Book with name {genre} not found");
}).WithName("SearchBooksByGenre").Produces(200).Produces(404);

app.MapGet("/api/book/author", async ([FromServices] IMapper _mapper,
    [FromServices] IBookRepository _bookRepo,
    string author) =>
{
    var book = await _bookRepo.SearchByAuthor(author.ToLower());
    if (book.Any())
    {
        return Results.Ok(_mapper.Map<List<BookDTO>>(book));
    }
    return Results.NotFound($"Author with name {author} not found");
}).WithName("SearchBooksByAuthor").Produces(200).Produces(404);



app.Run();

