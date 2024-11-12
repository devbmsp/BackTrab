using static TrabalhoBackEnd.Models.Users;

namespace TrabalhoBackEnd.IDAL
{
    public interface IUserRepository
    {
        User FindByEmail(string email);
        User Save(User user);
        User FindById(long id);
        List<User> FindAll();
        List<User> FindByRole(string role);
        void Delete(User user);
    }
}
