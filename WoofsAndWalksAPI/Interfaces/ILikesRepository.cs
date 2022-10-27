using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Helpers;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    
}