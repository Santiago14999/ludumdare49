using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class KeyTask : Task
{
    [SerializeField] private TMP_Text _keyToPressLabel;
    [SerializeField] private Image _timerImage;
    [SerializeField] private float _timeToPress;

    private float _startTime;
    private KeyCode _keyToPress;
    private bool _timeout;

    protected override void OnCreated()
    {
        _keyToPress = (KeyCode) Random.Range(97, 123);
        _keyToPressLabel.text = _keyToPress.ToString();
        _startTime = Time.time;
    }

    private void Update()
    {
        if (_timeout) return;
        
        if (Time.time > _startTime + _timeToPress)
        {
            TaskResult(false);
            _timeout = true;
        }

        KeyCode pressedKey = KeyCode.None;
        for (int i = 97; i < 123; i++)
        {
            if (Input.GetKeyDown((KeyCode) i))
            {
                pressedKey = (KeyCode) i;
                break;
            }
        }

        if (pressedKey != KeyCode.None)
        {
            TaskResult(pressedKey == _keyToPress);
        }

        float timerImagePercentage = 1f - (Time.time - _startTime) / _timeToPress;
        _timerImage.fillAmount = timerImagePercentage;
    }
}