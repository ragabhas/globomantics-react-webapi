using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
    Task<HouseDetailsDto?> GetById(int id);
    Task<HouseDetailsDto> Add(HouseDetailsDto houseDto);
    Task<HouseDetailsDto> Update(HouseDetailsDto houseDto);
    Task Delete(int id);
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
        return EntityToDto(house);
    }

    public async Task<HouseDetailsDto> Add(HouseDetailsDto houseDto)
    {
        var house = new House();
        DtoToEntity(houseDto, house);
        await _context.Houses.AddAsync(house);
        await _context.SaveChangesAsync();

        return EntityToDto(house);
    }

    public async Task<HouseDetailsDto> Update(HouseDetailsDto houseDto)
    {
        var house = await _context.Houses.FindAsync(houseDto.Id) ?? throw new InvalidOperationException("House not found");
        DtoToEntity(houseDto, house);
        _context.Entry(house).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return EntityToDto(house);
    }

    public Task Delete(int id)
    {
        var house = _context.Houses.Find(id) ?? throw new InvalidOperationException("House not found");
        _context.Houses.Remove(house);
        return _context.SaveChangesAsync();
    }

    private static void DtoToEntity(HouseDetailsDto houseDto, House house)
    {
        house.Address = houseDto.Address;
        house.Country = houseDto.Country;
        house.Price = houseDto.Price;
        house.Description = houseDto.Description;
        house.Photo = houseDto.Photo;
    }

    private static HouseDetailsDto EntityToDto(House house)
    {
        return new HouseDetailsDto(house.Id, house.Address, house.Country, house.Price, house.Description, house.Photo);
    }
}