using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.TestAggregates.Demo.Entities;

[Table("Follows", Schema = "dbo")]
public class UserFollowEntity
{
    [Key]
    public int Id { get; set; }

    public required int UserId { get; set; }

    public required int FollowedByUserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }

    [ForeignKey(nameof(FollowedByUserId))]
    public UserEntity? FollowedByUser { get; set; }
}
