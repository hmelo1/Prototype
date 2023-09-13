using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip
{
    private ChipData chipData;

    //X, A, B, C, D, F, *
    private string code;

    public void OnUse(Transform userTransform)
    {
        //Your code for using the chip here
    }
    
    public ChipData GetChipData()
    {
        return chipData;
    }

    public void Initialize(ChipData chipData)
    {
        this.chipData = chipData;
    }

    public void Initialize(ChipData chipData, string code)
    {
        this.chipData = chipData;
        this.code = code;
    }

    public virtual void TriggerAbility(Transform userTransform)
    {
        Debug.Log("Name: " + chipData.displayName);
    }
}
