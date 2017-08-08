using UnityEngine;
using Entitas;

public class MenuEmitter : MonoBehaviour {
	public void PauseButtonAction() {
		IGroup<GameEventEntity> _groupGameEvent = Contexts.sharedInstance.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));
		GameEventEntity[] entitiesGameEvent = _groupGameEvent.GetEntities();
		GameEventEntity entityGameEvent = entitiesGameEvent[0];
		entityGameEvent.ReplaceStateEvent("Pause");
	}

	public void ContinueButtonAction() {
		IGroup<GameEventEntity> _groupGameEvent = Contexts.sharedInstance.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));
		GameEventEntity[] entitiesGameEvent = _groupGameEvent.GetEntities();
		GameEventEntity entityGameEvent = entitiesGameEvent[0];
		entityGameEvent.ReplaceStateEvent("Game");
	}

	public void ResetButtonAction() {
		IGroup<GameEventEntity> _groupGameEvent = Contexts.sharedInstance.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));
		GameEventEntity[] entitiesGameEvent = _groupGameEvent.GetEntities();
		GameEventEntity entityGameEvent = entitiesGameEvent[0];
		entityGameEvent.ReplaceStateEvent("Reset");
	}

	public void QuitButtonAction() {
		Application.Quit();
	}
}