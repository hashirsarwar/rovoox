using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        Utils.PlayButtonImpact();
        OpenGameScene();
    }

    private void OpenGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
