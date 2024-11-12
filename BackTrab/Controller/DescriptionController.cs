using Microsoft.AspNetCore.Mvc;
using static TrabalhoBackEnd.Models.Descriptions;
using TrabalhoBackEnd.BLL;
using TrabalhoBackEnd.Models.Requests;
using TrabalhoBackEnd.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace TrabalhoBackEnd.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DescriptionController : ControllerBase
    {
        private readonly DescriptionService _descriptionService;

        public DescriptionController(DescriptionService descriptionService)
        {
            _descriptionService = descriptionService;
        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleRequest descriRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var descricao = descriRequest.ToRole();
            _descriptionService.AddRole(descricao);

            return Ok("Role Criada.");
        }

        [HttpGet]
        public ActionResult<List<Role>> GetAllRoles()
        {
            return _descriptionService.GetAllRoles();
        }

        [HttpGet("{id}")]
        public ActionResult<DescriptionResponse> GetRoleById(int id)
        {
            try
            {
                var description  = _descriptionService.GetRoleById(id);
                var response = new DescriptionResponse(description);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
