using AutoMapper;
using Domain.Users;

namespace Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandProfile : Profile
    {
        public LoginUserCommandProfile()
        {
            CreateMap<LoginUserCommand, User>()
                .ForMember(x => x.UserName, opt =>
                    opt.MapFrom(xx => xx.Username))
                ;
        }
    }
}