using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoopatBackend.Models;
using ScoopatBackend.Models.Farmers;
using ScoopatBackend.Models.RequestModels;

namespace ScoopatBackend.Controllers.Dashboard;

[Route("api/[controller]")]
[ApiController]
public class CultivationController : Controller
{
    
    private readonly ApplicationDbContext _context;

    public CultivationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("GetAllCultivation")]
    public async Task<IActionResult> GetAllCultivation()
    {
        var cultivations = await _context.CultivationInformations.Include(c => c.Farm).ToListAsync();
        return Ok(cultivations);
    }

    [HttpPost]
    [Route("AddCultivation")]
    public async Task<IActionResult> AddCultivation(int farmId , [FromBody] AddCultivationRequest model)
    {
        var farm = await _context.Farms.Where(f => f.FarmId == farmId).FirstOrDefaultAsync();
        if (farm == null) return NotFound("Farm Not Found !");
        var cultivation = new CultivationInformation()
        {
            ProductType = model.ProductType,
            ProductArea = model.ProductArea,
            Season = model.Season,
            TotalHarvestEstimate = model.TotalHarvestEstimate,
            TotalHarvest = model.TotalHarvest,
            DeliveredVolume = model.DeliveredVolume,
            Farm = farm

        };
        await _context.CultivationInformations.AddAsync(cultivation);
        await _context.SaveChangesAsync();

        return Ok(cultivation);
    }

}