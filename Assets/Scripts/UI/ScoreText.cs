using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    public void UpdateScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore + "/1000";
    }
}
