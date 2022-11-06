using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScoopatBackend.Models;
using ScoopatBackend.Models.Authentication;
using ScoopatBackend.Models.Users;

namespace ScoopatBackend.Controllers.Authentication;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    
    public AuthenticationController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _config = config;

    }
    
    
    [HttpPost]
    [Route("RegisterEmployee")]
    public async Task<IActionResult> Register([FromBody] RegisterEmployeeModel model)
    {
        
        var username = model.Firstname + "@" + model.Lastname + ".scoopat";
        var userToCreate = new ApplicationUser()
        {
            Email = model.Email,
            UserName = username,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.Firstname,
            LastName = model.Lastname,
            
            
        };

        var Password = "Test@123";
        
        //Create User
        var result = await _userManager.CreateAsync(userToCreate, Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(userToCreate, UserRoles.User);
            }
            var userFromDb = await _userManager.FindByNameAsync(userToCreate.UserName);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
            var userRoles = await _userManager.GetRolesAsync(userFromDb);


            var employee = new Employee()
            {
                User = userFromDb,
                CreatedAt = DateTime.UtcNow.Date,
                Contact = model.Contact,
                IdType = model.IdType,
                IdNumber = model.IdNumber,
                role = userRoles.FirstOrDefault()!,
                Firstname = model.Firstname,
                Lastname = model.Lastname
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();


            return Ok(
                new RegisterResponseModel()
                {
                    Token = token,
                    id = userFromDb.Id,
                    username = userFromDb.UserName,
                    role = userRoles.FirstOrDefault()!

                });
            
        }

        return BadRequest(result);
    }
    
    [HttpPost]
    [Route("RegisterAdmin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterEmployeeModel model)
    {
        var username = model.Firstname + "@" + model.Lastname + ".adminScoo";
        var userToCreate = new ApplicationUser()
        {
            Email = model.Email,
            UserName = username,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.Firstname,
            LastName = model.Lastname,
            
        };
        
        var Password = model.Firstname + "sco@admin" + model.Lastname;
        //Create User
        var result = await _userManager.CreateAsync(userToCreate, Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(userToCreate, UserRoles.Admin);
            }
            var userFromDb = await _userManager.FindByNameAsync(userToCreate.UserName);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
            var userRoles = await _userManager.GetRolesAsync(userFromDb);

            var employee = new Employee()
            {
                User = userFromDb,
                CreatedAt = DateTime.UtcNow.Date,
                Contact = model.Contact,
                IdNumber = model.IdNumber,
                IdType = model.IdType,
                role = userRoles.FirstOrDefault()!,
                Firstname = model.Firstname,
                Lastname = model.Lastname
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            

            return Ok(
                new RegisterResponseModel()
                {
                    Token = token,
                    id = userFromDb.Id,
                    username = userFromDb.UserName,
                    role = userRoles.FirstOrDefault(),
                    

                });
            
        }

        return BadRequest(result);
    }
    
    
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(

                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );


            var ctoken = new JwtSecurityTokenHandler().WriteToken(token);

            var role = userRoles.FirstOrDefault();


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                User = user.UserName,
                role,
                user.Id,
            });

        }

        return Unauthorized();

    }

    [HttpGet]
    [Route("GetAllInspectors")]
    public async Task<IActionResult> GetAllInspectors()
    {
        var inspectors = await _context.Employees.Where(e => e.role == "Inspector").ToListAsync();
        return Ok(inspectors);
    }











}