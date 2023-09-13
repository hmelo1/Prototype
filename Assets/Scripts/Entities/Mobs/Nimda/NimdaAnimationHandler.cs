using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimdaAnimationHandler : MonoBehaviour
{
    [SerializeField] private Nimda nimda;

    public void Attack()
    {
        nimda.LaunchProjectile();
    }
}
