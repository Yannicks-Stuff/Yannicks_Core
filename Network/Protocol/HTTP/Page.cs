namespace Yannick.Network.Protocol.HTTP;

public abstract class Page
{
    public abstract PageResult OnGet(Server.User usr);
}