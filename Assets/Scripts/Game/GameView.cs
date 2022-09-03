using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button _openPanelButton;
    [SerializeField] private Button _getCoinsButton;
    [SerializeField] private GameObject _getCoinsButtonTop;
    [SerializeField] private GameObject _getCoinsButtonBottom;
    [SerializeField] private RewardsPopupView _rewardsPopupView;
    [SerializeField] private CoinHUDView _coinHUDView;
    [SerializeField] private Timer _timer;

    private readonly string _timeToUnlockKey = "RovooxTimeToUnlockKey";

    private void Start()
    {
        InitializeButtons();
        SetState();
    }

    private void InitializeButtons()
    {
        _openPanelButton.onClick.AddListener(OpenPanelButtonClicked);
        _getCoinsButton.onClick.AddListener(OnGetCoinsClicked);
    }

    private void OpenPanelButtonClicked()
    {
        Utils.PlayButtonImpact();
        _rewardsPopupView.Show(RefreshCoinHUD);
    }

    private void RefreshCoinHUD() => _coinHUDView.SetCoinCountFromPrefs();

    private void OnGetCoinsClicked()
    {
        Utils.PlayButtonImpact();
        _coinHUDView.StartCoinsAddedProduction(_getCoinsButton.transform.position, 100, false, LockButton);
    }

    private void LockButton()
    {
        var timeToUnlock = DateTime.Now.Add(new TimeSpan(0, 1, 0));
        SetLockedState(timeToUnlock);
        PlayerPrefs.SetString(_timeToUnlockKey, timeToUnlock.ToString(CultureInfo.InvariantCulture));
    }

    private void SetLockedState(DateTime timeToUnlock)
    {
        _getCoinsButtonTop.SetActive(false);
        _getCoinsButtonBottom.SetActive(false);
        _getCoinsButton.interactable = false;
        _timer.Initialize(timeToUnlock, SetUnlockedState);
    }

    private void SetUnlockedState()
    {
        _getCoinsButtonTop.SetActive(true);
        _getCoinsButtonBottom.SetActive(true);
        _getCoinsButton.interactable = true;
    }

    private void SetState()
    {
        var timeToUnlockStr = PlayerPrefs.GetString(_timeToUnlockKey);
        if (string.IsNullOrEmpty(timeToUnlockStr)) return;
        var timeToUnlock = DateTime.Parse(timeToUnlockStr);
        var timeRemaining = timeToUnlock - DateTime.Now;
        if (timeRemaining.Seconds <= 0)
            SetUnlockedState();
        else
            SetLockedState(timeToUnlock);
    }
}
