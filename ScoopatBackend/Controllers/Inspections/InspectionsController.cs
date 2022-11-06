using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoopatBackend.Models;
using ScoopatBackend.Models.Farmers;
using ScoopatBackend.Models.RequestModels;

namespace ScoopatBackend.Controllers.Inspections;

[Route("api/[controller]")]
[ApiController]
public class InspectionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public InspectionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("GetAllInspections")]
    public async Task<IActionResult> GetAllInspections()
    {
        var inspections = await _context.Inspections
            .Include(i => i.Employee)
            .Include(i => i.Farm)
            .Include(i => i.InspectionQuestionResult)
            .ThenInclude(r => r.Result)
            .ToListAsync();
        return Ok(inspections);
    }

    [HttpGet]
    [Route("GetSingleInspection")]
    public async Task<IActionResult> GetSingleInspection(int inspectionId)
    {
        var inspection = await _context.Inspections.Where(i => i.InspectionId == inspectionId).Include(i => i.Assesment)
            .Include(i => i.Employee)
            .Include(i => i.Farm)
            .Include(i => i.Farmer)
            .Include(i => i.InspectionQuestionResult)
            .ThenInclude(r => r.Result)
            .ThenInclude(q => q.Question)
            .ThenInclude(c => c.Category)
            .FirstOrDefaultAsync();
        if (inspection == null) return NotFound("Inspection Not Found !");
        return Ok(inspection);
    }

    [HttpPost]
    [Route("MakeInspection")]
    public async Task<IActionResult> MakeInspection(int employeeId, int farmId, int farmerId)
    {
        var employee = await _context.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();
        var farm = await _context.Farms.Where(f => f.FarmId == farmId).FirstOrDefaultAsync();
        var farmer = await _context.Farmers.Where(f => f.FarmerId == farmerId).FirstOrDefaultAsync();
        if (farm == null) return NotFound("Farm Not Found !");
        if (farmer == null) return NotFound("Farmer Not Found !");
        if (employee == null) return NotFound("Employee Not Found !");
        

        var inspection = new Inspection()
        {
            Farm = farm,
            Farmer = farmer,
            Employee = employee,
            DateOfInspection = DateTime.Now.Date,
            
        };

        await _context.Inspections.AddAsync(inspection);
        await _context.SaveChangesAsync();

        return Ok(inspection);




    }

    [HttpPost]
    [Route("AddAssesment")]
    public async Task<IActionResult> AddAssesment(int inspectionId , [FromBody] AddAssesmentRequest model)
    {
        var inspection = await _context.Inspections.Where(i => i.InspectionId == inspectionId).FirstOrDefaultAsync();
        if (inspection == null) return NotFound("Inspection Not Found !");
        var nonConform = await _context.InspectionQuestionsResults
            .Where(r => r.Inspection == inspection && r.Result.ResultType == "N/C").ToListAsync();
        var conform = await _context.InspectionQuestionsResults
            .Where(r => r.Inspection == inspection && r.Result.ResultType == "C").ToListAsync();
        var nonApplicable = await _context.InspectionQuestionsResults
            .Where(r => r.Inspection == inspection && r.Result.ResultType == "N/A").ToListAsync();
        int nonConformCount = nonConform.Count;
        int conformCount = conform.Count;
        int nonAppCount = nonApplicable.Count;

        var assesment = new Assesment()
        {
            Inspection = inspection,
            NonConformity = nonConformCount,
            Conformity = conformCount,
            NonApplicable = nonAppCount,
            TotalApplicable = conformCount,
            Status = model.Status,
            Notes = model.Notes
            
        };
        await _context.Assesments.AddAsync(assesment);
        await _context.SaveChangesAsync();
        return Ok(assesment);

    }

    [HttpPost]
    [Route("AddQuestionResult")]
    public async Task<IActionResult> AddQuestion(int resultId, int inspectionId)
    {
        var result = await _context.Results.Where(r => r.ResultId == resultId).FirstOrDefaultAsync();
        var inspection = await _context.Inspections.Where(i => i.InspectionId == inspectionId).FirstOrDefaultAsync();
        if (result == null) return NotFound("Result Not Found !");
        if (inspection == null) return NotFound("Inspection Not Found !");
        var addQuestion = new InspectionQuestionResult()
        {
            Result = result,
            Inspection = inspection
        };
        await _context.InspectionQuestionsResults.AddAsync(addQuestion);
        await _context.SaveChangesAsync();
        return Ok(addQuestion);
    }
    // [HttpPost]
    // [Route("AddQuestion")]
    // public async Task<IActionResult> AddQuestion([FromBody] QuestionRequestModel model)
    // {
    //     var question = new Question()
    //     {
    //         QuestionTitle = model.QuestionTitle,
    //         QuestionContent = model.QuestionContent,
    //         AnswerType = model.AnswerType,
    //         Observation = model.Observation
    //     };
    //     await _context.Questions.AddAsync(question);
    //     await _context.SaveChangesAsync();
    //     return Ok(question);
    // }
}