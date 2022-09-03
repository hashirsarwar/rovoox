using System;
using TMPro;
using UnityEngine;

public sealed class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sessionTimeText;
    [SerializeField] private Animator _timerAnimator;

    private float _timerInSeconds;
    private bool _timerExpired = true;
    private Action _onTimerExpired;
    
    public void Initialize(DateTime endTime, Action onTimerExpired)
    {
        gameObject.SetActive(true);
        var span = endTime - DateTime.Now;
        _timerInSeconds = span.Seconds;
        _onTimerExpired = onTimerExpired;
        _timerAnimator.enabled = true;
        _timerExpired = false;
    }

    private void Update()
    {
        if (_timerExpired) return;
        _timerInSeconds -= Time.deltaTime;
        _sessionTimeText.text = _timerInSeconds.ToString("0") + "s";

        if (_timerInSeconds <= 0)
        {
            _sessionTimeText.text = "0s";
            _timerExpired = true;
            _timerAnimator.enabled = false;
            _onTimerExpired.Invoke();
        }
    }
}
