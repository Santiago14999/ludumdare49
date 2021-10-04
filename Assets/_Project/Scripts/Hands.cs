using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hands : MonoBehaviour
{
    public Transform ProjectileTarget => _projectileTarget;
    [SerializeField] private Transform _projectileTarget;
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private ParticleSystem _correctVFX;

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
        Instantiate(_correctVFX, _projectileTarget.position, Quaternion.identity);
        _animator.Play(THROW_ANIMATION);
    }

    public void DestroyHands()
    {
        Instantiate(_explosionVFX, _projectileTarget.position, Quaternion.identity);
        Destroy(gameObject, .5f);
    }

    public void PlayHold()
    {
        _animator.Play("Hold");
    }
}
