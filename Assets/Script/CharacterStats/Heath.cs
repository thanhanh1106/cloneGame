using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : CharacterStats
{
    public event Action OnDie;
    public override void AddStats(float statsValue)
    {
        base.AddStats(statsValue);

    }
    public override void RemoveStats(float statsValue)
    {
        base.RemoveStats(statsValue);
        if (currentStats.Value <= 0)
            Die();
    }
    private void Die()
    {
        OnDie?.Invoke();
    }

}
