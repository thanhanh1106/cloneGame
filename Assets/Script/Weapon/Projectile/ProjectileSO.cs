using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData")]
public class ProjectileSO : ScriptableObject
{
    [Header("Config")]
    public float MoveSpeed;
    public float TimeExistence; // thời gian tồn tại
    public float Damage;
    public ProjectileName ProjectileName;

    [Header("Object Reference")]
    public ParticleSystem ProjectileFX;
    public ParticleSystem HitFX;
    public ParticleSystem flashFX;
}
