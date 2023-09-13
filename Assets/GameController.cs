using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    FREEROAM,
    BATTLE
}

public class GameController : MonoBehaviour
{
    public GameState currentState = GameState.FREEROAM;

    // Start is called before the first frame update
    void Start()
    {
        // Listen for the BattleStart_OnBattleStart event
        StartBattle.OnBattleStart += OnBattleStartHandler;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Switch the state to BATTLE when the BattleStart_OnBattleStart event is triggered
    private void OnBattleStartHandler()
    {
        if(currentState == GameState.BATTLE)
        {
            Debug.Log("Battle already started");
            return;
        }
        currentState = GameState.BATTLE;
        Debug.Log("Battle Starting");

        // Load the battle scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
        
        // Set the battle scene as the active scene
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("Battle"));
    }
}