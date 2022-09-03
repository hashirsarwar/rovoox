using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardsPopupView : MonoBehaviour, IAddCoinsProduction
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private Image _overlay;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextAsset _rewardsJson;
    [SerializeField] private RewardEntryView _rewardEntryPrefab;
    [SerializeField] private Transform _entryParent;
    [SerializeField] private CoinHUDView _coinHUDView;
    [SerializeField] private ScrollRect _scrollRect;

    private RewardEntryPool _rewardEntryPool;
    private Action _onPopupClosed;
    private List<RewardEntryView> _activeRows = new List<RewardEntryView>();

    public void Show(Action onClose)
    {
        if (_rewardEntryPool == null)
            _rewardEntryPool = new RewardEntryPool(_rewardEntryPrefab, _entryParent);
        else
            _rewardEntryPool.ResetPool();
        
        gameObject.SetActive(true);
        Utils.PlayScaleUpAnimation(_overlay, _container);
        PopulateList();
        _onPopupClosed = onClose;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.X))
    //         _scrollRect.verticalNormalizedPosition = 1;
    // }

    private void Start()
    {
        InitializeButtons();
        ResetScroller();
    }

    private void InitializeButtons()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        Utils.PlayButtonImpact();
        Utils.PlayScaleDownAnimation(_overlay, _container, () => gameObject.SetActive(false));
        _onPopupClosed.Invoke();
    }

    private void PopulateList()
    {
        _activeRows.Clear();
        var rewards = JsonUtility.FromJson<Rewards>(_rewardsJson.text);
        foreach (var reward in rewards.rewards)
        {
            var entry = _rewardEntryPool.Get();
            entry.SetData(reward, this);
            _activeRows.Add(entry);
        }
    }

    void IAddCoinsProduction.PlayAddCoinsProduction(Vector3 fromPos, int coinCount) =>
        _coinHUDView.StartCoinsAddedProduction(fromPos, coinCount, true, LockRewards);

    private void LockRewards()
    {
        foreach (var row in _activeRows)
            row.SetButtonLockedState();
    }

    private void ResetScroller()
    {
        DOVirtual.DelayedCall(0.1f, () => _scrollRect.verticalNormalizedPosition = 1);
    }
}
