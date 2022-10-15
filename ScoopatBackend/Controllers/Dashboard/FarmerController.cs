using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoopatBackend.Models;
using ScoopatBackend.Models.Farmers;
using ScoopatBackend.Models.RequestModels;

namespace ScoopatBackend.Controllers.Dashboard;

[Route("api/[controller]")]
[ApiController]
public class FarmerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FarmerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("GetAllFarmers")]
    public async Task<IActionResult> GetAllFarmers()
    {
        var farmers = await _context.Farmers.ToListAsync();
        return Ok(farmers);
    }

    [HttpGet]
    [Route("GetFarmersByRegion")]
    public async Task<IActionResult> GetFarmersByRegion(string region)
    {
        var farmers = await _context.Farmers.Where(f => f.Region == region).ToListAsync();
        return Ok(farmers);
    }
    
    [HttpPost]
    [Route("AddFarmer")]
    public async Task<IActionResult> AddFarmer([FromBody] AddFarmerRequest model)
    {
        if (!ModelState.IsValid) return BadRequest();
        var farmer = new Farmer()
        {
            FarmerCode = model.FarmerCode,
            FirstName = model.FirstName,
            LastName = model.LastName,
            IdType = model.IdType,
            IdNumber = model.IdNumber,
            BirthPlace = model.BirthPlace,
            BirthDate = model.BirthDate,
            Sex = model.Sex,
            Contact = model.Contact,
            Location = model.Location,
            Section = model.Section,
            Region = model.Region,
            Department = model.Department,
            isFarmOwner = model.isFarmOwner

        };
        await _context.Farmers.AddAsync(farmer);
        await _context.SaveChangesAsync();

        return Ok(farmer);
        

    }

    [HttpPost]
    [Route("AddFarmerToFarm")]
    public async Task<IActionResult> AddFarmerToFarm(int farmId, int farmerId)
    {
        var farmer = await _context.Farmers.Where(f => f.FarmerId == farmerId).FirstOrDefaultAsync();
        var farm = await _context.Farms.Where(f => f.FarmId == farmId).FirstOrDefaultAsync();
        if (farmer == null) return NotFound("Farmer Not Found !");
        if (farm == null) return NotFound("Farm Not Found !");

        var farmerFarm = new FarmersFarm()
        {
            Farmer = farmer,
            Farm = farm
        };

        await _context.FarmersFarms.AddAsync(farmerFarm);
        await _context.SaveChangesAsync();

        if (farmer.isFarmOwner == true)
        {
            var owner = new FarmOwner()
            {
                FirstName = farmer.FirstName,
                LastName = farmer.LastName,
                Sex = farmer.Sex,
                IdType = farmer.IdType,
                IdNumber = farmer.IdNumber,
                Contact = farmer.Contact
            };
            await _context.FarmOwners.AddAsync(owner);
            await _context.SaveChangesAsync();

            var ownerFarms = new OwnersFarms()
            {
                Farm = farm,
                FarmOwner = owner
            };
            await _context.OwnersFarms.AddAsync(ownerFarms);
            await _context.SaveChangesAsync();
        }

        return Ok(farmerFarm);
    }
    
    


}