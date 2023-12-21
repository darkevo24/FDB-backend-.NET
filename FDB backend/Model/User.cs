namespace FDB_backend.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        // Add other user properties as needed
    }

}
