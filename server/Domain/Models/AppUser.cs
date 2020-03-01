namespace Domain.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public GenderType Gender { get; set; }
    }
}