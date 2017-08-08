using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class AddViewSystem : ReactiveSystem<GameEntity> {
    private readonly GameContext _gameContext;
    private Transform _viewContainer;

    public AddViewSystem(Contexts contexts) : base(contexts.game) {
        _gameContext = contexts.game;
        _viewContainer = new GameObject("Views").transform;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Resource);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasResource;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities) {
            var res = Resources.Load<GameObject>(e.resource.name);
            GameObject gameObject = null;
            try {
                gameObject = UnityEngine.Object.Instantiate(res);

            } catch(Exception) {
                Debug.Log("Cannot instanciate" + e.resource.name);
            }

            if (gameObject != null) {
                gameObject.transform.SetParent(_viewContainer);
                gameObject.Link(e, _gameContext);
                e.AddView(gameObject);
            }
        }
    }
}