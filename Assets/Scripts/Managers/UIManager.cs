using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] PauseMenu _pauseMenu;
    [SerializeField] ScoreText _scoreText;

    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState) {
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

    public void UpdateScore(int score)
    {
        _scoreText.UpdateScore(score);
    }

}
