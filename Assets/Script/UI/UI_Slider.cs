using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Slider : MonoBehaviour
{

    [SerializeField] Image sliderImage;
    [SerializeField] FloatValueSO stats;
    private void OnEnable()
    {
        stats.OnValueChanged += SetValue;
    }
    private void OnDisable()
    {
        stats.OnValueChanged -= SetValue;
    }
    public void SetValue(float value)
    {
        value = value / stats.MaxValue;
        sliderImage.DOFillAmount(value, 0.2f);
    }
}
