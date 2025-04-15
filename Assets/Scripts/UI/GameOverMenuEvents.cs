using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenuEvents : MonoBehaviour
{
    UIDocument _document;
    Button _button;
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("restartButton");
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }

    void OnPlayGameClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    
}
