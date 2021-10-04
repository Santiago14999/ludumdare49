using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private List<Hands> _hands;
    [SerializeField] private Projectile _projectile;

    private int _currentHands = 0;

    private void Catch()
    {
        _hands[_currentHands].Catch();
    }

    public void ThrowToNext()
    {
        Hands hands = _hands[_currentHands];
        _hands[_currentHands].Throw();
        _currentHands = (_currentHands + 1) % _hands.Count;
        Transform target = _hands[_currentHands] == hands ? null : _hands[_currentHands].ProjectileTarget;
        _projectile.MoveToNextTarget(target);
    }

    public void InitializeProjectile()
    {
        _projectile.transform.position = _hands[0].ProjectileTarget.position;
        _projectile.Catched += Catch;
        _hands[0].PlayHold();
    }

    public void DestroyNextHands()
    {
        _hands[_currentHands].DestroyHands();
        _hands.RemoveAt(_currentHands);
        if (_hands.Count != 0)
        {
            _currentHands %= _hands.Count;
            _projectile.MoveToNextTarget(_hands[_currentHands].ProjectileTarget);
        }
        else
        {
            _projectile.DestroyProjectile();
        }
    }

    public void InitHands(GameObject prefab, Projectile projectile)
    {
        _projectile = projectile;
        _hands = prefab.GetComponentsInChildren<Hands>().ToList();
        _currentHands = 0;
    }
}
