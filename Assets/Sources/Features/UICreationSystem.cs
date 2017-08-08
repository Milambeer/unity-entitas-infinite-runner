using Entitas;

public class UICreationSystem : IInitializeSystem {
    private readonly GameContext _gameContext;

    public UICreationSystem(Contexts contexts) {
        _gameContext = contexts.game;
    }

    public void Initialize() {
        GameEntity inGameMenu= _gameContext.CreateEntity();
        inGameMenu.AddResource("InGameMenu");
        inGameMenu.AddActiveUI(true);

        GameEntity pauseMenu= _gameContext.CreateEntity();
        pauseMenu.AddResource("PauseMenu");
        pauseMenu.AddActiveUI(false);

        GameEntity gameOverMenu= _gameContext.CreateEntity();
        gameOverMenu.AddResource("GameOverMenu");
        gameOverMenu.AddActiveUI(false);
    }
}