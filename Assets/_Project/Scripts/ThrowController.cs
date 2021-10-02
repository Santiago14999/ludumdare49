using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private Hands[] _hands;
    [SerializeField] private Projectile _projectile;

    private int _currentHands = 0;

    private void Awake()
    {
        _projectile.Catched += () => _hands[_currentHands].Catch();
    }

    public void ThrowToNext()
    {
        _hands[_currentHands].Throw();
        _currentHands = (_currentHands + 1) % _hands.Length;
        _projectile.MoveToNextTarget(_hands[_currentHands].ProjectileTarget);
    }

    public void InitializeProjectile()
    {
        _projectile.transform.position = _hands[0].ProjectileTarget.position;
    }
}
