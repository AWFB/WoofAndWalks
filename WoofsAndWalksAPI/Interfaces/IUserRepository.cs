using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUserameAsync(string username);

    Task<IEnumerable<MemberDto>> GetAllMembersAsync();
    Task<MemberDto> GetMemberAsync(string username);
}
