using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private TaskController _taskController;
    [SerializeField] private GameObject _gameScene;
    [SerializeField] private ThrowController _throwController;

    public event System.Action GameOver;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();

            return _instance;
        }
    }
    private static GameController _instance;

    private void Awake()
    {
        Task.OnTaskResult += OnTaskResult;
    }

    public void StartGame()
    {
        _uiController.CloseWindows();
        _gameScene.SetActive(true);
        _throwController.InitializeProjectile();
        _taskController.CreateRandomTask();
    }

    public void EndGame()
    {
        _uiController.OpenRestartWindow();
        _gameScene.SetActive(false);
        GameOver?.Invoke();
    }

    private void OnTaskResult(bool result)
    {
        if (result)
        {
            _throwController.ThrowToNext();
            _taskController.CreateRandomTask();
        }
        else
        {
            EndGame();
        }
    }
}
