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
            isFarmOwner = model.isFarmOwner,
        };
        if (farmer.Region == "SEGUIE")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="SEGUIE").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "U";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "KOUADJAKRO")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="KOUADJAKRO").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "F";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "OFFOUMPO")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="OFFOUMPO").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "G";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "BOKAO")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="BOKAO").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "H";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "SICOGI")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="SICOGI").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "I";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "OFFA")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="OFFA").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "K";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "ABOUDE MANDEKE")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="ABOUDE MANDEKE").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "L";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "KOTCHIMPO")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="KOTCHIMPO").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "P";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
            
        }else if (farmer.Region == "ABOUDE NOUVEAU QUARTIER" || farmer.Region == "ABOUDE KOUASSIKRO")
        {
            string lastCode = (await _context.Farmers.OrderByDescending(f => f.FarmerId).Where(f => f.Region=="ABOUDE NOUVEAU QUARTIER" || f.Region=="ABOUDE KOUASSIKRO").Select(f => f.FarmerCode).FirstOrDefaultAsync())!;
            int newCode = Int32.Parse(lastCode.Substring(10));
            newCode++;
            Console.WriteLine(newCode);
            string identifier = "S";
            farmer.FarmerCode = "901585000"+identifier+"0"+newCode;
        }
        else
        {
            farmer.FarmerCode = "Nan";
        }
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
                FirstName = farmer.FirstName!,
                LastName = farmer.LastName!,
                Sex = farmer.Sex!,
                IdType = farmer.IdType!,
                IdNumber = farmer.IdNumber!,
                Contact = farmer.Contact!
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