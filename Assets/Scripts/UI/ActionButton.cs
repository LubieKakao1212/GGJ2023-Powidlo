using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private MoveBase move;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TurnManager.Instance.MoveManager.SetCurrentMove(move);
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
