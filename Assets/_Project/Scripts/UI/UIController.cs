using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _inGameMenuWindow;
    [SerializeField] private GameObject _restartWindow;
    
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIController>();

            return _instance;
        }
    }
    private static UIController _instance;

    public void OpenMenuWindow()
    {
        _menuWindow.SetActive(true);
        _restartWindow.SetActive(false);
    }

    public void SetInGameMenuWindow(bool state)
    {
        _inGameMenuWindow.SetActive(state);
    }

    public void OpenRestartWindow()
    {
        _menuWindow.SetActive(false);
        _restartWindow.SetActive(true);
    }

    public void CloseWindows()
    {
        _menuWindow.SetActive(false);
        _restartWindow.SetActive(false);
    }
}
