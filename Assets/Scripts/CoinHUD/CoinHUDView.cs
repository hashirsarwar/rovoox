using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinHUDView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCount;
    [SerializeField] private RectTransform _animTarget;
    [SerializeField] private RectTransform _coinsContainer;
    
    // This function takes global positions.
    public void StartCoinsAddedProduction(Vector3 fromPos, int coinCount, bool moveHUD, Action completed = null)
    {
        Utils.BlockTouchInput(GetComponentInParent<Canvas>(), true);
        var currCoins = GetCoinCount();
        SaveCoinCount(currCoins + coinCount);
        var coinHud = GetComponent<RectTransform>();
        coinHud.DOAnchorPosX(moveHUD ? 35 : coinHud.anchoredPosition.x, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            AnimateCoins(fromPos);
            DOVirtual.DelayedCall(1f, () =>
                DOVirtual.Int(currCoins, currCoins + coinCount, 1, SetCoinCount));
            DOVirtual.DelayedCall(2.5f, () =>
                    coinHud.DOAnchorPosX(moveHUD ? -200 : coinHud.anchoredPosition.x, 0.3f).SetEase(Ease.InBack))
                .OnComplete(() =>
                {
                    Utils.BlockTouchInput(GetComponentInParent<Canvas>(), false);
                    completed?.Invoke();
                });
        });
    }
    
    public void SetCoinCountFromPrefs() => SetCoinCount(PlayerPrefs.GetInt(_coinsPrefKey, 0));

    private readonly string _coinsPrefKey = "RovooxCoinState";
    private int GetCoinCount() => int.Parse(_coinCount.text);
    
    private void SetCoinCount(int count) => _coinCount.text = count.ToString();
    

    // setting PlayerPrefs is expensive, thus separated it from SetCoinCount
    // which is called in a loop.
    private void SaveCoinCount(int coins) => PlayerPrefs.SetInt(_coinsPrefKey, coins);

    private void AnimateCoins(Vector3 fromPos)
    {
        _coinsContainer.transform.position = fromPos;
        StartCoroutine(IAnimateCoins());
    }

    private void Start() => SetCoinCountFromPrefs();

    private IEnumerator IAnimateCoins()
    {
        foreach (Transform coin in _coinsContainer)
        {
            coin.localPosition = Vector3.zero;
            coin.gameObject.SetActive(true);
            coin.DOMove(_animTarget.transform.position, 1).SetEase(Ease.InBack)
                .OnComplete(() => coin.gameObject.SetActive(false));
            yield return new WaitForSeconds(.25f);
        }
    }
}
