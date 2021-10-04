using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartWindow : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() => GameController.Instance.RestartGame());
        _menuButton.onClick.AddListener(() => UIController.Instance.OpenMenuWindow());
    }
}
