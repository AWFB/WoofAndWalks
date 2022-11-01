namespace WoofsAndWalksAPI.Helpers;

public class MessageParams : PaginationParams
{
    public string? Username { get; set; } // logged in user
    public string Container { get; set; } = "Unread";

}