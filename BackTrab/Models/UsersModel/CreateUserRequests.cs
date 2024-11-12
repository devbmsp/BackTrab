using static TrabalhoBackEnd.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace TrabalhoBackEnd.Models.Requests
{
    public class CreateUserRequests
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public User ToUser()
        {
            return new User
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
        }
    }
}
