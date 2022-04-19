using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void isAttacking(bool isAttacking)
    {
        animator.SetBool("canAttack", isAttacking);
    }
}
