using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetSingleUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }

}
