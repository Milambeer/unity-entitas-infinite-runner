using System.Collections.Generic;
using Entitas;
using UnityEngine;
public sealed class GameEventSystem : ReactiveSystem<GameEventEntity>
{
    private GameContext _contextGame;
    private InputContext _contextInput;
    private IGroup<GameEntity> _groupUI;
    
    public GameEventSystem(Contexts contexts) : base(contexts.gameEvent) {
        _contextGame = contexts.game;
        _contextInput = contexts.input;
        _groupUI = _contextGame.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.ActiveUI));
    }

    protected override bool Filter(GameEventEntity entity) {
        return entity.hasStateEvent;
    }

    protected override ICollector<GameEventEntity> GetTrigger(IContext<GameEventEntity> context) {
        return context.CreateCollector(GameEventMatcher.StateEvent);
    }

    protected override void Execute(List<GameEventEntity> entities) {
        GameEntity[] entitiesUI = _groupUI.GetEntities();
        foreach (GameEventEntity e in entities) {
            switch (e.stateEvent.state) {
                case "Pause" :
                    Time.timeScale = 0f;
                    foreach (GameEntity eUI in entitiesUI) {
                        switch (eUI.resource.name) {
                            case "PauseMenu":
                                eUI.ReplaceActiveUI(true);
                                break;
                            default:
                                eUI.ReplaceActiveUI(false);
                                break;
                        }
                    }
                    break;
                case "Game" :
                    Time.timeScale = 1f;
                    foreach (GameEntity eUI in entitiesUI) {
                        switch (eUI.resource.name) {
                            case "InGameMenu":
                                eUI.ReplaceActiveUI(true);
                                break;
                            default:
                                eUI.ReplaceActiveUI(false);
                                break;
                        }
                    }
                    break;
                case "Death" :
                    Time.timeScale = 0f;
                    foreach (GameEntity eUI in entitiesUI) {
                        switch (eUI.resource.name) {
                            case "GameOverMenu":
                                eUI.ReplaceActiveUI(true);
                                break;
                            default:
                                eUI.ReplaceActiveUI(false);
                                break;
                        }
                    }
                    break;
                case "Reset" :
                    GameEntity[] gameEntities = _contextGame.GetEntities();
                    if (gameEntities.Length != 0) {
                        foreach (GameEntity eToDestroy in gameEntities) {
                            eToDestroy.isDestroyable = true;
                        }
                    }
    
                    InputEntity[] inputEntities = _contextInput.GetEntities();
                    if (inputEntities.Length != 0) {
                        foreach (InputEntity eToDestroy in inputEntities) {
                            eToDestroy.Destroy();
                        }
                    }
                    e.ReplaceStateEvent("Reload");
                    // The rest of destruction is done in gameController update
                    break;
            }
        }
    }
}