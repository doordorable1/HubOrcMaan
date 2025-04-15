using UnityEngine;
using UnityEngine.Events;

public class FeverTimer : MonoBehaviour
{
    public float feverDuration = 10f;
    private float timeRemaining;
    public UnityEvent OnFeverTimeEnded;

    void OnEnable()
    {
        timeRemaining = feverDuration;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            OnFeverTimeEnded?.Invoke();
            enabled = false;
        }
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
