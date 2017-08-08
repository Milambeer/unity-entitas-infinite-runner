using Entitas;

public class EventCreationSystem : IInitializeSystem {
    private readonly GameEventContext _gameEventcontext;

    public EventCreationSystem(Contexts contexts) {
        _gameEventcontext = contexts.gameEvent;
    }

    public void Initialize() {
        _gameEventcontext.CreateEntity()
            .AddStateEvent("Game");
    }
}