using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.TestAggregates.Demo.Entities;

[Table("AlbumArtists", Schema = "dbo")]
public class ArtistAlbumEntity
{
    [Key]
    public int Id { get; set; }

    public required int AlbumId { get; set; }

    public required int ArtistId { get; set; }

    [ForeignKey(nameof(AlbumId))]
    public AlbumEntity? Album { get; set; }

    [ForeignKey(nameof(ArtistId))]
    public UserEntity? Artist { get; set; }
}
