using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputSystem : IExecuteSystem {
    private readonly GameContext _contextGame;
    private readonly InputContext _contextInput;
    private GameEntity _player;
    private float _initTimeButtonDownEvent;
    private InputEntity _stableEntity;

    public InputSystem(Contexts contexts) {
        _contextGame = contexts.game;
        _contextInput = contexts.input;
        _initTimeButtonDownEvent = 0f;
    }
    public void Execute() {
        if (!IsPointerOverUIObject()) {
            if (_player == null) {
                GameEntity[] entities = _contextGame.GetEntities(GameMatcher.Player);
                _player = entities[0];
            }
            InputEntity[] e = _contextInput.GetEntities(Matcher<InputEntity>.AllOf(InputMatcher.Death));
            if (e.Length == 0) {
                if (Input.GetMouseButtonDown(0)){
                    _initTimeButtonDownEvent = Time.time;
                    _player.ReplacePlayerPhysic(false, true);
                }

                if (Input.GetMouseButtonUp(0) && _initTimeButtonDownEvent != 0) {
                    if (_stableEntity != null) {
                        _stableEntity.Destroy();
                        _stableEntity = null;
                    }
                    if ((Time.time - _initTimeButtonDownEvent) < 0.15) {
                        _player.ReplacePlayerPhysic(true, false);
                    } else {
                        _player.ReplacePlayerPhysic(false, false);
                    }
                    _initTimeButtonDownEvent = 0f;
                }
            }
        }
    }

    // Check if pointer if mouse is over a ui button
     private bool IsPointerOverUIObject() {
         var eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current)
         {
             position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
         };
         var results = new List<RaycastResult>();
         EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
         return results.Count > 0;
     }
}