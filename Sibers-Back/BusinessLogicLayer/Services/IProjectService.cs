using BusinessLogicLayer.Dto.ProjectDtos;
using DataAccessLayer.Entity.Models;
using DataLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IProjectService
    {
        Task<PagedResult<ProjectDetailsDto>> GetAllProjectsAsync(int page, int pageSize);
        Task<IEnumerable<ProjectForEmployeeDto>> GetAllProjectsForEmployeeAsync();
        Task<ProjectDetailsDto> GetProjectByIdAsync(int id);
        Task<int> AddProjectAsync(ProjectPostDto project);
        Task<bool> UpdateProjectAsync(int id, ProjectPostDto project);
        Task<bool> DeleteProjectAsync(int id);
    }
}
