using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator _buttonAnimator;

    private readonly int _buttonPressedKey = Animator.StringToHash("Pressed");
    private readonly int _buttonDefaultKey = Animator.StringToHash("Default");

    private void OnEnable() => PlayDefaultAnimation();

    private void Pressed() => _buttonAnimator.SetTrigger(_buttonPressedKey);

    private void PlayDefaultAnimation() => _buttonAnimator.SetTrigger(_buttonDefaultKey);

    public void OnPointerDown(PointerEventData eventData) => Pressed();

    public void OnPointerUp(PointerEventData eventData) => PlayDefaultAnimation();
}
