using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] PauseMenu _pauseMenu;
    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState) {
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

}
