using AutoMapper;
using Core.Entities.Users;

namespace Core.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandProfile : Profile
    {
        public LoginUserCommandProfile()
        {
            CreateMap<LoginUserCommand, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(xx => xx.EmailAddress))
                ;
        }
    }
}