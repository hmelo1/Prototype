using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public event EventHandler OnAttack;
    public EventHandler OnPlayerHealthChange;

    private List<Chip> playerDeck = new List<Chip>();
    private List<ChipDisplay> currentHand = new List<ChipDisplay>();
    private List<Chip> availableChips = new List<Chip>();

    [SerializeField] private BattleManager battleManager;
    [SerializeField] private ChipManager chipManager;


    // Start is called before the first frame update
    void Start()
    {
        this.Team = Team.Team1;
        this.Health = 100;
        this.MaxHealth = this.Health;
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddEntityAtGridPosition(gridPosition, this);



        // Needs refactor.
        this.availableChips = chipManager.GetAllChipsAvailable();

        // playerDeck.Add(availableChips[0]);
        playerDeck.Add(availableChips[1]);
        playerDeck.Add(availableChips[0]);
        playerDeck.Add(availableChips[2]);
        playerDeck.Add(availableChips[0]);
        playerDeck.Add(availableChips[2]);
        playerDeck.Add(availableChips[3]);
        playerDeck.Add(availableChips[3]);
    }

    // Update is called once per frame
    void Update()
    {
        HandleHealthChange();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            HandleAttack(transform.position, transform.TransformDirection(Vector3.forward));
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseEquippedChip();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            HandleEntityMovement(-this.cellSize, 0);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            HandleEntityMovement(0, -this.cellSize);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            HandleEntityMovement(this.cellSize, 0);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            HandleEntityMovement(0, this.cellSize);
        }
    }

    public void HandleHealthChange()
    {
        OnPlayerHealthChange(this, EventArgs.Empty);
    }

    public void HandleAttack(Vector3 shootPosition, Vector3 shootDirection)
    {
        OnAttack?.Invoke(this, EventArgs.Empty);
        
        RaycastHit hit; 
        if(Physics.Raycast(shootPosition, shootDirection, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            HandleEntityAttack(1, hit.collider.gameObject.GetComponent<Entity>());
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

    public void HandleHealing(int healthGain)
    {
        this.Health += healthGain;
    }

    public void UseEquippedChip()
    {
        if (currentHand.Count > 0)
        {
            ChipDisplay firstChip = currentHand[0];
            currentHand.RemoveAt(0);
            firstChip.GetChipData().TriggerAbility(this.transform);
        }
        else
        {
            Debug.LogWarning("Current hand is empty, cannot remove chip.");
        }
    }

    public void UpdateCurrentHand(List<ChipDisplay> newHand)
    {
        currentHand.Clear();
        currentHand = newHand;
    }

    public List<Chip> GetPlayerChips()
    {
        return playerDeck;
    }
}
