using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlasherAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (TryGetComponent<Slasher>(out Slasher slasher))
        {
            slasher.OnAttack += Slasher_OnAttack;
        }
    }

    private void Slasher_OnAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("Slasher_OnAttack");
    }
}