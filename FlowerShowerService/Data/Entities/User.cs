namespace FlowerShowerService.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // Px bus gerai
    public List<Order> Orders { get; set; } = new();
}
