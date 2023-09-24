using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] Heath heath;
    [SerializeField] Armor armor;

    public event Action OnDie;
    private void Start()
    {
        armor.OnEndProtection += HandlerEndProtection;
        heath.OnDie += HandlerDie;
    }

    public void TakeDamage(float damage)
    {
        
        armor.RemoveStats(damage);
    }
    private void HandlerEndProtection(float statsValue)
    {
        heath.RemoveStats(statsValue);
    }
    private void HandlerDie()
    {
        OnDie?.Invoke();
    }
}
