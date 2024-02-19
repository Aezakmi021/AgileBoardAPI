using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileBoardAPI.DTO;
using AgileBoardAPI.Interfaces;
using AgileBoardAPI.Models;
using AgileBoardAPI.Repository;
using Microsoft.AspNetCore.Http;

namespace AgileBoardAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProjectDto>> FindProjectsBySearchTerm(string searchTerm)
        {
            string username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var projects = await _projectRepository.FindProjectsBySearchTerm(searchTerm, username);
            return projects.Select(GetProjectDto).ToList();
        }

        public async Task<ProjectDto> CreateProject(CreateProject newProject)
        {
            var user = await _userRepository.GetByUsername(_httpContextAccessor.HttpContext.User.Identity.Name);

            var project = new Project
            {
                Name = newProject.Name,
                Description = newProject.Description,
                CreatedAt = DateTime.Now,
                User = user
            };

            var createdProject = await _projectRepository.Add(project);
            return GetProjectDto(createdProject);
        }

        public async Task DeleteProject(Guid id)
        {
            var project = await _projectRepository.GetById(id);

            string username = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (project.User.Username == username)
            {
                await _projectRepository.DeleteById(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Unauthorized action");
            }
        }

        public async Task<List<ProjectDto>> GetAllProjects()
        {
            string username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userRepository.GetByUsername(username);

            var projects = await _projectRepository.FindByUser(user);
            return projects.Select(GetProjectDto).ToList();
        }

        public async Task<ProjectDto> GetById(Guid id)
        {
            var project = await _projectRepository.GetById(id);
            return GetProjectDto(project);
        }

        public async Task<ProjectDto> Update(ProjectDto project)
        {
            var existingProject = await _projectRepository.GetById(project.Id);

            if (existingProject == null)
            {
                throw new InvalidOperationException("Project not found");
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            var updatedProject = await _projectRepository.Update(existingProject);
            return GetProjectDto(updatedProject);
        }

        private ProjectDto GetProjectDto(Project project)
        {
            var userDto = new ProjectDto.ProjectUserDto
            {
                Id = project.User.Id,
                FirstName = project.User.FirstName,
                LastName = project.User.LastName,
                Username = project.User.Username
            };

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = (DateTime)project.CreatedAt,
                User = userDto
            };

            return projectDto;
        }


    }
}
