using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CounterTapTask : Task
{
    [SerializeField] private Vector2 _minMaxTapCounter;
    [SerializeField] private Image _timer;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private float _timeToTap;
    [SerializeField] private Button _tapButton;

    private float _startTime;
    private int _tapCounter;
    
    protected override void OnCreated()
    {
        _startTime = Time.time;
        _tapCounter = Random.Range((int) _minMaxTapCounter.x, (int) _minMaxTapCounter.y);
        _counter.text = _tapCounter.ToString();
        _tapButton.onClick.AddListener(Tap);
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        float percentage = 1f - (Time.time - _startTime) / _timeToTap;
        _timer.fillAmount = Mathf.Max(percentage, 0f);
        
        if (percentage <= 0f)
            TaskResult(false);
    }

    private void Tap()
    {
        _tapCounter--;
        _counter.text = _tapCounter.ToString();
        if (_tapCounter == 0)
        {
            TaskResult(true);
        }
    }
}
