using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Team1 = 1,
    Team2 = 0
}

public enum Status
{
    Caution = 1,
    Regular = 0
}

public class GridObject
{

    private GridSystem gridSystem;

    private GridPosition gridPosition;

    private List<Entity> EntityList; 

    public Team Team { get; set;}

    public Entity Entity {get; set;}

    private Status Status;

    public GridObject(GridSystem _gridSystem, GridPosition _gridPosition, Team _team) 
    {
        this.gridPosition = _gridPosition;
        this.gridSystem = _gridSystem;
        this.Team = _team;
        this.Status = Status.Regular;
    }

    public override string ToString()
    {
        return gridPosition.ToString() + "\n" + $"{this?.Entity?.name}" + "\n" + $"{this.Status}";
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public void SetGridStatus(Status status)
    {
        this.Status = status;
    }

    public Status GetGridStatus()
    {
        return Status;
    }
}
