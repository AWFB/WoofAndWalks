﻿using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Helpers;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUsernameAsync(string username);
    Task<string> GetUserGender(string username);

    Task<PagedList<MemberDto>> GetAllMembersAsync(UserParams userParams);

    Task<MemberDto> GetMemberAsync(string username);
}
