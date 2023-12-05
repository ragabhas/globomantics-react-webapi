using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
    Task<HouseDetailsDto?> GetById(int id);
}

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext _context;
    public HouseRepository(HouseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HouseDto>> GetAll()
    {
        return await _context.Houses.Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price)).ToListAsync();
    }

    public async Task<HouseDetailsDto?> GetById(int id)
    {
        var house = await _context.Houses.FindAsync(id);
        if (house == null)
        {
            return null;
        }
        return new HouseDetailsDto(house.Id, house.Address, house.Country, house.Price, house.Description, house.Photo);
    }
}