namespace AspNetWebApplication.Models
{
    public class RegisterModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        public string Role { get; set; }
        public bool IsSalesRep { get; set; }
        public bool IsPreprer { get; set; }
    }
}