using System.Collections.Generic;
using Entitas;

public sealed class ActiveUISystem : ReactiveSystem<GameEntity> {

    public ActiveUISystem(Contexts contexts) : base(contexts.game) {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.ActiveUI);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasActiveUI;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach(var e in entities) {
            bool isActiveUI = e.activeUI.isActive;
            if (isActiveUI) {
                e.view.gameObject.SetActive(true);
            } else {
                e.view.gameObject.SetActive(false);
            }
        }
    }
}
