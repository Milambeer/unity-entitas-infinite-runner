using UnityEngine;
using UnityEngine.SceneManagement;
using Entitas;
using Entitas.VisualDebugging.Unity;

public class GameController : MonoBehaviour {

	private Systems _systems;
	private IGroup<GameEventEntity> _groupGameEvent;
	private Contexts _contexts;

	void Start () {
		_contexts = Contexts.sharedInstance;

		_systems = createSystems(_contexts);
		_systems.Initialize();
		_groupGameEvent = _contexts.gameEvent.GetGroup(Matcher<GameEventEntity>.AllOf(GameEventMatcher.StateEvent));
	}

	void Update() {
		bool isReload = false;
		GameEventEntity gameEventEntity = null;
		GameEventEntity[] entitiesGameEvent = _groupGameEvent.GetEntities();
		if (entitiesGameEvent.Length != 0) {
			gameEventEntity = entitiesGameEvent[0];
			if (gameEventEntity.stateEvent.state == "Reload Ready") {
				isReload = true;
			}	
		}	

		if (isReload) {
			gameEventEntity.Destroy();
 			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else {
			_systems.Execute();
		}
	}
	
	private Systems createSystems(Contexts contexts) {
		Systems systems;
		#if (UNITY_EDITOR)
			systems = new DebugSystems("Editor");
		#else
			systems = new Systems();
		#endif
			systems
				// Initialize
				.Add(new WorldCreationSystem(contexts))
				.Add(new PlayerCreationSystem(contexts))
				.Add(new ObstacleSystem(contexts, this))
				.Add(new UICreationSystem(contexts))
				.Add(new EventCreationSystem(contexts))
				// Reactive
				.Add(new AddViewSystem(contexts))
				.Add(new PositionSystem(contexts))
				.Add(new InputSystem(contexts))
				.Add(new CollisionSystem(contexts))
				// .Add(new JumpSystem(contexts))
				.Add(new DestroySystem(contexts))
				.Add(new ActiveUISystem(contexts))
				.Add(new GameEventSystem(contexts))
				.Add(new DeathSystem(contexts))
				.Add(new PlayerPhysicSystem(contexts))
				// Execute
				.Add(new MoveSystem(contexts));

			return systems;
	}
}
