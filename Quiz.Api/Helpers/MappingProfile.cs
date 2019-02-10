using AutoMapper;
using QuizApi.Models;
using QuizData;


namespace QuizApi.Helpers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserData>();
            CreateMap<UserData, User>();
        }
    }
}