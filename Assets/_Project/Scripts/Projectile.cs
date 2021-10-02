using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public event System.Action Catched;
    
    [SerializeField] private float _transitionTime;
    
    private Sequence _moveSequence;

    public void MoveToNextTarget(Transform nextTarget)
    {
        if (_moveSequence != null && _moveSequence.active)
        {
            _moveSequence.Kill();
        }

        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMove(nextTarget.position, _transitionTime));
        _moveSequence.OnComplete(() => Catched?.Invoke());
    }
}
