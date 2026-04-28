using EFCore.TestAggregates.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFCore.TestAggregates;

public sealed class EntityMap<TAggregateRoot, TDbContext> 
    where TDbContext : class
{
    public ICollection<ITrackedEntity<TAggregateRoot, TDbContext>> TrackedEntities { get; private set; } = [];

    public EntityMap<TAggregateRoot, TDbContext> Add<TEntity>(
        Func<TAggregateRoot, ICollection<TEntity>> propertyAccessor,
        Expression<Func<TDbContext, DbSet<TEntity>>> dbSetAccessor
    ) where TEntity : class
    {
        TrackedEntities.Add(
            TrackedEntity.Create(
                new PropertyAccessor<TAggregateRoot, TEntity>(propertyAccessor),
                new DbSetAccessor<TDbContext, TEntity>(dbSetAccessor)
            )
        );
        return this;
    }

    public EntityMap<TAggregateRoot, TDbContext> Add<TEntity>(
        Func<TAggregateRoot, TEntity?> propertyAccessor,
        Expression<Func<TDbContext, DbSet<TEntity>>> dbSetAccessor
    ) where TEntity : class
    {
        TrackedEntities.Add(
            TrackedEntity.Create(
                new PropertyAccessor<TAggregateRoot, TEntity>(aggregate => [propertyAccessor(aggregate)!]),
                new DbSetAccessor<TDbContext, TEntity>(dbSetAccessor)
            )
        );
        return this;
    }
}
