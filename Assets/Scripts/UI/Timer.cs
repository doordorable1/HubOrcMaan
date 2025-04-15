using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    
    public float roundTime = 60f;
    private float timeRemaining;
    public UnityEvent OnTimeUp; // Event fired when time is up

    private bool feverTriggered = false;

    [Header("UI Timer")]
    [SerializeField] private TextMeshProUGUI timerText; // Reference to a UI Text element to display the timer

    void OnEnable()
    {
        timeRemaining = roundTime;
        feverTriggered = false;
        UpdateTimerUI();
    }
    

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        
        UpdateTimerUI();

        if (!feverTriggered && timeRemaining <= 20f && timeRemaining >= 10f)
        {
            feverTriggered = true;
            // Trigger fever time for 10 seconds
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartFeverTime();
                Invoke("EndFever", 10f);
            }
        }
        
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            UpdateTimerUI();
            OnTimeUp?.Invoke();
            enabled = false; // Stop the timer
            GameManager.Instance.OnRoundTimeUp();
        }
    }

    void EndFever()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndFeverTime();
        }
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
        }
    }
    

}