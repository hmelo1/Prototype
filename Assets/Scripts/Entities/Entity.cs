using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected GridPosition gridPosition;

    protected Vector3 worldPosition;

    public Team Team { get; protected set; }

    public int Health { get; protected set; }

    public int MaxHealth { get; protected set; }

    // centralize cellSize
    protected int cellSize = 2;

    protected bool _isDead = false;

    protected bool _isAttacking = false;

    protected bool _isMoving = false;

    protected bool _isJunping = false;

    protected bool _isSliding = false;
    
    protected bool _isTeleporting = false;
    
    protected bool _isSpawned = false;
    

    public void HandleDamageTaken(int damageAmount)
    {
        this.Health -= damageAmount;
    }

    protected virtual void HandleEntityMovement(int X, int Z)
    {
        Vector3 oldWorldPosition = transform.position;
        GridPosition oldGridPosition = this.gridPosition;
        var newWorldPosition = new Vector3(oldWorldPosition.x + X, 0, oldWorldPosition.z + Z);

        // Out of Grid or Something else in grid Position
        if (!GridManager.Instance.IsValidGridPositionFromWorldPosition(newWorldPosition, this) ||
            !GridManager.Instance.IsGridPositionEmptyFromWorldPosition(newWorldPosition))
        {
            return;
        }

        transform.position = Vector3.MoveTowards(oldWorldPosition, newWorldPosition, cellSize+Time.deltaTime);
        GridPosition newGridPosition = GridManager.Instance.GetGridPosition(newWorldPosition);
        GridManager.Instance.EntityMovedGridPosition(oldGridPosition, newGridPosition, this);
        this.gridPosition = newGridPosition;
    }

    protected virtual void HandleEntityTeleport(int X, int Z)
    {
        Vector3 oldWorldPosition = transform.position;
        GridPosition oldGridPosition = this.gridPosition;
        var newWorldPosition = new Vector3(X*cellSize, 0, Z*cellSize);

        // Out of Grid or Something else in grid Position
        if (!GridManager.Instance.IsValidGridPositionFromWorldPosition(newWorldPosition) || 
            !GridManager.Instance.IsGridPositionEmptyFromWorldPosition(newWorldPosition))
        {
            return;
        }
        
        GridPosition newGridPosition = GridManager.Instance.GetGridPosition(newWorldPosition);
        GridManager.Instance.EntityMovedGridPosition(oldGridPosition, newGridPosition, this);
        transform.position = new Vector3(newWorldPosition.x, 0, newWorldPosition.z);
        this.gridPosition = newGridPosition;
    }
    
    private void Update()
    {
        HandleEntityDeath();
    }

    protected virtual void HandleEntityAttack(int damageAmount, Entity target)
    {
        target.Health -= damageAmount;
    }

    protected virtual void HandleEntityDeath()
    {
        // If Entity = enemy
        // Death animation
        // Destroy after delay

        // If Entity = player
        // Game Over
        if(this.Health <= 0)
        {
            this._isDead = true;
            GridManager.Instance.RemoveEntityAtGridPosition(this.gridPosition, this);
            Destroy(this.gameObject);
        }
    }

    public GridPosition GetGridPosition()
    {
        return GridManager.Instance.GetGridPosition(transform.position);
    }
}
