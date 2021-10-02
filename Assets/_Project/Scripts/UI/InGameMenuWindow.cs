using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuWindow : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() => UIController.Instance.SetInGameMenuWindow(false));
        _menuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }
}
