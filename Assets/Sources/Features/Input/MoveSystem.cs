using Entitas;

public sealed class MoveSystem : IExecuteSystem {
    private readonly IGroup<GameEntity> _groupMovable;
    private readonly IGroup<GameEventEntity> _groupEvent;

    public MoveSystem(Contexts contexts) {
        _groupMovable = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Move, GameMatcher.Position));
        _groupEvent = contexts.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));
    }

    public void Execute() {
        GameEventEntity[] entities = _groupEvent.GetEntities();
        if (entities.Length != 0) {
            GameEventEntity entity = entities[0];
            if (entity.stateEvent.state == "Game") {
                foreach (GameEntity e in _groupMovable.GetEntities()) {
                    var move = e.move;
                    var pos = e.position;
                    e.ReplacePosition(pos.x - move.speed, pos.y, pos.z);
                }
            }
        }
    }
}