using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimda : Mob
{
    public event EventHandler OnAttack;
    public EventHandler<OnShockwaveEventArgs> OnShockwave;
    public class OnShockwaveEventArgs : EventArgs 
    {
        public Vector3 shootPosition;
        public Team team;
        public int damage;
    }

    void Awake()
    {
        this.Health = 40;
        this.MaxHealth = this.Health;
        this.damage = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Team = Team.Team2;
        this._isSpawned = true;
        target = GameObject.FindWithTag("Player").transform;
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddEntityAtGridPosition(gridPosition, this);
        InvokeRepeating("HandleMovement", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == target.position.x)
        {
            HandleShockwave();
        }
    }

    private void HandleShockwave()
    {
        _isAttacking = true;
        this.nextShootTime += Time.deltaTime;
        
        if (nextShootTime >= 2.0f)
        {
            OnAttack?.Invoke(this, EventArgs.Empty);

            nextShootTime = 0.0f;
        } 
        _isAttacking = false;
    }

    public void LaunchProjectile()
    {
        OnShockwave?.Invoke(this, 
                    new OnShockwaveEventArgs 
                    {   shootPosition = this.transform.position, 
                        damage = this.damage,
                        team = this.Team});
    }
    private void HandleMovement()
    {
        if (_isAttacking || transform.position.x == target.position.x)
        {
            return;
        }

        if (transform.position.x > target.position.x)
        {
            HandleEntityMovement(-cellSize, 0);
        }
        else
        {
            HandleEntityMovement(cellSize, 0);
        }
    }
}
