using System;
using UnityEngine;
using TMPro;

public class PlayerHealthDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Player player;

    private void Start()
    {
        player.OnPlayerHealthChange += Player_OnHealthChange;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        string healthString = $"{player.Health}/{player.MaxHealth}";
        healthText.text = healthString;
    }

    private void Player_OnHealthChange(object sender, EventArgs e)
    {
        UpdateHealthText();
    }
}
