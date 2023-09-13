using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    public delegate void BattleStartHandler();
    public static event BattleStartHandler OnBattleStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Trigger the OnBattleStart event
            if (OnBattleStart != null)
            {
                OnBattleStart();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger exited by " + other.gameObject.name);
        }    
    }
}