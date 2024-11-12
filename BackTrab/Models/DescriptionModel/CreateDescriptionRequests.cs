using static TrabalhoBackEnd.Models.Descriptions;
using System.ComponentModel.DataAnnotations;

namespace TrabalhoBackEnd.Models.Requests
{
    public class CreateRoleRequest
    {
        
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public Role ToRole()
        {
            return new Role
            {
                Name = Name,
                Description = Description
            };
        }
    }
}
