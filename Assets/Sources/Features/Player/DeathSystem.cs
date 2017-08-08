using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class DeathSystem : ReactiveSystem<InputEntity>
{
    private readonly IGroup<GameEventEntity> _groupGameEvent;
    public DeathSystem(Contexts contexts) : base(contexts.input) {
        _groupGameEvent = contexts.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));

    }
    protected override bool Filter(InputEntity entity) {
        return entity.isDeath;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Death);
    }

    protected override void Execute(List<InputEntity> entities) {
        if (entities.Count == 1) {
            GameEventEntity[] entitiesGameEvent = _groupGameEvent.GetEntities();
            GameEventEntity entitieGameEvent = entitiesGameEvent[0];
            entitieGameEvent.ReplaceStateEvent("Death");
        }
    }
}