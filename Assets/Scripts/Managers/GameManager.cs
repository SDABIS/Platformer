using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Declarations

    public enum GameState {
        PREGAME,
        RUNNING,
        PAUSED
    }
    private GameState _currentGameState = GameState.RUNNING;
    public GameState CurrentGameState {
        get {
            return _currentGameState;
        }

        private set {
            _currentGameState = value;
        }
    }
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> {}
    public EventGameState OnGameStateChanged;

    [SerializeField] GameObject[] SystemPrefabs;
    
    private List<GameObject> _instantiatedSystemPrefabs;
    private List<AsyncOperation> _loadOperations;
    private static GameManager instance;

    private string _currentLevel = string.Empty;

    #endregion
    
    #region Start Update

    private void Start() {
        DontDestroyOnLoad(gameObject);

        _loadOperations = new List<AsyncOperation>();
        _instantiatedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();


    }

    private void Update() {

        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME) return;
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.TogglePause();
        }
    }

    #endregion

    private void InstantiateSystemPrefabs() {

        GameObject prefabInstance;
        for(int i = 0; i<SystemPrefabs.Length; i++) {
            prefabInstance = Instantiate(SystemPrefabs[i]);

            _instantiatedSystemPrefabs.Add(prefabInstance);
        }
    }

    #region Load

    private void OnLoadOperationComplete(AsyncOperation ao) {
        if(_loadOperations.Contains(ao)) {
            _loadOperations.Remove(ao);

            if(_loadOperations.Count == 0) UpdateState(GameState.RUNNING);
        }
        Debug.Log("Load Complete");
    }

    private void OnUnloadOperationComplete(AsyncOperation ao) {
        Debug.Log("Unload Complete");
    }

    public void LoadLevel(string levelName) {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if(ao == null) {
            Debug.LogError("Unable to load level " + levelName + ".");
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevel = levelName;
    }

    public void UnloadLevel(string levelName) {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if(ao == null) {
            Debug.LogError("Unable to unload level " + levelName + ".");
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    #endregion

    private void UpdateState(GameState state) { 
        GameState previousGameState = _currentGameState;
        _currentGameState = state;
        switch(_currentGameState) {
            case GameState.PREGAME:
                Time.timeScale = 1;
            break;
            case GameState.RUNNING:
                Time.timeScale = 1;
            break;
            
            case GameState.PAUSED:
                Time.timeScale = 0;
            break;
            
            default:
            break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i<SystemPrefabs.Length; i++) {

            Destroy(_instantiatedSystemPrefabs[i]);
        }

        _instantiatedSystemPrefabs.Clear();

    }

    public void StartGame() {
        LoadLevel("Game");
    }

    public void TogglePause(){
        if(_currentGameState == GameState.RUNNING) {
            UpdateState(GameState.PAUSED);
        }
        else {
            UpdateState(GameState.RUNNING);
        }
    }

    public void RestartGame() {
        UpdateState(GameState.PREGAME);
    }
}
