using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService skillService;
        public SkillsController(ISkillService skillService)
        {
            this.skillService = skillService ?? throw new ArgumentNullException(nameof(skillService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var skills = skillService.GetAll();

            return Ok(skills);
        }
    }
}
