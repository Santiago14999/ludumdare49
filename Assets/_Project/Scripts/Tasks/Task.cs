using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public static event System.Action<bool> OnTaskResult;

    protected virtual void Awake()
    {
        OnCreated();
    }

    protected abstract void OnCreated();

    protected void TaskResult(bool result)
    {
        Destroy(gameObject);
        OnTaskResult.Invoke(result);
    }
}
