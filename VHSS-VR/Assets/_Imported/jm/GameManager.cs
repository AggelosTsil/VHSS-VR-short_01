using UnityEngine;
using UnityEngine.Events;

public class GameManager: MonoBehaviour {

    public static GameState DEFAULT_GAME_STATE = GameState.EXPLORING;

    public enum GameState {
        EXPLORING,
        INSPECTING
    };

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private UnityEvent gameStateSwitchEvent;

    private GameState gameState = DEFAULT_GAME_STATE;

    private static GameManager inst;

    public static GameManager GetInstance() {
        return inst;
    }

    public GameState GetGameState() {
        return gameState;
    }

    public void SetGameState(GameState gameState) {

        // TODO: checks, etc.

        this.gameState = gameState;

        switch (this.gameState) {
            case GameState.EXPLORING:
                playerManager.SaveInspectionTransforms();
                playerManager.Explore();
                break;
            case GameState.INSPECTING:
                playerManager.SaveExplorationTransforms();
                playerManager.Inspect();
                break;
        }

        gameStateSwitchEvent.Invoke();

        Debug.LogFormat("[GameManager] Switched to game state {0}...", gameState);
    }

    protected void Awake() {
        inst = this;
    }

    protected void Start() {
    }

    protected void Update() {

        switch (gameState) {

            case GameState.EXPLORING:

                break;

        }

    }
}
