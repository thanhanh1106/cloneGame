using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected FloatValueSO currentStats;

    public event Action<float> OnStatsChanged;

    protected virtual void Start()
    {
        currentStats.Value = currentStats.MaxValue;
    }
    
    public virtual void AddStats(float statsValue)
    {
        float currentStatsValue = currentStats.Value;
        currentStatsValue += statsValue;
        currentStatsValue = Mathf.Clamp(currentStatsValue, 0, currentStats.MaxValue);
        currentStats.Value = currentStatsValue;
        OnStatsChanged?.Invoke(currentStatsValue);
    }
    public virtual void RemoveStats(float statsValue)
    {
        float currentStatsValue = currentStats.Value;
        currentStatsValue -= statsValue;
        currentStatsValue = Mathf.Clamp(currentStatsValue, 0, currentStats.MaxValue);
        currentStats.Value = currentStatsValue;
        OnStatsChanged?.Invoke(currentStatsValue);
    }
    public virtual void ResetStats()
    {
        currentStats.Value = currentStats.MaxValue;
    }
}
