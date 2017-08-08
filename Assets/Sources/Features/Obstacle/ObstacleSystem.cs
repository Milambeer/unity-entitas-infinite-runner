using System.Collections;
using Entitas;
using UnityEngine;

public sealed class ObstacleSystem : IInitializeSystem {
    private readonly GameContext _gameContext;
    private readonly MonoBehaviour _monoBehaviourInstance;

    public ObstacleSystem(Contexts contexts, MonoBehaviour monoBehaviourInstance) {
        _gameContext = contexts.game;
        _monoBehaviourInstance = monoBehaviourInstance;
    }

    public void Initialize() {
        _monoBehaviourInstance.StartCoroutine(executeAfterTime(1));
    }

    IEnumerator executeAfterTime(float time) {
        while (true) {
            int randNumber = Random.Range(-5, 5);
            GameEntity gameEntity =  _gameContext.CreateEntity();
            // switch (randNumber) {
            //     case 1:
            //             gameEntity.isObstacle = true;
            //             gameEntity.AddResource("Obstacle");
            //             gameEntity.AddPosition(30, 5, 0);
            //             gameEntity.AddMove(0.5f);
            //         break;
            //     case 2:
            //             gameEntity.isObstacle = true;
            //             gameEntity.AddResource("Obstacle");
            //             gameEntity.AddPosition(30, -4, 0);
            //             gameEntity.AddMove(0.5f);
            //         break;
            // }
             gameEntity.isObstacle = true;
                        gameEntity.AddResource("Obstacle");
                        gameEntity.AddPosition(30, randNumber, 0);
                        gameEntity.AddMove(0.4f);
            yield return new WaitForSeconds(time);
        }
    }
}