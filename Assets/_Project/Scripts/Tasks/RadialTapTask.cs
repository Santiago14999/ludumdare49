using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RadialTapTask : Task
{
    [SerializeField] private Button _tapButton;
    [SerializeField] private Image _timer;
    [SerializeField] private float _timeToTap;
    [SerializeField] private Image _greenZone;
    [SerializeField] private Image _cursor;
    [SerializeField] private Vector2 _minMaxGreenZoneAngle;
    [SerializeField] private float _cursorRegressSpeed;
    [SerializeField] private float _tapStrength;
    [SerializeField] private float _fillSpeed;
    [SerializeField] private Image _fillBar;

    private float _startTime;
    private float _fillPercentage;
    private float _greenZoneRotation;
    private float _cursorCurrentRotation;
    
    protected override void OnCreated()
    {
        _startTime = Time.time;
        _tapButton.onClick.AddListener(Tap);
        _greenZoneRotation = Random.Range(_minMaxGreenZoneAngle.x, _minMaxGreenZoneAngle.y);
        _greenZone.rectTransform.eulerAngles = Vector3.forward * _greenZoneRotation;
        _cursorCurrentRotation = 180f;
    }

    private void Tap()
    {
        float rotation = _cursorCurrentRotation - _tapStrength;
        _cursorCurrentRotation = Mathf.Max(3f, rotation);
    }

    private void Update()
    {
        float rotation = _cursorCurrentRotation + Time.deltaTime * _cursorRegressSpeed;
        _cursorCurrentRotation = Mathf.Min(180f, rotation);

        _cursor.rectTransform.eulerAngles = Vector3.forward * _cursorCurrentRotation;
        if (_cursorCurrentRotation <= _greenZoneRotation && _cursorCurrentRotation > _greenZoneRotation - 8f)
            FillBar();
        
        UpdateTimer();
    }

    private void FillBar()
    {
        _fillPercentage += Time.deltaTime * _fillSpeed;
        _fillBar.fillAmount = Mathf.Min(_fillPercentage, 1f);
        if (_fillPercentage >= 1f)
        {
            TaskResult(true);
        }
    }

    private void UpdateTimer()
    {
        float percentage = 1f - (Time.time - _startTime) / _timeToTap;
        _timer.fillAmount = Mathf.Max(percentage, 0f);
        
        if (percentage <= 0f)
            TaskResult(false);
    }
}
