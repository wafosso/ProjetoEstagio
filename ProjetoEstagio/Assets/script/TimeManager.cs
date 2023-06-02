using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float levelDuration = 120f; // Duração em segundos
    private float currentTime = 0f;
    private bool isTimerRunning = false;

    public TextMeshProUGUI timerText;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;

            UpdateTimerText();

            if (currentTime >= levelDuration)
            {
                FinishLevel();
            }
        }
    }

    public bool IsTimerRunning()
    {
        return isTimerRunning;
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    private void UpdateTimerText()
    {
        float timeRemaining = levelDuration - currentTime;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void FinishLevel()
    {
        isTimerRunning = false;
        SceneManager.LoadScene("GameOver");
    }
}
