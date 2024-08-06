using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public GameObject defeat;
    public GameObject victory;
    public GameObject draw;
    GameObject timer;
    // é©ï™é©êgÇÃÉ^ÉOÇéÊìæ
    string myTag;
    float timeRem;
    private CountdownTimer countdownTimer; 
    int team1;
    int team2;
    public Text uiText1;
    public Text uiText2;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer");
        
        myTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer = timer.GetComponent<CountdownTimer>();
        timeRem = countdownTimer.timeRemaining;
        team1 = countdownTimer.team1;
        team2 = countdownTimer.team2;

        if (myTag == "Team1")
        {
            uiText1.text = team1.ToString();
            uiText2.text = team2.ToString();
            if (timeRem == 0)
            {
                if (team1 > team2)
                {
                    victory.gameObject.SetActive(true);
                }
                else if(team1 < team2)
                {
                    defeat.gameObject.SetActive(true);
                }
                else
                {
                    draw.gameObject.SetActive(true);
                }
            }
        }
        else if(myTag == "Team2")
        {
            uiText2.text = team1.ToString();
            uiText1.text = team2.ToString();
            if (timeRem == 0)
            {
                if (team1 > team2)
                {
                    defeat.gameObject.SetActive(true);
                }
                else if (team1 < team2)
                {
                    
                    victory.gameObject.SetActive(true);
                }
                else
                {
                    draw.gameObject.SetActive(true);
                }
            }
        }

    }
}
