using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static void PlayButtonImpact()
    {
        SoundController.Instance.PlaySound(SoundType.ButtonClick);        
    }

    public static void PlayScaleUpAnimation(Image overlay, RectTransform container, float duration = 0.3f)
    {
        container.localScale = Vector3.zero;
        var color = overlay.color;
        var initialAlpha = color.a;
        color.a = 0;
        overlay.color = color;
        container.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
        overlay.DOFade(initialAlpha, duration);
    }
    
    public static void PlayScaleDownAnimation(Image overlay, RectTransform container, TweenCallback onComplete, float duration = 0.2f)
    {
        var initialColor = overlay.color;
        container.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
        overlay.DOFade(0, duration).OnComplete(() =>
        {
            onComplete.Invoke();
            overlay.color = initialColor;
        });
    }

    public static void BlockTouchInput(Canvas canvas, bool block)
    {
        canvas.GetComponent<GraphicRaycaster>().enabled = !block;
    }
}
