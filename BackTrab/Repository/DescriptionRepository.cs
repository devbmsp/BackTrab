using TrabalhoBackEnd.IDAL;
using static TrabalhoBackEnd.Models.Descriptions;

namespace TrabalhoBackEnd.DAL
{
    public class DescriptionRepository : IDescriptionRepository
    {
        private readonly ProjectDbContext _context;

        public DescriptionRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public List<Role> GetAll()
        {
            return _context.Description.ToList();
        }

        public Role GetById(long id)
        {
            return _context.Description.FirstOrDefault(r => r.Id == id);
        }

        public Role Insert(Role role)
        {
            _context.Description.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role Save(Role role)
        {
            if (role.Id == 0)
            {
                _context.Description.Add(role);
            }
            else
            {
                _context.Description.Update(role);
            }
            _context.SaveChanges();
            return role;
        }

        public Role FindByName(string roleName)
        {
            return _context.Description.FirstOrDefault(r => r.Name == roleName);
        }

        public List<Role> FindAll()
        {
            return _context.Description.ToList();
        }
    }
}
