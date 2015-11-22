using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class GameStateController : MonoBehaviour {

	static private GameStateController _instance = null;

	static public GameStateController Instance {
		get {
			if (_instance == null) {
				_instance = Camera.main.gameObject.AddComponent<GameStateController> ();
			}

			return _instance;
		}
	}

	public enum GameState {
		kOverworld = 0,
		kCutscene,
		kFight
	};

	public enum BattleState {
		kPlayerTurn = 0,
		kEnemyTurn,
		kCutscene,
		kMenu
	}

	[SerializeField] private GameState _currentState;
	[SerializeField] private BattleState _currentBattleState;
	[SerializeField] private Text _playerTimeRemaining;
	public float timeRemaining;

	[SerializeField] private UnityEvent _playerTurnCallbacks = new UnityEvent();
	[SerializeField] private UnityEvent _menuTurnCallbacks = new UnityEvent();

	public GameState gameState {
		get {
			return _currentState;
		}
	}

	public BattleState battleState {
		get {
			return _currentBattleState;
		}
	}

	// Use this for initialization
	void Start () {
		if (_instance == null) {
			_instance = this;
		}

		if (_instance != this) {
			Debug.Log ("Duplicate GameStateController detected, destroying.");
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		_playerTimeRemaining.text = "TIME REMAINING: " + timeRemaining.ToString ("F2");

		if (timeRemaining < 0 && _currentBattleState != BattleState.kMenu) {
			timeRemaining = 0;
			_currentBattleState = BattleState.kMenu;
			_menuTurnCallbacks.Invoke ();
		} else if (_currentBattleState == BattleState.kPlayerTurn) {
			timeRemaining -= Time.deltaTime;
		}
	}

	public void PlayerFight(int enemyIdx) {
		_currentBattleState = BattleState.kPlayerTurn;
		_playerTurnCallbacks.Invoke ();
		timeRemaining = 30;
	}
}
