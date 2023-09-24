using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : CharacterStats
{
    public event Action<float> OnEndProtection;

    [SerializeField] float damageReductionRate;
    public override void AddStats(float statsValue)
    {
        base.AddStats(statsValue);

    }
    public override void RemoveStats(float statsValue)
    {
        float numOfRemainDamage = statsValue * (100 - damageReductionRate) / 100;

        if (currentStats.Value  >= numOfRemainDamage)
            base.RemoveStats(numOfRemainDamage);
        else if(currentStats.Value < numOfRemainDamage && currentStats.Value > 0)
        {
            // nếu số dame đã giảm vẫn lớn hơn số giáp còn lại thì trừ hết số giáp còn lại đi
            // còn bao nhiêu dame trừ thẳng vào máu
            float damage = (numOfRemainDamage - currentStats.Value)/(100 - damageReductionRate)*100;
            base.RemoveStats(currentStats.Value);
            EndProtection(damage);
        }
        else
            EndProtection(statsValue);
        
    }
    private void EndProtection(float statsValue)
    {
        OnEndProtection?.Invoke(statsValue);
    }
}
