using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip_Cannon : Chip
{
    public override void TriggerAbility(Transform userTransform)
    {
        // Fire a raycast from the user's transform position
        RaycastHit hit;
        if (Physics.Raycast(userTransform.position, userTransform.forward, out hit))
        {
            GridPosition gp = GridManager.Instance.GetGridPosition(hit.transform.position);
            hit.collider.gameObject.GetComponent<Entity>().HandleDamageTaken(this.GetChipData().Damage);

            foreach (var gridCoordinate in this.GetChipData().TargetGridCoordinates)
            {
                // Bug around here
                /*
                    IndexOutOfRangeException: Index was outside the bounds of the array.
                    Chip_Cannon.TriggerAbility (UnityEngine.Transform userTransform) (at Assets/Scripts/Chips/ChipEffects/Chip_Cannon.cs:18)
                    Player.UseEquippedChip () (at Assets/Scripts/Entities/Player/Player.cs:107)
                    Player.Update () (at Assets/Scripts/Entities/Player/Player.cs:54)
                */
                GridObject nextGridObject = GridManager.Instance.GetGridObject(gp.x + gridCoordinate.RangeX, gp.z + gridCoordinate.RangeZ);

                if (nextGridObject.Entity != null)
                {
                    nextGridObject.Entity.HandleDamageTaken(this.GetChipData().Damage);
                }
            }
        }
    }
}