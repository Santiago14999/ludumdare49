using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public static event System.Action<bool> OnTaskResult;

    [SerializeField] private AudioClip _soundEffect;

    protected virtual void Awake()
    {
        OnCreated();
    }

    protected abstract void OnCreated();

    protected void TaskResult(bool result)
    {
        GameObject go = new GameObject();
        var sound = go.AddComponent<AudioSource>();
        go.AddComponent<VFXDestroy>();
        sound.clip = _soundEffect;
        sound.Play();
        Destroy(gameObject);
        OnTaskResult.Invoke(result);
    }
}
