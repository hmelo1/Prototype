using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChipDisplay : MonoBehaviour
{
    [SerializeField] private Chip chip;

    [SerializeField] private TextMeshProUGUI DmgTxt;
    [SerializeField] private Image ChipImg;
    [SerializeField] private TextMeshProUGUI CodeTxt;
    [SerializeField] private Button button;

    public EventHandler<bool> HandleChipInHand;

    private void Awake()
    {
        button.onClick.AddListener(SendChipData);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChipData data = chip.GetChipData();
        DmgTxt.text = data.Damage.ToString();
        ChipImg.sprite = data.icon;
        CodeTxt.text = data.Code;
    }

    public void LoadChipData(Chip chip)
    {
        this.chip = chip;
    }

    void Update()
    {
        DmgTxt.text = chip.GetChipData().Damage.ToString();
        ChipImg.sprite = chip.GetChipData().icon;
        CodeTxt.text = chip.GetChipData().Code;
    }

    public void SendChipData()
    {       
        if(this.transform.parent.tag == "SelectorGrid")
        {
            HandleChipInHand(this, true);
        }
        else
        {
            HandleChipInHand(this, false);
        }
    }

    public Chip GetChipData()
    {
        return chip;
    }
}
