using static TrabalhoBackEnd.Models.Users;

namespace TrabalhoBackEnd.Models.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }
    }
}
