using AutoMapper;
using QuizData;
using QuizMvc.Models;

namespace QuizMvc.Helpers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserData>();
            CreateMap<UserData, User>();
            CreateMap<UserRoleData, UserRoleSummary>();
            CreateMap<UserRoleSummary, UserRoleData>();
        }
    }
}