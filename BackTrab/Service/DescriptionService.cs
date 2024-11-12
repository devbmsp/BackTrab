using static TrabalhoBackEnd.Models.Descriptions;
using TrabalhoBackEnd.IDAL;

namespace TrabalhoBackEnd.BLL
{
    public class DescriptionService
    {
        private readonly IDescriptionRepository _descriptionRepository;

        public DescriptionService(IDescriptionRepository descriptionRepository)
        {
            _descriptionRepository = descriptionRepository;
        }
        public Role GetRoleById(int id)
        {
            var role = _descriptionRepository.GetById(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id {id} not found.");
            }
            return role;
        }
        public void AddRole(Role role)
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                throw new ArgumentException("Role não pode ser nulo.");
            }

            _descriptionRepository.Insert(role);
        }

        public List<Role> GetAllRoles()
        {
            return _descriptionRepository.GetAll();
        }

       
    }
}
