using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Interfaces;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllMembersAsync();

        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetSingleUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

}
