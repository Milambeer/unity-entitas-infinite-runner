using System.Collections.Generic;
using Entitas;
using System;

public sealed class CollisionSystem : ReactiveSystem<InputEntity>
{
    private InputContext _contextInput;
    
    public CollisionSystem(Contexts contexts) : base(contexts.input) {
        _contextInput = contexts.input;
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasCollision;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Collision);
    }

    protected override void Execute(List<InputEntity> entities) {
        foreach (InputEntity e in entities) {
            GameEntity from = e.collision.from;
            GameEntity to = e.collision.to;

            string toResourceName = to.resource.name;
            if (from != null && to != null) {
                if (from.isPlayer) {
                    switch (toResourceName) {
                        case "BottomBorder":
                            from.isDestroyable = true;
                            createDeathEvent();
                            break;
                        case "TopBorder":
                            break;
                        case "Obstacle":
                            from.isDestroyable = true;
                            createDeathEvent();
                            break;
                        default:
                            throw new Exception("Target resource " + toResourceName + " in collision system not found");
                    }
                } else if (from.isLeftBorder) {
                    switch (toResourceName) {
                        case "Obstacle":
                            to.isDestroyable = true;
                            break;
                    }
                }
            }
            e.Destroy();
        }
    }

    private void createDeathEvent() {
        InputEntity inputEntity = _contextInput.CreateEntity();
        inputEntity.isDeath = true;
    }
}