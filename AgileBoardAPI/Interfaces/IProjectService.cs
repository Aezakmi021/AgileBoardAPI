using AgileBoardAPI.DTO;

namespace AgileBoardAPI.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> FindProjectsBySearchTerm(string searchTerm);
        Task<ProjectDto> CreateProject(CreateProject newProject);
        Task DeleteProject(Guid id);
        Task<List<ProjectDto>> GetAllProjects();
        Task<ProjectDto> GetById(Guid id);
        Task<ProjectDto> Update(ProjectDto project);
    }
}
