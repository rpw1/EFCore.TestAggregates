using EFCore.TestAggregates.Internal;
using Moq;

namespace EFCore.TestAggregates;

public abstract class EntityAggregateBuilder<TAggregateRoot, TDbContext> 
    where TAggregateRoot : class
    where TDbContext : class
{
    private readonly Random _random = new();

    protected TAggregateRoot AggregateRoot { get; set; } = null!;

    protected int NewId() => _random.Next(100000);

    protected abstract EntityMap<TAggregateRoot, TDbContext> EntityMap { get; }

    public TAggregateRoot Build() => AggregateRoot ?? throw new InvalidOperationException("Aggregate root must not be defined.");

    public void SetupDbSets(Mock<TDbContext> mockDbContext, IEnumerable<EntityAggregateBuilder<TAggregateRoot, TDbContext>> builders)
    {
        EntityMap.TrackedEntities.SetupDbSets(mockDbContext, builders.Build());
    }

    public void SetupDbSets(Mock<TDbContext> mockDbContext)
    {
        EntityMap.TrackedEntities.SetupDbSets(mockDbContext, Build());
    }

    public TAggregateRoot SetupDbSetsAndBuild(Mock<TDbContext> mockDbContext)
    {
        SetupDbSets(mockDbContext);
        return Build();
    }
}

public static class EntityAggregateBuilder
{
    public static IEnumerable<EntityAggregateBuilder<TAggregateRoot, TDbContext>> CreateMany<TAggregateRoot, TDbContext>(
        IEnumerable<EntityAggregateBuilder<TAggregateRoot, TDbContext>> builders
    ) where TAggregateRoot : class
        where TDbContext : class
        => builders;

    extension<TAggregateRoot, TDbContext>(IEnumerable<EntityAggregateBuilder<TAggregateRoot, TDbContext>> builders)
        where TAggregateRoot : class
        where TDbContext : class
    {
        public void SetupDbSets(Mock<TDbContext> mockDbContext)
        {
            builders.First().SetupDbSets(mockDbContext, builders);
        }

        public ICollection<TAggregateRoot> Build()
            => [.. builders.Select(builder => builder.Build())];

        public ICollection<TAggregateRoot> SetupDbSetsAndBuild(Mock<TDbContext> mockDbContext)
        {
            builders.SetupDbSets(mockDbContext);
            return builders.Build();
        }
    }
}