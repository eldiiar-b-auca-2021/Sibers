using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.ProjectDtos;
using DataAccessLayer.Data;
using DataAccessLayer.Entity.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeGetDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(emp => emp.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .ToListAsync();

            var employeeDto = employees.Select(emp => new EmployeeGetDto
            {
                   Id = emp.EmployeeId,
                   FirstName = emp.FirstName,
                   LastName = emp.LastName,
                   MiddleName = emp.MiddleName,
                   Email = emp.Email,   
                   Position = emp.Position,
                   Projects = emp.EmployeeProjects.Select(ep => new ProjectDto
                   {
                       Id = ep.Project.ProjectId,
                       Name = ep.Project.Name,
                       CustomerCompany = ep.Project.CustomerCompany,
                       ExecutorCompany = ep.Project.ExecutorCompany,
                       Priority = ep.Project.Priority,
                       ProjectManagerId = ep.Project.ProjectManagerId,
                       StartDate = ep.Project.StartDate,
                       EndDate = ep.Project.EndDate
                   }).ToList(),

            }).ToList();


            return employeeDto;
        }

        public async Task<EmployeeGetDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            var employeeDto = employee.Adapt<EmployeeGetDto>();
            return employeeDto;
        }

        public async Task<EmployeeGetDto> AddEmployeeAsync(EmployeePostDto employeeDto)
        {

            var employee = new Employee()
            {
                Email = employeeDto.Email,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                MiddleName = employeeDto.MiddleName,
                Position = employeeDto.Position
            };


            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            if (employeeDto.ProjectIds != null)
            {
                foreach (var projectId in employeeDto.ProjectIds)
                {
                    var project = await _context.Projects.FindAsync(projectId);
                    if (project != null)
                    {
                        var projectEmployee = new ProjectEmployee
                        {
                            EmpId = employee.EmployeeId,
                            PrId = project.ProjectId
                        };
                        _context.ProjectEmployees.Add(projectEmployee);
                    }
                }
                await _context.SaveChangesAsync();
            }
            var emp = employee.Adapt<EmployeeGetDto>();
            return emp;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, EmployeePostDto employee)
        {
            var existingEmployee = await _context.Employees.Include(e => e.EmployeeProjects).FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (existingEmployee == null)
            {
                return false;
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.MiddleName = employee.MiddleName;
            existingEmployee.Email = employee.Email;

            
            if (employee.ProjectIds != null)
            {
               
                var projectsToRemove = existingEmployee.EmployeeProjects.Where(p => !employee.ProjectIds.Contains(p.PrId)).ToList();
                foreach (var projectToRemove in projectsToRemove)
                {
                    existingEmployee.EmployeeProjects.Remove(projectToRemove);
                }

                foreach (var projectId in employee.ProjectIds)
                {
                    var existingProject = existingEmployee.EmployeeProjects.FirstOrDefault(p => p.PrId == projectId);
                    if (existingProject == null)
                    {
                        var projectEmployee = new ProjectEmployee
                        {
                            EmpId = existingEmployee.EmployeeId,
                            PrId = projectId
                        };
                        existingEmployee.EmployeeProjects.Add(projectEmployee);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return false;
            }

            var projects = await _context.Projects.Where(p => p.ProjectManagerId != null && p.ProjectManagerId == id).ToListAsync();

            foreach ( var project in projects)
            {
                project.ProjectManagerId = null;
            }
        
            foreach (var projectEmployee in employee.EmployeeProjects.Where(ep => ep.Project.ProjectManagerId != null && ep.Project.ProjectManagerId == id).ToList())
            {
                projectEmployee.Project.ProjectManagerId = null; 
                _context.ProjectEmployees.Remove(projectEmployee);
            }
     
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeGetDtoForProject>> GetEmployeesForProjectAsync()
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .ToListAsync();

            var employeeDto = employees.Select(emp => new EmployeeGetDtoForProject
            {
                  Id = emp.EmployeeId,
                  FirstName = emp.FirstName,
                  LastName = emp.LastName,
                  MiddleName = emp.MiddleName,
                  Email = emp.Email,
                  Position = emp.Position
            }).ToList();

            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeGetDtoForProject>> GetEmployeesProjectManagerAsync()
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Where(e => e.Position == "Project Manager")
                .ToListAsync();

            var employeeDtos = employees.Adapt<IEnumerable<EmployeeGetDtoForProject>>();

            return employeeDtos;
        }
    }
}
