using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Monedas: 0";
    }

    // Update is called once per frame
    public void UpdateScore(int score)
    {
        scoreText.text = "Monedas: " + score;
    }
}
