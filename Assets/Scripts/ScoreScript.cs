using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private string textContent;
    private TextMeshProUGUI text;

    private uint pScore = 0, gScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        textContent = $"SCORE\nPlayer:{pScore}\nGoalie:{gScore}";
    }

    // Update is called once per frame
    void Update()
    {
        pScore = GameObject.Find("Soccer Ball").GetComponent<BallController>().pScore;
        gScore = GameObject.Find("Soccer Ball").GetComponent<BallController>().gScore;
        textContent = $"SCORE\nPlayer:{pScore}\nGoalie:{gScore}";
        text.text = textContent;
    }
}
