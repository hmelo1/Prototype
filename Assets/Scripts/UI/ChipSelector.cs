using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChipSelector : MonoBehaviour
{
    [SerializeField] private GameObject chipSelector;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private Button confirmationButton;
    [SerializeField] private Button hideButton;
    [SerializeField] private GridLayoutGroup SelectorGrid;
    [SerializeField] private HandDisplay ProposedHandDisplay;
    [SerializeField] private Transform chipPrefab;
    [SerializeField] private Player player;
    
     // List of available chip displays for the player
    [SerializeField] private List<ChipDisplay> availableChips = new List<ChipDisplay>();
    
    // List of chip displays in the proposed hand for the current turn in battle
    private List<ChipDisplay> proposedHand = new List<ChipDisplay>();

     // Event that is raised when the player confirms their chip selection
    public EventHandler<List<ChipDisplay>> OnConfirmation;

    void Awake()
    {
        battleManager.OnChipSelector += BattleManager_OnChipSelector;

        hideButton.onClick.AddListener(ManageChipSelector);
        confirmationButton.onClick.AddListener(ManageConfirmation);

        chipSelector.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Loop through the player's chips and create a display for each one
        foreach (Chip chip in player.GetPlayerChips())
        {
            Transform pfTest = GameObject.Instantiate(chipPrefab, SelectorGrid.transform);
            ChipDisplay chipDisplayed = pfTest.GetComponent<ChipDisplay>();
            chipDisplayed.LoadChipData(chip);
            availableChips.Add(chipDisplayed);
            chipDisplayed.HandleChipInHand += ChipSelector_DisplayHand;
        }
    }

    private void Update()
    {
        // Check if there are any changes to the proposed hand, and update the display accordingly
        // This method is called every frame to ensure the display is up-to-date
        // Loop through the proposedHand list and add any new chips to the ProposedHandDisplay grid
        // If a chip has been removed from the proposedHand list, remove it from the display as well
        // Note: This implementation assumes that the ProposedHandDisplay grid has been set up properly in the inspector

        List<ChipDisplay> chipsToRemove = new List<ChipDisplay>();

        // Loop through the ProposedHandDisplay and remove any chips that are not in the proposedHand list
        foreach (ChipDisplay chipDisplay in ProposedHandDisplay.GetChipDisplays())
        {
            if (!proposedHand.Contains(chipDisplay))
            {
                chipsToRemove.Add(chipDisplay);
            }
        }

        // Remove the chips that need to be removed
        foreach (ChipDisplay chipDisplay in chipsToRemove)
        {
            ProposedHandDisplay.RemoveChipFromHand(chipDisplay);
        }

        if(proposedHand.Count > 4)
        {
            return;
        }

        // Loop through the proposedHand list and add any new chips to the ProposedHandDisplay grid
        foreach (ChipDisplay chipDisplay in proposedHand)
        {
            ProposedHandDisplay.AddChipToHand(chipDisplay);
        }
    }

    private void BattleManager_OnChipSelector(object sender, EventArgs e)
    {
        ActivateChipSelector();
    }

    private void ChipSelector_DisplayHand(object sender, bool isAdding)
    {
        if (!(sender is ChipDisplay chip)) return;

        if (isAdding)
        {
            proposedHand.Add(chip);
        }
        else
        {
            proposedHand.Remove(chip);
        }
    }

    public void ManageConfirmation()
    {
        DeactivateChipSelector();
        foreach(ChipDisplay chip in proposedHand)
        {
            Debug.Log($"Selector: {chip.name}");
        }
        OnConfirmation(this, proposedHand);
    }

    private void ActivateChipSelector()
    {
        chipSelector.SetActive(true);
    }

    private void DeactivateChipSelector()
    {
        chipSelector.SetActive(false);
    }

    private void ManageChipSelector()
    {
        if(chipSelector.activeSelf)
        {
            Debug.Log("Hiding");
            DeactivateChipSelector();
        }
        else
        {
            Debug.Log("Showing");
            ActivateChipSelector();
        }
    }
}
