using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HouseDbContext>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddCors();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.MapGet("/houses", (IHouseRepository houseRepository) => houseRepository.GetAll())
.Produces<IEnumerable<HouseDto>>(statusCode: StatusCodes.Status200OK);

app.MapGet("/house/{id}", async (IHouseRepository houseRepository, int id) =>
{
    var house = await houseRepository.GetById(id);
    if (house == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(house);
}).ProducesProblem(statusCode: StatusCodes.Status404NotFound)
.Produces<HouseDetailsDto>(statusCode: StatusCodes.Status200OK);

app.MapPost("/houses", async (IHouseRepository houseRepository, [FromBody] HouseDetailsDto houseDto, IValidator<HouseDetailsDto> houseValidator) =>
{
    var validationResult = await houseValidator.ValidateAsync(houseDto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var newHouse = await houseRepository.Add(houseDto);
    return Results.Created($"/house/{newHouse.Id}", newHouse);
}).Produces<HouseDetailsDto>(statusCode: StatusCodes.Status201Created)
.ProducesValidationProblem(statusCode: StatusCodes.Status400BadRequest);

app.MapPut("/houses", async (IHouseRepository houseRepository, [FromBody] HouseDetailsDto houseDto) =>
{
    var house = await houseRepository.GetById(houseDto.Id);
    if (house == null)
    {
        return Results.NotFound();
    }
    var updatedHouse = await houseRepository.Update(houseDto);
    return Results.Ok(updatedHouse);
}).ProducesProblem(statusCode: StatusCodes.Status404NotFound)
.Produces<HouseDetailsDto>(statusCode: StatusCodes.Status200OK);

app.MapDelete("/houses/{id}", async (IHouseRepository houseRepository, int id) =>
{
    var house = await houseRepository.GetById(id);
    if (house == null)
    {
        return Results.NotFound();
    }
    await houseRepository.Delete(id);
    return Results.Ok();
}).ProducesProblem(statusCode: StatusCodes.Status404NotFound)
.Produces(statusCode: StatusCodes.Status200OK);

app.Run();