using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.TestAggregates.Demo.Entities;

[Table("Tracks", Schema = "dbo")]
public class TrackEntity
{
    [Key]
    public int Id { get; set; }

    public required int AlbumId { get; set; }

    [ForeignKey(nameof(AlbumId))]
    public AlbumEntity? Album { get; set; }

    public required string Name { get; set; }

    public required TimeSpan Duration { get; set; }
}
