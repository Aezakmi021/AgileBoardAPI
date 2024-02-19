using AgileBoardAPI.Data;
using AgileBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileBoardAPI.Repository
{
    public interface IProjectRepository
    {
        Task<List<Project>> FindByUser(User user);
        Task<Project> GetById(Guid id);
        Task DeleteById(Guid id);
        Task<List<Project>> FindProjectsBySearchTerm(string searchTerm, string username);
        Task<int> UpdateNameById(Guid projectId, string name);
        Task<Project> Add(Project project);
        Task<Project> Update(Project project);
    }

    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> FindByUser(User user)
        {
            return await _dbContext.Projects.Where(p => p.User == user).ToListAsync();
        }

        public async Task<Project> GetById(Guid id)
        {
            return await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task DeleteById(Guid id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project != null)
            {
                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Project>> FindProjectsBySearchTerm(string searchTerm, string username)
        {
            return await _dbContext.Set<Project>().FromSqlRaw(
                "SELECT * " +
                "FROM project p " +
                "WHERE p.name " +
                "LIKE '%' + @searchTerm + '%' " +
                "AND p.owner_username = @username",
                new { searchTerm, username }
            ).ToListAsync();
        }


        public async Task<int> UpdateNameById(Guid projectId, string name)
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE project SET name = {name} WHERE project_id = {projectId}");
            return result;
        }
    }
}
