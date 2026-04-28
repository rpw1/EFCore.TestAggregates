using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.TestAggregates.Demo.Entities;

[Table("Albums", Schema = "dbo")]
public sealed class AlbumEntity
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime ReleaseDate { get; set; }

    public ICollection<ArtistAlbumEntity> Artists { get; set; } = [];
}
