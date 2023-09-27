using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour,IDamageable
{
    [Header("Config")]
    [SerializeField] protected string characterId;

    [Header("Component System")]
    [SerializeField] protected Agent agent;
    [SerializeField] protected AnimatorCharacterController animator;
    [SerializeField] protected CharacterAttack characterAttack;
    [SerializeField] protected StatsManager statsManager;
    
    public string Id => characterId;
    protected abstract CharacterBrain target { get; }
    protected bool CanAttack => target != null;

    protected virtual void Awake()
    {
        agent.Initialized();
    }
    protected virtual void Start()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<AnimatorCharacterController>();
        characterAttack = GetComponent<CharacterAttack>();
        statsManager.OnDie += HandlerDie;
    }
    //protected virtual void Update()
    //{

    //}

    protected virtual void Attack()
    {
        animator.SetAttack(AnimatorCharacterController.AttackType.Shoot);
        // cho quay mặt về phía target
        agent.RotateInDir(target.transform.position - transform.position);
        characterAttack.Attack(target.transform);
    }

    public virtual void TakeDamage(float damage)
    {
        statsManager.TakeDamage(damage);
    }
    protected abstract void HandlerDie();
}
