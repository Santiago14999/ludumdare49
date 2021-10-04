using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private TaskController _taskController;
    [SerializeField] private GameObject _gameScene;
    [SerializeField] private ThrowController _throwController;
    [SerializeField] private GameObject _handsPrefab;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private TMP_Text _scoreText;

    private int _currentScore;
    private int _lives;
    
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
        _lives = 4;
        _scoreText.transform.parent.gameObject.SetActive(true);
        _scoreText.text = "0";
        _currentScore = 0;
        _throwController.InitializeProjectile();
        _taskController.CreateRandomTask();
    }

    public void RestartGame()
    {
        _throwController.InitHands(Instantiate(_handsPrefab), Instantiate(_projectilePrefab));
        StartGame();
    }

    public void EndGame()
    {
        _scoreText.transform.parent.gameObject.SetActive(false);
        _uiController.OpenRestartWindow();
        GameOver?.Invoke();
    }

    private void OnTaskResult(bool result)
    {
        if (result)
        {
            _throwController.ThrowToNext();
            _currentScore++;
            _scoreText.text = _currentScore.ToString();
            Invoke(nameof(CreateNextTask), 1f);
        }
        else
        {
            OnFail();
        }
    }

    private void CreateNextTask()
    {
        _taskController.CreateRandomTask();
    }

    private void OnFail()
    {
        if (_lives < 0)
            return;
            
        _throwController.DestroyNextHands();
        _lives--;
        if (_lives == 0)
        {
            Invoke(nameof(EndGame), 2f);
        }
        else
        {
            Invoke(nameof(CreateNextTask), 1f);
        }
    }
}
