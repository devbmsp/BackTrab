using static TrabalhoBackEnd.Models.Users;

namespace TrabalhoBackEnd.ISecurity
{
    public interface IJwtService
    {
        string CreateToken(User user);
    }

}
