using BusinessLogicLayer;
using BusinessLogicLayer.Dto.ProjectDtos;
using DataAccessLayer.Entity.Models;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Sibers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("allProjects")]
        public async Task<ActionResult<PagedResult<ProjectDetailsDto>>> GetProjects([FromQuery] int page, [FromQuery] int pageSize)
        {
            var projects = await _projectService.GetAllProjectsAsync(page, pageSize);
            return Ok(projects);
        }

        [HttpGet("projectForEmployee")]
        public async Task<ActionResult<IEnumerable<ProjectForEmployeeDto>>> GetProjectsForEmployee()
        {
            var projects = await _projectService.GetAllProjectsForEmployeeAsync();
            return Ok(projects);
        }

        [HttpGet("project/{id}")]
        public async Task<ActionResult<ProjectDetailsDto>> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null) return NotFound(); 
            

            return project;
        }

        [HttpPost("addProject")]
        public async Task<ActionResult<int>> PostProject([FromBody] ProjectPostDto project)
        {
            var createdProject = await _projectService.AddProjectAsync(project);
            return createdProject;
        }

        [HttpPut("updateProject/{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectPostDto project)
        {
            var result = await _projectService.UpdateProjectAsync(id, project);
            if (!result) return BadRequest(result);
            

            return NoContent();
        }

        [HttpDelete("deleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
