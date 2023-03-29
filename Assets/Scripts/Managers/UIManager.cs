using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] ScoreText _scoreText;


    public void UpdateScore(int score)
    {
        _scoreText.UpdateScore(score);
    }

}
