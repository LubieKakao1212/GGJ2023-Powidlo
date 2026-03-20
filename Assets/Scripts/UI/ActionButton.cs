using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [FormerlySerializedAs("move")] [SerializeField]
    private UnitActionBase unitAction;

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Image imageUi;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TurnManager.Instance.MoveManager.SetCurrentMove(unitAction);
            eventData.Use();
            // InputManager.instance.SuppressNextSecondary();
        }

        imageUi.color = highlightColor;
        var seq = LeanTween.sequence();
        seq.append(LeanTween.color(imageUi.rectTransform, Color.white, 1f).setEaseOutExpo());
    }

    public void OnPointerEnter(PointerEventData eventData) {
        imageUi.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData) {
        imageUi.sprite = normalSprite;
    }
}
