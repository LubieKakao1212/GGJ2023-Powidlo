using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [FormerlySerializedAs("move")] [SerializeField]
    private UnitActionBase unitAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TurnManager.Instance.MoveManager.SetCurrentMove(unitAction);
            eventData.Use();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO
    }
}
