using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }   
        Instance = this; 
  
        gridSystem = new GridSystem(4, 8);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);  
    public GridObject GetGridObject(int positionX, int positionZ) => gridSystem.GetGridObject(positionX, positionZ);  
    public void AddEntityAtGridPosition(GridPosition gridposition, Entity entity) 
    {
        gridSystem.GetGridObject(gridposition).Entity = entity;
    }

    public GridObject GetGridObjectFromGridPosition(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition);

    public void RemoveEntityAtGridPosition(GridPosition gridPosition, Entity entity)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.Entity = null;
    }

    public void EntityMovedGridPosition(GridPosition fromGridPosition, GridPosition toGridPosition, Entity entity)
    {
        RemoveEntityAtGridPosition(fromGridPosition, entity);
        AddEntityAtGridPosition(toGridPosition, entity);
    }

    public bool IsValidGridPositionFromWorldPosition(Vector3 worldPosition, Entity entity)
    {
        return gridSystem.IsValidGridPosition(gridSystem.GetGridPosition(worldPosition)) &&
               gridSystem.GetGridObject(gridSystem.GetGridPosition(worldPosition)).Team == entity.Team;
    }

    public bool IsValidGridPositionFromWorldPosition(Vector3 worldPosition)
    {
        return gridSystem.IsValidGridPosition(gridSystem.GetGridPosition(worldPosition));
    }

    public bool IsGridPositionEmptyFromWorldPosition(Vector3 worldPosition)
    {
        return GetGridObjectFromGridPosition(GetGridPosition(worldPosition)).Entity == null;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public void SetGridObjectStatus(GridObject gridObject, Status status)
    {
        gridObject.SetGridStatus(status);
    }

    public void AddToTeamPositions(Team team, GridPosition gridPosition)
    {
        gridSystem.AddToTeamPositions(gridPosition, team);
    }

    public void RemoveFromTeamPositions(Team team, GridPosition gridPosition)
    {
        gridSystem.RemoveFromTeamPositions(gridPosition, team);
    }

    public GridPosition? GetRandomTeamPosition(Team team)
    {
        return gridSystem.GetRandomTeamPosition(team);
    }

    public List<GridPosition> GetTeamPositions(Team team)
    {
        return gridSystem.GetTeamPositions(team);
    }
}
