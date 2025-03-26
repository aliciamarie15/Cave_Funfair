using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int Punkte = 0;
    public int maxPunkte = 10;
    public int Becher = 0;
    public int Baelle = 0;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Punkte >= maxPunkte)
        {
            ShowEndScore();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Becher: " + Punkte + " | Score: " + Punkte;
    }
    
    public void ShowEndScore()
    {
        scoreText.text = "Endscore: " + Punkte + " Becher und " + Baelle + " Bälle verwendet.";
    }
}
