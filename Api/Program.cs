using Microsoft.AspNetCore.Http.HttpResults;
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

app.Run();