using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hands : MonoBehaviour
{
    public Transform ProjectileTarget => _projectileTarget;
    [SerializeField] private Transform _projectileTarget;

    private Animator _animator;
    
    private static readonly int THROW_ANIMATION = Animator.StringToHash("Throw");
    private static readonly int CATCH_ANIMATION = Animator.StringToHash("Catch");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Catch()
    {
        _animator.Play(CATCH_ANIMATION);
    }

    public void Throw()
    {
        _animator.Play(THROW_ANIMATION);
    }
}
