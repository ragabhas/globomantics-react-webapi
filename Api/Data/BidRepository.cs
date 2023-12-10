using Microsoft.EntityFrameworkCore;

public interface IBidRepository
{
    Task<List<BidDto>> GetAsync(int houseId);
    Task<BidDto> AddAsync(BidDto bidDto);
}

public class BidRepository : IBidRepository
{
    private readonly HouseDbContext _context;
    public BidRepository(HouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<BidDto>> GetAsync(int houseId)
    {
        return await _context.Bids.AsNoTracking()
            .Where(b => b.HouseId == houseId)
            .Select(b => new BidDto(b.Id, b.HouseId, b.Bidder, b.Amount))
            .ToListAsync();
    }

    public async Task<BidDto> AddAsync(BidDto bidDto)
    {
        var bid = new Bid
        {
            HouseId = bidDto.HouseId,
            Bidder = bidDto.Bidder,
            Amount = bidDto.Amount
        };
        _context.Bids.Add(bid);
        await _context.SaveChangesAsync();
        return new BidDto(bid.Id, bid.HouseId, bid.Bidder, bid.Amount);
    }
}
