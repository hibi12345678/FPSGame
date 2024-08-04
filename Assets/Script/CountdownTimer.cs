using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    
    public float totalTime = 10f;  // カウントダウンする時間（秒）

    public  float timeRemaining;

    public int team1 = 1;
    public int team2 = 0;
    // UI Textへの参照
    public Text uiText;
    // テキストの色を変更するための変数
    private Color textColor = Color.white;
    public
    void Start()
    {
        timeRemaining = totalTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0) timeRemaining = 0;
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        if (seconds < 10)
        {
            // 変数の値をテキストに表示
            uiText.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            // 変数の値をテキストに表示
            uiText.text = minutes.ToString() + ":" + seconds.ToString();
        }

        // 条件に応じて色を変更する例
        if (timeRemaining <= 10)
        {
            uiText.color = Color.red;
        }
        else
        {
            uiText.color = Color.white;
        }

    }
}