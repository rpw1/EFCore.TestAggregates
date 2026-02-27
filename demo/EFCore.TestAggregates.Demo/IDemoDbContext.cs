using EFCore.TestAggregates.Demo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.TestAggregates.Demo;

public interface IDemoDbContext
{
    DbSet<AlbumEntity> Albums { get; set; }

    DbSet<ArtistAlbumEntity> ArtistAlbumns { get; set; }

    DbSet<TrackEntity> Tracks { get; set; }

    DbSet<UserEntity> Users { get; set; }

    DbSet<UserFollowEntity> UserFollows { get; set; }
}
