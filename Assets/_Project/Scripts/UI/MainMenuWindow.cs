using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() => GameController.Instance.StartGame());
        _exitButton.onClick.AddListener(Application.Quit);
    }
}
