using UnityEngine;
using TMPro;    

public class CurrentBoxCount : MonoBehaviour
{
    public TextMeshProUGUI boxCountText;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UpdateBoxCount();
    }

    void Update()
    {
        UpdateBoxCount();
    }

    void UpdateBoxCount()
    {
        if (gameManager != null && boxCountText != null)
        {
            boxCountText.text = gameManager.currentBoxCount.ToString();
        }
    }
}
