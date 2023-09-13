using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnState
{
    PROCESSING,
    PENDING,
    SELECTING,
    ACTION,
    ENDBATTLE,
}

public class BattleManager : MonoBehaviour
{
    public TurnState currentState;
    public float currentProgress = 0f;
    public float maxProgress = 10f;

    [SerializeField] private Slider progressBar;
    [SerializeField] private ChipSelector chipSelector;
    [SerializeField] private ChipManager chipManager;
    [SerializeField] private Player player;
    [SerializeField] private List<ChipDisplay> currentHand;

    public EventHandler OnChipSelector;

    // Start is called before the first frame update
    void Start()
    {
        chipSelector.OnConfirmation += ChipSelector_OnConfirmation;
        currentState = TurnState.SELECTING; 
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && currentState == TurnState.PENDING)
        {
            currentState = TurnState.SELECTING;
        }
        if(Input.GetKeyDown(KeyCode.J) && currentState == TurnState.SELECTING)
        {
            currentState = TurnState.PROCESSING;
        }

        switch(currentState)
        {
            case(TurnState.PROCESSING):
                ResumeGame();
                UpgradeProgressBar();
                break;
            case(TurnState.PENDING):
                break;
            case(TurnState.SELECTING):
                RestartProgressBar();
                PauseGame();
                HandleChipSelector();
                break;
            case(TurnState.ACTION):
                break;
            case(TurnState.ENDBATTLE):
                break;
            default:
                break;
        }
    }

    public void HandleChipSelector()
    {
        OnChipSelector(this, EventArgs.Empty);
    }

    private void RestartProgressBar()
    {
        currentProgress = 0f;
    }

    private void UpgradeProgressBar()
    {
        currentProgress = currentProgress + Time.deltaTime;
        float calcProgress = currentProgress/maxProgress;
        progressBar.value = calcProgress;
        if(currentProgress >= maxProgress)
        {
            currentState = TurnState.PENDING;
        }
    }
    
    private void PauseGame ()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    private void ChipSelector_OnConfirmation(object sender, List<ChipDisplay> chips)
    {
        UpdateCurrentHand(chips);
        currentState = TurnState.PROCESSING;
    }

    public void UpdateCurrentHand(List<ChipDisplay> selectedChips)
    {
        // Clear the current hand to prepare for the new chips.
        foreach(ChipDisplay chip in selectedChips)
        {
            Debug.Log($"BattleManager: {chip.name}");
        }

        currentHand = selectedChips;

        // Update the player's current hand.
        player.UpdateCurrentHand(selectedChips);
    }
}
