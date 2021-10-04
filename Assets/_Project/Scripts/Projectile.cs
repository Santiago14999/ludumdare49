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

        if (nextTarget == null)
        {
            MoveUp();
            return;
        }
        
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMove(nextTarget.position, _transitionTime));
        _moveSequence.OnComplete(() => Catched?.Invoke());
    }

    private void MoveUp()
    {
        Vector3 defaultPosition = transform.position;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMove(transform.position + Vector3.up * 2f, _transitionTime * .5f));
        _moveSequence.Append(transform.DOMove(defaultPosition, _transitionTime * .5f));
        _moveSequence.OnComplete(() => Catched?.Invoke());
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
