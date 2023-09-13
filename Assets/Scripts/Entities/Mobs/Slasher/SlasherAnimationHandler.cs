using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlasherAnimationHandler : MonoBehaviour
{
    [SerializeField] private Slasher slasher;

    public void Attack()
    {
        slasher.Attack();
    }

    public void ReturnTeleort()
    {
        slasher.SetAttacking(false);
        slasher.TeleportToRandomLocation();
    }
}
