using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoopatBackend.Models;
using ScoopatBackend.Models.Farmers;
using ScoopatBackend.Models.RequestModels;

namespace ScoopatBackend.Controllers.Surveys;

[Route("api/[controller]")]
[ApiController]
public class SurveyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SurveyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("AddCategory")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequestModel model)
    {
        var category = new Category()
        {
            CategoryTitle = model.CategoryTitle,
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return Ok(category);
    }

    [HttpPost]
    [Route("AddQuestion")]
    public async Task<IActionResult> AddQuestion(int categoryId , [FromBody] QuestionRequestModel model)
    {
        var category = await _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync();
        if (category == null) return NotFound("Category Not Found !");
        var question = new Question()
        {
            QuestionContent = model.QuestionContent,
            Category = category
        };
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return Ok(question);
    }

    [HttpGet]
    [Route("GetAllSurvey")]
    public async Task<IActionResult> GetAllSurveys()
    {
        var surveys = await _context.Categories.Include(c => c.Questions).ToListAsync();
        return Ok(surveys);
    }

    [HttpGet]
    [Route("GetCategoryQuestion")]
    public async Task<IActionResult> GetCategoryQuestion(int categoryId)
    {
        var category = await _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync();
        var questions = await _context.Questions.Where(q => q.Category == category).ToListAsync();
        return Ok(questions);
    }

    [HttpPost]
    [Route("SetAssesment")]
    public async Task<IActionResult> SetAssesment(int questionId , [FromBody] ResultRequestModel model )
    {
        var question = await _context.Questions.Where(q => q.QuestionId == questionId).FirstOrDefaultAsync();
        if (question == null) return NotFound("Question Not Found !");
        var result = new Result()
        {
            ResultType = model.ResultType,
            Observations = model.Observations,
            Question = question
        };

        await _context.Results.AddAsync(result);
        await _context.SaveChangesAsync();

        return Ok(result);
    }

    
}