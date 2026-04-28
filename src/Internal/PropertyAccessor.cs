namespace EFCore.TestAggregates.Internal;

internal sealed record PropertyAccessor<TAggregateRoot, TEntity>(Func<TAggregateRoot, ICollection<TEntity>> Accessor);
