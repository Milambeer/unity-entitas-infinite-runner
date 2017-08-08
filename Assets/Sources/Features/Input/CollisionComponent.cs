using Entitas;

[Input]
public sealed class CollisionComponent : IComponent {
    public GameEntity from;
    public GameEntity to;
}