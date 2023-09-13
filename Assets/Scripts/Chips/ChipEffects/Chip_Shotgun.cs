using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip_Shotgun : Chip
{
    public override void TriggerAbility(Transform userTransform)
    {
        Debug.Log($"Shotgun: {this.GetChipData().displayName}");
    }
}