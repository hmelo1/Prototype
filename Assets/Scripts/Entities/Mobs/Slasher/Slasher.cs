using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : Mob
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Player player;
    public event EventHandler OnAttack;
    [SerializeField] private float cooldownTime = 5f; // Cooldown time in seconds
    private bool _canAttack = true; // Flag to track whether the mob can attack or not
    private float lastAttackTime; // Time of the last attack
    
    void Awake()
    {
        this.Health = 40;
        this.MaxHealth = this.Health;
        this.damage = 10;
        lastAttackTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Team = Team.Team2;
        this._isSpawned = true;
        target = GameObject.FindWithTag("Player").transform;
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddEntityAtGridPosition(gridPosition, this);
        InvokeRepeating("HandleMovement", 0.5f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == target.position.x)
        {
            HandleAttack();
        }

        // Check if the cooldown period is over
        if (!_canAttack && Time.time >= cooldownTime + lastAttackTime)
        {
            _canAttack = true; // Reset the canAttack flag
        }
    }

    private void HandleAttack()
    {
        if (_canAttack)
        {
            SetAttacking(true);
            Invoke("TeleportAfterDelay", 0.5f); // Delay the teleportation by 0.5 seconds
            _canAttack = false; // Start the cooldown period
            OnAttack?.Invoke(this, EventArgs.Empty);
            lastAttackTime = Time.time;
        }
    }

    private void TeleportAfterDelay()
    {
        HandleEntityTeleport(player.GetGridPosition().x, player.GetGridPosition().z+1);
    }

    private void HandleMovement()
    {
        if (_isAttacking || transform.position.x == target.position.x)
        {
            return;
        }

        // Get random Team position and move to that position if it's valid.
        TeleportToRandomLocation();
    }

    public void Attack()
    {
        Collider[] hitColliders = Physics.OverlapBox(attackPoint.position, new Vector3(1, 1, 1), Quaternion.identity);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Player player = hitCollider.GetComponent<Player>();
                if (player != null)
                {
                    Debug.Log("Player hit.");
                }
            }
        }

        
    }

    public void TeleportToRandomLocation()
    {
        bool foundValidPosition = false;
        while (!foundValidPosition)
        {
            GridPosition? randomPosition = GridManager.Instance.GetRandomTeamPosition(this.Team);
            if (randomPosition != null && 
                GridManager.Instance.GetGridObjectFromGridPosition(randomPosition.Value).Entity == null)
            {
                HandleEntityTeleport(randomPosition.Value.x, randomPosition.Value.z);
                foundValidPosition = true;
            }
        }
    }

    public void SetAttacking(bool attacking)
    {
        _isAttacking = attacking;
    }
}
