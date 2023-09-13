using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip_Shockwave : Chip
{
    public override void TriggerAbility(Transform userTransform)
    {
        Debug.Log($"Shockwave: {this.GetChipData().displayName}");
    }
}