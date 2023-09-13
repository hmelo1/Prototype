using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    [SerializeField] private List<ChipData> ListOfAllChipDatas = new List<ChipData>();

    private List<Chip> listOfAllChips = new List<Chip>();
    // Handle MasterDeck unlocks
    // Handle CurrentRunDeck unlocks/upgrades
    // Handle card history

    private Dictionary<ChipEffect, System.Type> chipTypeDictionary = new Dictionary<ChipEffect, System.Type>()
    {
        { ChipEffect.Spread, typeof(Chip_Spread) },
        { ChipEffect.Shotgun, typeof(Chip_Shotgun) },
        { ChipEffect.Shockwave, typeof(Chip_Shockwave) },
        { ChipEffect.Cannon, typeof(Chip_Cannon) }
        /*
        { ChipEffect.Self, typeof(Chip_Self) },
        { ChipEffect.Target, typeof(Chip_Target) },
        { ChipEffect.Field, typeof(Chip_Field) },
        { ChipEffect.Trap, typeof(Chip_Trap) },
        { ChipEffect.Sensor, typeof(Chip_Sensor) },
        { ChipEffect.Sword, typeof(Chip_Sword) },
        { ChipEffect.Champion, typeof(Chip_Champion) */
    };

    private void Awake()
    {
        foreach(ChipData chipdata in ListOfAllChipDatas)
        {
            // Create a new instance of the appropriate subclass based on the chip's effect
            Chip newChip = CreateChipFromData(chipdata);


            // Initialize the new chip and add it to the list
            newChip.Initialize(chipdata);
            listOfAllChips.Add(newChip);
        }
    }

    public List<Chip> GetAllChipsAvailable()
    {
        return listOfAllChips;
    }

    private Chip CreateChipFromData(ChipData chipData)
    {
        Chip newChip;

        switch (chipData.ChipEffect)
        {
            case ChipEffect.Spread:
                newChip = new Chip_Cannon();
                break;
            case ChipEffect.Shotgun:
                newChip = new Chip_Cannon();
                break;
            case ChipEffect.Shockwave:
                newChip = new Chip_Cannon();
                break;
            case ChipEffect.Cannon:
                newChip = new Chip_Cannon();
                break;
            default:
                newChip = new Chip();
                break;
        }

        return newChip;
    }
}
