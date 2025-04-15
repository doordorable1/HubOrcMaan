using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentBoxCount = 0;

    public bool isFeverTime = false;
    public float feverMoveSpeedMultiplier = 1.5f;
    public float feverDashCooldownMultiplier = 0.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartFeverTime()
    {
        isFeverTime = true;
        Debug.Log("Fever Time started!");
    }
    public void EndFeverTime()
    {
        isFeverTime = false;
        Debug.Log("Fever Time ended!");
    }
    public void AddBox()
    {
        currentBoxCount++;
        if (currentBoxCount > 19)
        {
            GameOver();
        }
    }
    public void RemoveBox()
    {
        currentBoxCount--;
    }

    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
        currentBoxCount = 0;
    }

    public void OnRoundTimeUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameClearMenu");
    }
}
