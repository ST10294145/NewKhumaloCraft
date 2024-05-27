namespace NewKhumaloCraft.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Logged { get; set; }
    }
}
