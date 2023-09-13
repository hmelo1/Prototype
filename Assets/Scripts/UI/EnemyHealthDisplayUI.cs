using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Mob mob;

    private void Start()
    {
        mob.OnMobHealthChange += Mob_OnHealthChange;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = mob.Health.ToString();
    }

    private void Mob_OnHealthChange(object sender, EventArgs e)
    {
        UpdateHealthText();
    }
}
