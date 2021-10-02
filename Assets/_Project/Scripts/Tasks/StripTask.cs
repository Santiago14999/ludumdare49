using UnityEngine;
using UnityEngine.UI;

public class StripTask : Task
{
    [SerializeField] private Image _targetZone;
    [SerializeField] private Image _cursor;
    [SerializeField] private float _cursorSpeed;
    [SerializeField] private float _maxX;

    private Vector2 _targetZoneBorders;
    private float _startTime;
    
    protected override void OnCreated()
    {
        _cursor.rectTransform.anchoredPosition = new Vector2(-_maxX + _cursor.rectTransform.sizeDelta.x * .5f, 0);
        float targetZoneX = Random.Range(-_maxX + _targetZone.rectTransform.sizeDelta.x * .5f,
            _maxX - _targetZone.rectTransform.sizeDelta.x * .5f);

        _targetZone.rectTransform.anchoredPosition = new Vector2(targetZoneX, 0);
        _targetZoneBorders = new Vector2(targetZoneX - _targetZone.rectTransform.sizeDelta.x * .5f,
            targetZoneX + _targetZone.rectTransform.sizeDelta.x * .5f);

        _startTime = Time.time;
    }

    private void Update()
    {
        Vector2 pos = _cursor.rectTransform.anchoredPosition;
        pos.x = Mathf.PingPong((Time.time - _startTime) * _cursorSpeed * 100f, _maxX * 2) - _maxX;
        _cursor.rectTransform.anchoredPosition = pos;

        if (InputExtension.AnyKeyDown())
        {
            Submit();
        }
    }

    private void Submit()
    {
        float x = _cursor.rectTransform.anchoredPosition.x;
        TaskResult(x > _targetZoneBorders.x && x < _targetZoneBorders.y);
    }
}
