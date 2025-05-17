using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int JoiningDate { get; set; }
        public bool IsActive { get; set; }
    }
}
