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
    [SerializeField] private AudioSource _explosion;
    [SerializeField] private AudioSource _music;

    private int _currentScore;
    private int _lives;
    private bool _firstGame;

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
        _firstGame = true;
        Task.OnTaskResult += OnTaskResult;
    }

    public void StartGame()
    {
        if (_firstGame)
        {
            _firstGame = false;
        }
        else
        {
            _throwController.InitHands(Instantiate(_handsPrefab), Instantiate(_projectilePrefab));
        }
        Time.timeScale = 1f;
        _uiController.CloseWindows();
        _gameScene.SetActive(true);
        _lives = 4;
        _scoreText.transform.parent.gameObject.SetActive(true);
        _scoreText.text = "0";
        _currentScore = 0;
        _throwController.InitializeProjectile();
        _taskController.CreateRandomTask();
    }

    public void EndGame()
    {
        Time.timeScale = 1f;
        _uiController.OpenRestartWindow();
        _music.Stop();
        _music.Play();
        GameOver?.Invoke();
    }

    private void OnTaskResult(bool result)
    {
        if (result)
        {
            Time.timeScale += .1f / Mathf.Pow(Mathf.Max(1f, _currentScore), .8f);
            print(Time.timeScale);
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

    public void HideScore()
    {
        _scoreText.transform.parent.gameObject.SetActive(false);
    }

    private void CreateNextTask()
    {
        _taskController.CreateRandomTask();
    }

    private void OnFail()
    {
        if (_lives < 0)
            return;
            
        _explosion.Play();
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
