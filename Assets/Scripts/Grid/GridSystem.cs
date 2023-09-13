using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width { get; }
    private int height { get; }
    private float cellSize = 2f;

    private GridObject[,] gridObjects;

    private Dictionary<Team, List<GridPosition>> teamPositions;

    public GridSystem(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.gridObjects = new GridObject[width, height];
        this.teamPositions = new Dictionary<Team, List<GridPosition>>();

        // Initialize the team positions
        teamPositions[Team.Team1] = new List<GridPosition>();
        teamPositions[Team.Team2] = new List<GridPosition>();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                GridObject newGridObject = CreateGridObject(gridPosition, z < 4 ? Team.Team1 : Team.Team2);
                gridObjects[x, z] = newGridObject;
            }
        }
    }

    private GridObject CreateGridObject(GridPosition gridPosition, Team team)
    {
        GridObject newGridObject = new GridObject(this, gridPosition, team);

        // Add the grid position to the team positions dictionary
        teamPositions[team].Add(gridPosition);

        return newGridObject;
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        if (!this.IsValidGridPosition(gridPosition))
        {
            return null;
        }
        return gridObjects[gridPosition.x, gridPosition.z];
    }

    public GridObject GetGridObject(int positionX, int positionZ)
    {
        return gridObjects[positionX, positionZ];
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public bool IsValidGridPosition(GridPosition gridPosition) => gridPosition.x >= 0 &&
                                                                  gridPosition.z >= 0 &&
                                                                  gridPosition.x < width &&
                                                                  gridPosition.z < height;

    public List<GridPosition> GetTeamPositions(Team team)
    {
        return teamPositions[team];
    }

    public void AddToTeamPositions(GridPosition gridPosition, Team team)
    {
        if (!teamPositions.ContainsKey(team))
        {
            teamPositions.Add(team, new List<GridPosition>());
        }
        teamPositions[team].Add(gridPosition);
    }

    public void RemoveFromTeamPositions(GridPosition gridPosition, Team team)
    {
        if (teamPositions.ContainsKey(team))
        {
            teamPositions[team].Remove(gridPosition);
        }
    }

    public GridPosition? GetRandomTeamPosition(Team team)
    {
        if (!teamPositions.ContainsKey(team) || teamPositions[team].Count == 0)
        {
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, teamPositions[team].Count);
        return teamPositions[team][randomIndex];
    }
}
