using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public static class WebApplicationExtensions
{
    public static void MapHouseEndpoints(this WebApplication app)
    {
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

        app.MapPost("/houses", async (IHouseRepository houseRepository, [FromBody] HouseDetailsDto houseDto,
        IValidator<HouseDetailsDto> houseValidator) =>
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
    }

    public static void MapBidEndpoints(this WebApplication app)
    {
        app.MapGet("/houses/{houseId}/bids", async (IHouseRepository houseRepository, IBidRepository bidRepository,
        int houseId) =>
        {
            var house = await houseRepository.GetById(houseId);
            if (house == null)
            {
                return Results.NotFound();
            }

            var bids = await bidRepository.GetAsync(houseId);
            return Results.Ok(bids);
        }).Produces(statusCode: StatusCodes.Status404NotFound)
        .Produces<IEnumerable<BidDto>>(statusCode: StatusCodes.Status200OK);

        app.MapPost("/houses/{houseId}/bids", async (IHouseRepository houseRepository, IBidRepository bidRepository,
        int houseId, IValidator<BidDto> validator, [FromBody] BidDto bidDto) =>
        {
            var house = await houseRepository.GetById(houseId);
            if (house == null)
            {
                return Results.NotFound();
            }

            if (bidDto.HouseId != houseId)
            {
                return Results.BadRequest();
            }

            var validationResult = await validator.ValidateAsync(bidDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var newBid = await bidRepository.AddAsync(bidDto);
            return Results.Created($"/houses/{houseId}/bids/{newBid.Id}", newBid);
        }).Produces(statusCode: StatusCodes.Status400BadRequest)
        .Produces(statusCode: StatusCodes.Status404NotFound)
        .Produces<BidDto>(statusCode: StatusCodes.Status201Created)
        .Produces<IEnumerable<BidDto>>(statusCode: StatusCodes.Status200OK);
    }
}