using AutoMapper;
using TodoApp.DataAccess.Entities;
using TodoApp.Services.Models;

namespace TodoApp.Services.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TodoItemInputModel, TodoItem>();
            CreateMap<TodoItem, TodoItemOutputModel>();
        }
    }
}
