using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handle Validatioin in the future.
public class HandDisplay : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup displayGrid;
    [SerializeField] private GridLayoutGroup selectorGrid;

    // List of chip displays in the hand
    private List<ChipDisplay> chipsInHand = new List<ChipDisplay>();

    // Add a chip display to the hand
    public void AddChipToHand(ChipDisplay chipDisplay)
    {
        chipsInHand.Add(chipDisplay);
        chipDisplay.transform.SetParent(displayGrid.transform, false);
    }

    // Remove a chip display from the hand
    public void RemoveChipFromHand(ChipDisplay chipDisplay)
    {
        chipsInHand.Remove(chipDisplay);
        chipDisplay.transform.SetParent(selectorGrid.transform, false);
    }

    // Clear all chip displays from the hand
    public void ClearHand()
    {
        foreach (ChipDisplay chipDisplay in chipsInHand)
        {
            chipDisplay.transform.SetParent(selectorGrid.transform, false);
        }

        chipsInHand.Clear();
    }

    public List<ChipDisplay> GetChipDisplays()
    {
        return chipsInHand;
    }
}