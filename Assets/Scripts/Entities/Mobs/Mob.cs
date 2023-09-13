using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{
    protected Transform target;
    protected int damage;
    public EventHandler OnMobHealthChange;

    protected float nextShootTime;

    private void Update()
    {
        HandleHealthChange();
        HandleEntityDeath();
    }

    private void HandleHealthChange()
    {
        OnMobHealthChange(this, EventArgs.Empty);
    }
}
