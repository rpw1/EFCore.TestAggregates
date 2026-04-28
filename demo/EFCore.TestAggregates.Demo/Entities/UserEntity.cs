using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.TestAggregates.Demo.Entities;

[Table("Users", Schema = "dbo")]
public class UserEntity
{
    [Key]
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string DisplayName { get; set; }

    public required string Email { get; set; }

    public required UserType UserType { get; set; }

    public ICollection<UserFollowEntity> Following { get; set; } = [];

    public ICollection<ArtistAlbumEntity> AlbumsWorkedOn { get; set; } = [];
}

public enum UserType
{
    Admin,
    User,
    Artist
}
