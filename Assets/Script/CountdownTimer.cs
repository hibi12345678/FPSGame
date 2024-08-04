using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    
    public float totalTime = 10f;  // �J�E���g�_�E�����鎞�ԁi�b�j

    public  float timeRemaining;

    public int team1 = 1;
    public int team2 = 0;
    // UI Text�ւ̎Q��
    public Text uiText;
    // �e�L�X�g�̐F��ύX���邽�߂̕ϐ�
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
            // �ϐ��̒l���e�L�X�g�ɕ\��
            uiText.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            // �ϐ��̒l���e�L�X�g�ɕ\��
            uiText.text = minutes.ToString() + ":" + seconds.ToString();
        }

        // �����ɉ����ĐF��ύX�����
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