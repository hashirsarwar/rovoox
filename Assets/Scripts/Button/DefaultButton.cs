using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DefaultButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _topLayer;
    [SerializeField] private RectTransform _bottomLayer;
    [SerializeField] private RectTransform _shadow;

    private Vector2 _topInitialPos;
    private Vector2 _bottomInitialPos;
    private Vector2 _shadowInitialPos;

    private Color _topColor;
    private Color _bottomColor;

    private void OnEnable()
    {
        _topInitialPos = _topLayer.anchoredPosition;
        _bottomInitialPos = _bottomLayer.anchoredPosition;
        _shadowInitialPos = _shadow.anchoredPosition;
        _topColor = _topLayer.GetComponent<Image>().color;
        _bottomColor = _bottomLayer.GetComponent<Image>().color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _topLayer.anchoredPosition = _bottomInitialPos;
        _shadow.anchoredPosition = _bottomInitialPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _topLayer.anchoredPosition = _topInitialPos;
        _bottomLayer.anchoredPosition = _bottomInitialPos;
        _shadow.anchoredPosition = _shadowInitialPos;
    }

    public void GreyOutButton()
    {
        _topLayer.GetComponent<Image>().color = Color.gray;
        _bottomLayer.GetComponent<Image>().color = Color.gray;
    }

    public void ReturnColorToNormal()
    {
        _topLayer.GetComponent<Image>().color = _topColor;
        _bottomLayer.GetComponent<Image>().color = _bottomColor;
    }
}
