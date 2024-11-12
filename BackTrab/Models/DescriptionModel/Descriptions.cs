using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrabalhoBackEnd.Models
{
    public class Descriptions
    {
        [Table("Role")]
        public class Role
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string Description { get; set; }
        }
    }
}
