namespace FlowerShowerService.Security;

public sealed class RelaxedPasswordHelper : IPasswordHelper
{
    public bool VerifyPassword(string password)
    {
        return password.Any(char.IsDigit);
    }
}
