using UnityEngine;
using Entitas;
using System.Collections.Generic;

public sealed class PlayerPhysicSystem : ReactiveSystem<GameEntity>
{

    public PlayerPhysicSystem(Contexts contexts) : base(contexts.game) {}

    protected override bool Filter(GameEntity entity) {
        return entity.hasPlayerPhysic;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.PlayerPhysic);
    }

    protected override void Execute(List<GameEntity> entities) {
        if (entities.Count != 0) {
            GameEntity player = entities[0];
            if (player.playerPhysic.isStable) {
                player.view.gameObject.GetComponent<Rigidbody>().useGravity = false;
                player.view.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            } else {
                player.view.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }

            if (player.playerPhysic.isJumping) {
                player.view.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,7,0);
                player.ReplacePlayerPhysic(false, player.playerPhysic.isStable);
            }
        }
    }
}