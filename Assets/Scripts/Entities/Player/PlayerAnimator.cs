using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (TryGetComponent<Player>(out Player player))
        {
            player.OnAttack += Player_OnAttack;
        }
    }

    private void Player_OnAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("Player_OnAttack");
    }
}
