using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Helpers;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Interfaces;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message> GetMessage(int id);
    Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
    Task<bool> SaveAllAsync();
}