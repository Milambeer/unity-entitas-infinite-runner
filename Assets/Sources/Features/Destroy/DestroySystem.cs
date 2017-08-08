using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class DestroySystem : ReactiveSystem<GameEntity>
{
    public GameContext _gameContext;
    private IGroup<GameEventEntity> _groupGameEvent;
    
    public DestroySystem(Contexts contexts) : base(contexts.game){
        _gameContext = contexts.game;
        _groupGameEvent = contexts.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));
    }
    protected override bool Filter(GameEntity entity) {
        return entity.isDestroyable;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Destroyable);
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity e in entities) {
            if (e.hasView) {
                e.view.gameObject.Unlink();
            }
            e.Destroy();
        }

        GameEventEntity gameEventEntity = null;
		GameEventEntity[] gameEventEntities = _groupGameEvent.GetEntities();
		if (gameEventEntities.Length != 0) {
			gameEventEntity = gameEventEntities[0];
			if (gameEventEntity.stateEvent.state == "Reload") {
				gameEventEntity.ReplaceStateEvent("Reload Ready");
			}	
		}
    }
}