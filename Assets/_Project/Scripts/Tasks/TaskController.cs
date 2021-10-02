using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private Task[] _tasks;

    public Task CreateRandomTask()
    {
        return Instantiate(_tasks[Random.Range(0, _tasks.Length)]);
    }
}
