using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private TaskController _taskController;
    [SerializeField] private GameObject _gameScene;

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

    public void StartGame()
    {
        _uiController.CloseWindows();
        _gameScene.SetActive(true);
    }

    public void EndGame()
    {
        _uiController.OpenRestartWindow();
        _gameScene.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Task task = _taskController.CreateRandomTask();
            task.OnTaskResult += OnTaskResult;
        }
    }

    private void OnTaskResult(bool result)
    {
        print(result);
    }
}
