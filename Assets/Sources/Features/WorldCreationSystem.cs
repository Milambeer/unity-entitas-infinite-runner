using Entitas;

public class WorldCreationSystem : IInitializeSystem {
    private readonly GameContext _gameContext;

    public WorldCreationSystem(Contexts contexts) {
        _gameContext = contexts.game;
    }

    public void Initialize() {
        GameEntity topBorder = _gameContext.CreateEntity();
        topBorder.isWorld = true;
        topBorder.AddResource("TopBorder");
        topBorder.AddPosition(0, 11.5f, 0);

        GameEntity bottomBorder = _gameContext.CreateEntity();
        bottomBorder.isWorld = true;
        bottomBorder.AddResource("BottomBorder");
        bottomBorder.AddPosition(0, -9, 0);

        GameEntity leftBorder = _gameContext.CreateEntity();
        leftBorder.isWorld = true;
        leftBorder.isLeftBorder = true;
        leftBorder.AddResource("LeftBorder");
        leftBorder.AddPosition(-25, 1, 0);
    }
}