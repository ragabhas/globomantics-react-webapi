using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
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
}