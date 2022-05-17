namespace FlowerShowerService.Security;

public interface IPasswordHelper
{
    bool VerifyPassword(string password);
}
