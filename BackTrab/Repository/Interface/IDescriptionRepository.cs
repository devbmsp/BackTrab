using static TrabalhoBackEnd.Models.Descriptions;

namespace TrabalhoBackEnd.IDAL
{
    public interface IDescriptionRepository
    {
        Role Insert(Role role);
        List<Role> GetAll();
        Role GetById(long id);
        Role FindByName(string roleName);
        List<Role> FindAll();
    }

}
