using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimdaAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (TryGetComponent<Nimda>(out Nimda nimda))
        {
            nimda.OnAttack += Nimda_OnAttack;
        }
    }

    private void Nimda_OnAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("Nimda_OnAttack");
    }
}
