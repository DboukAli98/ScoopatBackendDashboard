using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoopatBackend.Models;
using ScoopatBackend.Models.Farmers;
using ScoopatBackend.Models.RequestModels;

namespace ScoopatBackend.Controllers.Dashboard;

[Route("api/[controller]")]
[ApiController]
public class FarmController : Controller
{
    private readonly ApplicationDbContext _context;

    public FarmController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("GetAllFarms")]
    public async Task<IActionResult> GetAllFarms()
    {
        var farms = await _context.Farms.ToListAsync();
        return Ok(farms);
    }

    [HttpGet]
    [Route("GetFarmWorkers")]
    public async Task<IActionResult> GetFarmWorkers(int farmId)
    {
        var farm = await _context.Farms.Where(f => f.FarmId == farmId).FirstOrDefaultAsync();
        if (farm == null) return NotFound("Farm Not Found !");
        var workers = await _context.Workers.Where(f => f.Farm == farm).ToListAsync();
        return Ok(workers);
    }

    [HttpGet]
    [Route("GetFarmCultivation")]
    public async Task<IActionResult> GetFarmCultivation(int farmId)
    {
        var farm = await _context.Farms.Where(f => f.FarmId == farmId).FirstOrDefaultAsync();
        if (farm == null) return NotFound("Farm Not Found !");
        var cultivations = await _context.CultivationInformations.Where(c => c.Farm == farm).ToListAsync();
        return Ok(cultivations);
    }

    [HttpPost]
    [Route("AddFarm")]
    public async Task<IActionResult> AddFarm([FromBody] AddFarmRequest model)
    {
        var farm = new Farm()
        {
            FarmCode = model.FarmCode,
            TotalArea = model.TotalArea,
            Lattitude = model.Lattitude,
            Longtitude = model.Longtitude
            
        };
        await _context.Farms.AddAsync(farm);
        await _context.SaveChangesAsync();

        return Ok(farm);

    }

    [HttpPost]
    [Route("AddFarmOwner")]
    public async Task<IActionResult> AddFarmOwner(int farmId, int ownerId)
    {
        var owner = await _context.FarmOwners.Where(f => f.OwnerId == ownerId).FirstOrDefaultAsync();
        var farm = await _context.Farms.Where(f => f.FarmId == farmId).FirstOrDefaultAsync();
        if (owner == null) return NotFound("Owner Not Found !");
        if (farm == null) return NotFound("Farm Not Found !");
        var farmowner = new OwnersFarms()
        {
            Farm = farm,
            FarmOwner = owner
        };
        await _context.OwnersFarms.AddAsync(farmowner);
        await _context.SaveChangesAsync();
        return Ok(farmowner);
    }

    [HttpPost]
    [Route("AddOwner")]
    public async Task<IActionResult> AddOwner([FromBody] AddOwnerRequest model)
    {
        var owner = new FarmOwner()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            IdType = model.IdType,
            IdNumber = model.IdNumber,
            Sex = model.Sex,
            Contact = model.Contact,

        };
        await _context.FarmOwners.AddAsync(owner);
        await _context.SaveChangesAsync();

        return Ok(owner);

    }




}