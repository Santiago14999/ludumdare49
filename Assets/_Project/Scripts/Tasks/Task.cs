using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public event System.Action<bool> OnTaskResult;

    protected virtual void Awake()
    {
        OnTaskResult += state => Destroy(gameObject);
        OnCreated();
    }

    protected abstract void OnCreated();

    protected void TaskResult(bool result)
    {
        OnTaskResult.Invoke(result);
    }
}
