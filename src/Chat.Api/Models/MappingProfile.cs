using AutoMapper;
using Chat.Api.Models.Groups;
using Chat.Api.Models.Users;

namespace Chat.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AllowNullCollections = true;
            this.AllowNullDestinationValues = true;

            // api -> db
            this.CreateMap<CreateUserRequest, Db.Models.AppIdentityUser>();
            this.CreateMap<User, Db.Models.AppIdentityUser>();
            this.CreateMap<Group, Db.Models.Group>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom((src, dest) => src.GroupId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => src.GroupName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom((src, dest) => src.GroupImage));


            // db -> api
            this.CreateMap<Db.Models.AppIdentityUser, CreateUserRequest>();
            this.CreateMap<Db.Models.AppIdentityUser, User>();
            this.CreateMap<Db.Models.Group, Group>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom((src, dest) => src.GroupId))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom((src, dest) => src.Name))
                .ForMember(dest => dest.Users, opt => opt.MapFrom((src, dest) => src.UserGroups?.Select(ug => ug?.User)))
                .ForMember(dest => dest.GroupImage, opt => opt.MapFrom((src, dest) => src.Image));
        }
    }
}
