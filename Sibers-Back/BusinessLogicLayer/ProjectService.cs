using BusinessLogicLayer.Dto.ProjectDtos;
using DataAccessLayer.Data;
using DataAccessLayer.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLogicLayer.Dto;
using DataLayer.Enums;
using System.Globalization;
using DataLayer;

namespace BusinessLogicLayer
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, 
                PropertyNameCaseInsensitive = true, 
                WriteIndented = true
            };
        }

        public async Task<PagedResult<ProjectDetailsDto>> GetAllProjectsAsync(int page, int pageSize)
        {
            var query = _context.Projects         
                .Include(p => p.ProjectManager)
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var projects = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var projectDtos = projects.Select(p => new ProjectDetailsDto
            {
                Id = p.ProjectId,
                Name = p.Name,
                CustomerCompany = p.CustomerCompany,
                ExecutorCompany = p.ExecutorCompany,
                ProjectManager = p.ProjectManagerId != null ? $"{ p.ProjectManager.FirstName } { p.ProjectManager.LastName }" : "No Project Manager",
                Priority = p.Priority,
                ProjectManagerId = p.ProjectManagerId,
                Employees = p.EmployeeProjects.Select(ep => new EmployeeGetDtoForProject
                {
                    Id = ep.Employee.EmployeeId,
                    FirstName = ep.Employee.FirstName,
                    LastName = ep.Employee.LastName,
                    MiddleName = ep.Employee.MiddleName,
                    Email = ep.Employee.Email,
                    Position = ep.Employee.Position
                }).ToList(),
                StartDate = p.StartDate,
                EndDate = p.EndDate
            }).ToList();

            return new PagedResult<ProjectDetailsDto>
            {
                Items = projectDtos,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
          
        }

        public async Task<ProjectDetailsDto> GetProjectByIdAsync(int id)
        {
            var p = await _context.Projects
                    .AsNoTracking()
                    .Include(p => p.ProjectManager)
                    .Include(p => p.EmployeeProjects)
                        .ThenInclude(ep => ep.Employee)
                    .FirstOrDefaultAsync(p => p.ProjectId == id);

            var result = new ProjectDetailsDto()
            {
                Id = p.ProjectId,
                Name = p.Name,
                CustomerCompany = p.CustomerCompany,
                ExecutorCompany = p.ExecutorCompany,
                Priority = p.Priority,
                ProjectManagerId = p.ProjectManagerId,
                Employees = p.EmployeeProjects.Select(ep => new EmployeeGetDtoForProject
                {
                    Id = ep.Employee.EmployeeId,
                    FirstName = ep.Employee.FirstName,
                    LastName = ep.Employee.LastName,
                    MiddleName = ep.Employee.MiddleName,
                    Email = ep.Employee.Email,
                    Position = ep.Employee.Position
                }).ToList(),
                StartDate = p.StartDate,
                EndDate = p.EndDate
            };
            return result;
        }
        public DateTime convertDateFromString(string dateString)
        {
            DateTime date;

            if (!DateTime.TryParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                throw new ArgumentException("Invalid StartDate format.");
            }
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            return date;
        }
        public async Task<int> AddProjectAsync(ProjectPostDto project)
        {
      
            var pr = new Project()
            {
                Name = project.Name,
                CustomerCompany = project.CustomerCompany,
                ExecutorCompany = project.ExecutorCompany,
                Priority =(ProjectPriorityEnum) project.Priority,
                ProjectManagerId = project.ProjectManagerId,
                StartDate = convertDateFromString(project.StartDate),
                EndDate = convertDateFromString(project.EndDate)
            };
    
            _context.Projects.Add(pr);
            await _context.SaveChangesAsync();

            Console.WriteLine(pr.ProjectId);
            if (project.EmployeeIds != null)
            {
                foreach (var employeeID in project.EmployeeIds)
                {
                    var employee = await _context.Employees.FindAsync(employeeID);
                    if (employee != null)
                    {
                        var projectEmployee = new ProjectEmployee
                        {
                            EmpId = employee.EmployeeId,
                            PrId = pr.ProjectId
                        };
                        _context.ProjectEmployees.Add(projectEmployee);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return pr.ProjectId;
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectPostDto projectDto)
        {

            var existingProject = await _context.Projects
                .Include(p => p.EmployeeProjects)
                .FirstOrDefaultAsync(p => p.ProjectId == id);


            if (existingProject == null) return false;
           

            existingProject.Name = projectDto.Name;
            existingProject.CustomerCompany = projectDto.CustomerCompany;
            existingProject.ExecutorCompany = projectDto.ExecutorCompany;
            existingProject.Priority = (ProjectPriorityEnum) projectDto.Priority;
            existingProject.ProjectManagerId = projectDto.ProjectManagerId;
            existingProject.StartDate = convertDateFromString(projectDto.StartDate);
            existingProject.EndDate = convertDateFromString(projectDto.EndDate);

            if (projectDto.EmployeeIds != null)
            {
                var projectsToDelete = existingProject.EmployeeProjects.Where(p => !projectDto.EmployeeIds.Contains(p.EmpId)).ToList();

                foreach(var employeeId in projectsToDelete)
                {
                    existingProject.EmployeeProjects.Remove(employeeId);
                }

                foreach (var employeeId in projectDto.EmployeeIds)
                {
                    var existingEmps = existingProject.EmployeeProjects.FirstOrDefault(e => e.EmpId == employeeId); 
                    if (existingEmps == null)
                    {
                        var update = new ProjectEmployee()
                        {
                            EmpId = employeeId,
                            PrId = existingProject.ProjectId
                        };
                        existingProject.EmployeeProjects.Add(update);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var projets = await _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.EmployeeProjects)
                    .ThenInclude(pe => pe.Employee)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (projets == null) return false;

            if (projets.ProjectManagerId != null) projets.ProjectManagerId = null;


            var projectsToDelete = projets.EmployeeProjects.Where(p => p.PrId == id).ToList();

            foreach(var pr in projectsToDelete)
            {
                if (pr != null)
                    _context.ProjectEmployees.Remove(pr);
            }


            _context.Projects.Remove(projets);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectForEmployeeDto>> GetAllProjectsForEmployeeAsync()
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .ToListAsync();

            var projectDtos = projects.Select(p => new ProjectForEmployeeDto()
            {
                Id = p.ProjectId,
                Name = p.Name,
                CustomerCompany = p.CustomerCompany,
                ExecutorCompany = p.ExecutorCompany,
                Priority = p.Priority,
                ProjectManagerId = p.ProjectManagerId
            }).ToList();

            return projectDtos;
        }
    }
}
