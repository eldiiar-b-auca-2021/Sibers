using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.ProjectDtos;
using DataAccessLayer.Entity.Models;
using Mapster;

namespace BusinessLogicLayer.Mappings
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<EmployeePostDto, Employee>.NewConfig()
                .Map(dest => dest.EmployeeProjects, src => src.ProjectIds.Select(id => new ProjectEmployee { PrId = id }));

            TypeAdapterConfig<Employee, EmployeeGetDto>.NewConfig()
                .Map(dest => dest.Projects,
                     src => src.EmployeeProjects.Select(ep => ep.Project.Adapt<ProjectDto>()).ToList());

            TypeAdapterConfig<Employee, EmployeeGetDtoForProject>.NewConfig();

            TypeAdapterConfig<Project, ProjectDto>.NewConfig();


        }
    }
}
