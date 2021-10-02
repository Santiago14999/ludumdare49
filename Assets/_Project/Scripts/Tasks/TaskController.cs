using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private Task[] _tasks;
    
    private Task _lastTask;

    private void Awake()
    {
        GameController.Instance.GameOver += DestroyLastTask;
    }

    private void DestroyLastTask()
    {
        if (_lastTask != null)
            Destroy(_lastTask.gameObject);
    }

    public Task CreateRandomTask()
    {
        _lastTask = Instantiate(_tasks[Random.Range(0, _tasks.Length)]);
        return _lastTask;
    }
}
