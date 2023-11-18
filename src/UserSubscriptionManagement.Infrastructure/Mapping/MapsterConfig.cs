using Mapster;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Entities;

namespace UserSubscriptionManagement.Infrastructure.Mapping;
public class MapsterConfig
{
    public TypeAdapterConfig Config { get; }

    public MapsterConfig()
    {
        Config = new TypeAdapterConfig();

        Config.NewConfig<Users, User>()
            .Map(dest => dest.Role, src => src.AdminType.AdminType);

        Config.NewConfig<User, Users>()
            .Map(dest => dest.ProfileId, src => src.Profile.Id)
            .IgnoreIf((src, dest) => src.Profile == null, src => src.Profile);

        Config.NewConfig<UserInfoDto, User>()
            .Map(dest => dest.Profile.Address, src => src.Address)
            .Map(dest => dest.Profile.Email, src => src.Email)
            .Map(dest => dest.Profile.FirstName, src => src.FirstName)
            .Map(dest => dest.Profile.LastName, src => src.LastName)
            .Map(dest => dest.Profile.MiddleName, src => src.MiddleName)
            .Map(dest => dest.Profile.Phone, src => src.Phone)
            .Map(dest => dest.Profile.Zip, src => src.Zip);

        Config.NewConfig<UserSubscription, UserSubscriptions>()
            .Map(dest => dest.UserId, src => src.User.Id)
            .Map(dest => dest.SubscriptionId, src => src.Subscription.Id)
            .Ignore(x => x.User)
            .Ignore(x => x.Subscription);
    }
}