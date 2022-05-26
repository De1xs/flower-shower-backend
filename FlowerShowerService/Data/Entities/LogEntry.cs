namespace FlowerShowerService.Data.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; } = "User";

        public string Endpoint { get; set; } = string.Empty;

        public string? Message { get; set; }
        
        public DateTime Logged { get; set; }
    }
}
