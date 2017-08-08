using Entitas;

public class PlayerCreationSystem : IInitializeSystem {

    private readonly GameContext _gameContext;

    public PlayerCreationSystem(Contexts contexts) {
        _gameContext = contexts.game;
    }

    public void Initialize() {
        GameEntity playerEntity = _gameContext.CreateEntity();
        playerEntity.isPlayer = true;
        playerEntity.AddResource("Player");
        playerEntity.AddPosition(-10, 0, 0);
        playerEntity.AddPlayerPhysic(false, false);
    }
}