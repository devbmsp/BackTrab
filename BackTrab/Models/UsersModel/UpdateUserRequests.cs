using System.ComponentModel.DataAnnotations;

namespace TrabalhoBackEnd.Models.Requests
{
    public class UpdateUserRequests
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
