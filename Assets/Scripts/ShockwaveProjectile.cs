using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveProjectile : MonoBehaviour
{
    [SerializeField] private Transform pfShockwave;
    [SerializeField] private Nimda nimda;
    private void Awake()
    {
        nimda.OnShockwave += ShockwaveProjectile_OnShockwave; 
    }

    private void ShockwaveProjectile_OnShockwave(object sender, Nimda.OnShockwaveEventArgs e)
    {
        Transform shockwaveTransform = Instantiate(pfShockwave, e.shootPosition + new Vector3(0,0,-2), Quaternion.identity);
        Vector3 shootDir = Vector3.back;
        shockwaveTransform.GetComponent<Projectile>().Setup(shootDir, e.damage, e.team, e.shootPosition + new Vector3(0,0,-2));
    }
}
