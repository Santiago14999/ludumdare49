using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExpressionTask : Task
{
    private enum Operator {Add, Sub}
    
    [SerializeField] private TMP_Text _expression;
    [SerializeField] private Image _timer;
    [SerializeField] private float _timeToSolve;

    private string _currentExpression;
    private string _currentAnswer;
    private int _targetNumber;
    private Operator _operator;

    private float _startTime;
    
    protected override void OnCreated()
    {
        _currentExpression = _expression.text;
        _targetNumber = Random.Range(2, 20);
        int leftOperand, rightOperand;

        leftOperand = Random.Range(1, 20);
        leftOperand = (_targetNumber + leftOperand) % 20;
        if (leftOperand == 0)
            leftOperand = 1;

        if (leftOperand > _targetNumber)
        {
            _operator = Operator.Sub;
            rightOperand = leftOperand - _targetNumber;
        }
        else
        {
            _operator = Operator.Add;
            rightOperand = _targetNumber - leftOperand;
        }

        _currentExpression = leftOperand + (_operator == Operator.Add ? " + " : " - ") + rightOperand + " = ";
        _expression.text = _currentExpression;
        _currentAnswer = "";
        _startTime = Time.time;
    }

    private void Update()
    {
        UpdateTimer();
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (_currentAnswer.Length > 0)
            {
                _currentAnswer = _currentAnswer.Remove(_currentAnswer.Length - 1);
                UpdateExpression();
            }

            return;
        }
        
        KeyCode number = InputExtension.FirstNumberDown();
        if (number == KeyCode.None || _currentAnswer.Length >= 3)
            return;

        if (_currentAnswer.Length == 0 && number == KeyCode.Alpha0)
            return;
        
        _currentAnswer += (int) number - (int) KeyCode.Alpha0;
        UpdateExpression();
        HandleAnswer(int.Parse(_currentAnswer));
    }

    private void UpdateTimer()
    {
        float percentage = 1f - (Time.time - _startTime) / _timeToSolve;
        _timer.fillAmount = Mathf.Max(percentage, 0);

        if (percentage <= 0f)
        {
            TaskResult(false);
        }
    }

    private void UpdateExpression()
    {
        _expression.text = _currentExpression + _currentAnswer;
    }

    private void HandleAnswer(int answer)
    {
        if (answer == _targetNumber)
        {
            TaskResult(true);
        }
    }
}
