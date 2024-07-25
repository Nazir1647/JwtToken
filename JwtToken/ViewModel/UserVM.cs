namespace JwtToken.ViewModel
{
    public class UserVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool? Isvalid { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyUser { get; set; }
    }
}
