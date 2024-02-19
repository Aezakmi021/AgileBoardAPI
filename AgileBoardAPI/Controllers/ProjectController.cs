using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgileBoardAPI.DTO;
using AgileBoardAPI.Services;
using AgileBoardAPI.Interfaces;

namespace AgileBoardAPI.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ProjectDto>>> FindProjectsBySearchTerm([FromQuery] string searchTerm)
        {
            var projects = await _projectService.FindProjectsBySearchTerm(searchTerm);
            return Ok(projects);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjectsForUser()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(Guid id)
        {
            var project = await _projectService.GetById(id);

            if (project == null)
            {
                return NotFound(); // Or any other appropriate NotFound response
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> AddProject([FromBody] CreateProject project)
        {
            var createdProject = await _projectService.CreateProject(project);
            return Ok(createdProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProject(Guid id)
        {
            try
            {
                await _projectService.DeleteProject(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // Handle the exception appropriately, for example, return a NotFound response.
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle the exception appropriately, for example, return a Forbidden response.
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions if needed
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> UpdateProject([FromBody] ProjectDto project)
        {
            var updatedProject = await _projectService.Update(project);
            return Ok(updatedProject);
        }
    }
}
