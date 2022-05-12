namespace FlowerShowerService.Security;

public sealed class StrictPasswordHelper : IPasswordHelper
{
    public bool VerifyPassword(string password)
    {
        return password.Any(char.IsDigit) && password.Any(char.IsUpper);
    }
}
