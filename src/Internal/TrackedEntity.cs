using MockQueryable.Moq;
using Moq;

namespace EFCore.TestAggregates.Internal;

public interface ITrackedEntity<TAggregateRoot, TDbContext> 
    where TDbContext : class
{
    public void ConfigureDbSet(Mock<TDbContext> mockDbContext, ICollection<TAggregateRoot> aggregateRoots);
}

internal sealed class TrackedEntity<TAggregateRoot, TDbContext, TEntity>(
    PropertyAccessor<TAggregateRoot, TEntity> propertyAccessor, 
    DbSetAccessor<TDbContext, TEntity> dbSetAccessor
) : ITrackedEntity<TAggregateRoot, TDbContext>
    where TEntity : class
    where TDbContext : class
{
    public void ConfigureDbSet(Mock<TDbContext> mockDbContext, ICollection<TAggregateRoot> aggregateRoots)
    {
        var entities = aggregateRoots.SelectMany(propertyAccessor.Accessor)
            ?.Where(entity => entity is not null) 
            ?? [];

        _ = mockDbContext.Setup(dbSetAccessor.Accessor).Returns(entities.ToArray().BuildMockDbSet().Object);
    }
}

internal static class TrackedEntity
{
    internal static ITrackedEntity<TAggregateRoot, TDbContext> Create<TAggregateRoot, TDbContext, TEntity>(
        PropertyAccessor<TAggregateRoot, TEntity> propertyAccessor,
        DbSetAccessor<TDbContext, TEntity> dbSetAccessor
    ) where TEntity : class
        where TDbContext : class
        => new TrackedEntity<TAggregateRoot, TDbContext, TEntity>(propertyAccessor, dbSetAccessor);

    extension<TAggregateRoot, TDbContext>(IEnumerable<ITrackedEntity<TAggregateRoot, TDbContext>> trackedEntities)
        where TDbContext : class
    {
        internal void SetupDbSets(Mock<TDbContext> mockDbContext, TAggregateRoot aggregateRoot)
        {
            foreach(var trackedEntity in trackedEntities)
            {
                trackedEntity.ConfigureDbSet(mockDbContext, [aggregateRoot]);
            }
        }

        internal void SetupDbSets(Mock<TDbContext> mockDbContext, ICollection<TAggregateRoot> aggregateRoot)
        {
            foreach (var trackedEntity in trackedEntities)
            {
                trackedEntity.ConfigureDbSet(mockDbContext, aggregateRoot);
            }
        }
    }
}