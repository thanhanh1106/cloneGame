using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCharacterController : MonoBehaviour
{
    public enum AnimationState
    {
        Movement,
        Attack
    }
    public enum MovementType
    {
        Idle,
        Run
    }
    public enum AttackType
    {
        Shoot
    }

    Animator animator;

    protected AnimationState currentAnimationState;
    protected MovementType currentMovementType;
    protected AttackType currentAttackType;
    protected string currentTrigger;

    public Animator Animator => this.TryGetComponent(ref animator);

    bool animationDieTriggered = false;

    public bool ApplyRootMotion 
    { 
        get => Animator.applyRootMotion;
        set => Animator.applyRootMotion = value;
    }

    public void SetMovement(MovementType type)
    {
        if (animationDieTriggered) return;
        // nếu đang trong trạng thái di chuyển và đúng kiểu di chuyển rồi thì thôi
        if (currentAnimationState == AnimationState.Movement && currentMovementType == type)
            return;

        SetFloat("MovementBlend",(float)type);
        SetTrigger("Movement");

        currentAnimationState = AnimationState.Movement;
        currentMovementType = type;
    }

    public void SetAttack(AttackType type)
    {
        if(animationDieTriggered) return;
        if(currentAnimationState == AnimationState.Attack && currentAttackType == type) return;

        SetFloat("AttackBlend", (float)type);
        SetTrigger("Attack");

        currentAnimationState = AnimationState.Attack;
        currentAttackType = type;
    }

    public void SetFloat(string name, float value)
    {
        this.Animator.SetFloat(name, value);
    }

    public void SetBool(string name, bool value)
    {
        this.Animator.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        // nếu trạng thái này đang trigger rồi thì return
        if(name.Equals(currentTrigger)) return;


        // reset lại biến trigger hiện tại (ko cho nó trigger nữa)
        if (!string.IsNullOrEmpty(currentTrigger))
            this.Animator.ResetTrigger(currentTrigger);

        this.Animator.SetTrigger(name);
        currentTrigger = name;
    }
}
