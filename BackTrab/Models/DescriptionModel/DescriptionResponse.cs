using static TrabalhoBackEnd.Models.Descriptions;

namespace TrabalhoBackEnd.Models.Responses
{
    public class DescriptionResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DescriptionResponse(Role description)
        {
            Name = description.Name;
            Description = description.Description;
        }
    }
}
