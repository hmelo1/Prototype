using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip_Spread : Chip
{
    public override void TriggerAbility(Transform userTransform)
    {
        Debug.Log($"Spread: {this.GetChipData().displayName}");
    }
}