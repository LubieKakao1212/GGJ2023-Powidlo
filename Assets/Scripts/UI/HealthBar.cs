using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Unit unit;

    private RectTransform rectTransform;
    private float width;

    private int defaultHealth;

    public void SetRatio(float ratio)
    {
        var d = rectTransform.sizeDelta;
        rectTransform.sizeDelta = new Vector2(width * ratio, d.y);
    }

    private void Start()
    {
        rectTransform = (RectTransform)transform;

        width = rectTransform.sizeDelta.x;

        defaultHealth = unit.health;

        unit.Damaged += (arg) => SetRatio(((float)arg) / defaultHealth);
    }
}
