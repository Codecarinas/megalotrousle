using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

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
		kEnemyTurnIntro,
		kEnemyTurn,
		kCutscene,
		kMenu
	}

	[SerializeField] private GameState _currentState;
	[SerializeField] private BattleState _currentBattleState;
	[SerializeField] private Text _playerTimeRemaining;
	public float timeRemaining;

	public UnityEvent playerTurnCallbacks = new UnityEvent();
	public UnityEvent menuTurnCallbacks = new UnityEvent();
	public UnityEvent enemyTurnCallbacks = new UnityEvent();
	public UnityEvent enemyTurnIntroCallbacks = new UnityEvent();

	public List<Enemy> enemies;

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
			if (_currentBattleState == BattleState.kPlayerTurn) {
				NextBattleState (BattleState.kEnemyTurnIntro);
			} else if (_currentBattleState == BattleState.kEnemyTurn) {
				NextBattleState (BattleState.kMenu);
			}

		} else if (_currentBattleState == BattleState.kPlayerTurn || _currentBattleState == BattleState.kEnemyTurn) {
			timeRemaining -= Time.deltaTime;
		}
	}

	public void NextBattleState(BattleState state) {
		switch (state) {
		case BattleState.kMenu:
			menuTurnCallbacks.Invoke ();
			NotificationCenter.defaultInstance.PostNotification ("GameState.PlayerMenu");
			break;
		case BattleState.kEnemyTurnIntro:
			enemyTurnIntroCallbacks.Invoke ();
			NotificationCenter.defaultInstance.PostNotification ("GameState.EnemyAttack.Intro");
			break;
		case BattleState.kEnemyTurn:
			enemyTurnCallbacks.Invoke ();
			NotificationCenter.defaultInstance.PostNotification ("GameState.EnemyAttack");
			break;
		case BattleState.kPlayerTurn:
			playerTurnCallbacks.Invoke ();
			NotificationCenter.defaultInstance.PostNotification ("GameState.PlayerAttack");
			break;
		default:
			break;
		}

		_currentBattleState = state;
	}

	public void PlayerFight(int idx) {
		NextBattleState (BattleState.kPlayerTurn);
		timeRemaining = 30;
	}

	public void EnemyFight(float time) {
		NextBattleState (BattleState.kEnemyTurn);
		timeRemaining = time;
	}

	public void EnemyIntro() {
		NextBattleState (BattleState.kEnemyTurnIntro);
	}

	public void PlayerAct() {
		EnemyIntro ();
	}
}
