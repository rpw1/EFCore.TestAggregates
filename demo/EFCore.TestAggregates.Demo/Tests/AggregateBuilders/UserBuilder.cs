using EFCore.TestAggregates.Demo.Entities;

namespace EFCore.TestAggregates.Demo.Tests.AggregateBuilders;

internal class UserBuilder : EntityAggregateBuilder<UserEntity, IDemoDbContext>
{
    protected override EntityMap<UserEntity, IDemoDbContext> EntityMap
        => new EntityMap<UserEntity, IDemoDbContext>()
            .Add(u => u.Following, ctx => ctx.UserFollows)
            .Add(u => u.AlbumsWorkedOn, ctx => ctx.ArtistAlbumns);

    private UserBuilder(UserType userType)
    {
        int id = NewId();
        AggregateRoot = new()
        {
            Id = id,
            UserType = userType,
            Username = string.Format("User_{0}", id),
            DisplayName = string.Format("User {0}", id),
            Email = string.Format("email{0}@email.com", id)
        };
    }

    public static UserBuilder Create(UserType userType) => new(userType);

    public UserBuilder AddUsersFollowed(IEnumerable<UserEntity> usersFollowed)
    {
        foreach (var user in usersFollowed)
        {
            AggregateRoot.Following.Add(new UserFollowEntity()
            {
                UserId = user.Id,
                FollowedByUserId = AggregateRoot.Id
            });
        }
        return this;
    }

    public UserBuilder AddAlbumsWorkedOn(IEnumerable<AlbumEntity> albums)
    {
        foreach(var album in albums)
        {
            AggregateRoot.AlbumsWorkedOn.Add(new ArtistAlbumEntity()
            {
                AlbumId = album.Id,
                ArtistId = AggregateRoot.Id
            });
        }

        return this;
    }
}
