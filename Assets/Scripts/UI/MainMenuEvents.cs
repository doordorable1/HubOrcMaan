using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    UIDocument _document;
    Button _button;
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("startButton");
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _button = _document.rootVisualElement.Q<Button>("settingButton");
        _button.RegisterCallback<ClickEvent>(OnSettingClick);

    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _button.UnregisterCallback<ClickEvent>(OnSettingClick);
    }
    void OnPlayGameClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    void OnSettingClick(ClickEvent evt)
    {   
        
        Debug.Log("SettingButton clicked");
    }
}
