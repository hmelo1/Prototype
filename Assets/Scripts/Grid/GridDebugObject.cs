using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridDebugObject : MonoBehaviour
{
    public GridObject gridObject;
    [SerializeField] private TMP_Text gridText;

    public void SetGridObject(GridObject _gridObject) 
    {
        this.gridObject = _gridObject;
        this.gridText.text = this.gridObject?.ToString();
        if (gridObject?.Team == Team.Team1)
        {
            this.gridText.color = Color.blue;
        }
        else
        {
            this.gridText.color = Color.red;
        }
    }

    private void Update()
    {
        this.gridText.text = this.gridObject?.ToString();
        switch(gridObject?.GetGridStatus())
        {
            case Status.Caution:
                this.gridText.color = Color.yellow;
                break;
            default:
                this.gridText.color = gridObject?.Team == Team.Team1  ? Color.blue : Color.red;
                break;
        }
    }
}
