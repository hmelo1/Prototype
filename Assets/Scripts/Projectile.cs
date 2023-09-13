using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    private Vector3 shootDir;
    private int damage;
    private Team team;

    public void Setup(Vector3 shootDir, int damage, Team team, Vector3 initialPos)
    {
        this.shootDir = shootDir;
        this.damage = damage;
        this.team = team;
        this.gridPosition = GridManager.Instance.GetGridPosition(initialPos);
        GridManager.Instance.SetGridObjectStatus(GridManager.Instance.GetGridObjectFromGridPosition(gridPosition), Status.Caution);
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        float moveSpeed = 3f;
        transform.position += shootDir * moveSpeed *Time.deltaTime;
        if (!GridManager.Instance.IsValidGridPositionFromWorldPosition(transform.position))
        {
            Destroy(gameObject);
        }
        // Set Grid Position to caution
        GridPosition newPos = GridManager.Instance.GetGridPosition(transform.position);
        if(this.gridPosition != newPos)
        {
            GridManager.Instance.SetGridObjectStatus(GridManager.Instance.GetGridObjectFromGridPosition(this.gridPosition), Status.Regular);
            this.gridPosition = newPos;
            if(GridManager.Instance.GetGridObjectFromGridPosition(newPos) != null)
            {
                GridManager.Instance.SetGridObjectStatus(GridManager.Instance.GetGridObjectFromGridPosition(newPos), Status.Caution);
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Entity>().HandleDamageTaken(this.damage);
            GridManager.Instance.SetGridObjectStatus(GridManager.Instance.GetGridObjectFromGridPosition(this.gridPosition), Status.Regular);
            Destroy(gameObject);
        }
    }
}
