using EFCore.TestAggregates.Demo.Entities;

namespace EFCore.TestAggregates.Demo.Tests.AggregateBuilders;

internal class TrackBuilder : EntityAggregateBuilder<TrackEntity, IDemoDbContext>
{
    protected override EntityMap<TrackEntity, IDemoDbContext> EntityMap
        => new EntityMap<TrackEntity, IDemoDbContext>()
            .Add(t => t.Album, ctx => ctx.Albums);

    private TrackBuilder(AlbumEntity? album)
    {
        int id = NewId();
        int albumnId = album is null ? NewId() : album.Id;
        AggregateRoot = new()
        {
            Id = id,
            AlbumId = albumnId,
            Album = album,
            Name = string.Format("Track_{0}", id),
            Duration = TimeSpan.FromMinutes(3)
        };
    }

    public static TrackBuilder Create(AlbumEntity? album = null) => new(album);

    public TrackBuilder WithDuration(TimeSpan duration)
    {
        AggregateRoot.Duration = duration;
        return this;
    }
}
