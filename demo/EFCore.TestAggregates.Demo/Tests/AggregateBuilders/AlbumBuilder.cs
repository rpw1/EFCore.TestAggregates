using EFCore.TestAggregates.Demo.Entities;

namespace EFCore.TestAggregates.Demo.Tests.AggregateBuilders;

internal class AlbumBuilder : EntityAggregateBuilder<AlbumEntity, IDemoDbContext>
{
    protected override EntityMap<AlbumEntity, IDemoDbContext> EntityMap 
        => new EntityMap<AlbumEntity, IDemoDbContext>()
            .Add(a => a.Artists, ctx => ctx.ArtistAlbumns);

    private AlbumBuilder()
    {
        int albumnId = NewId();
        AggregateRoot = new()
        {
            Id = albumnId,
            Name = string.Format("Albumn Name #{0}", albumnId),
            Artists = []
        };
    }

    public static AlbumBuilder Create() => new();

    public AlbumBuilder AddFoundingArtists(IEnumerable<UserEntity> artists)
    {
        foreach(var artist in artists)
        {
            AggregateRoot.Artists.Add(new ArtistAlbumEntity()
            {
                AlbumId = AggregateRoot.Id,
                ArtistId = artist.Id
            });
        }
        return this;
    }
}
