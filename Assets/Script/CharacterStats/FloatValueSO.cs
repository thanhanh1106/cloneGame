using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatValue")]
public class FloatValueSO : ScriptableObject
{
    public float MaxValue;
    [SerializeField] private float value;
    public event Action<float> OnValueChanged;
    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}
