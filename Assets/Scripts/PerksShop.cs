using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksShop : MonoBehaviour
{
    [SerializeField] private int playerMoney = 0;

    [SerializeField] private Text moneyTextComponent = null;

    [SerializeField] private Color blockingColor = Color.white;
    
    [SerializeField] private Text descriptionComponent = null;

    [SerializeField] private ActivatorPerks activatorPerks = null;
    
    public void TryBuying()
    {
        TableSlotControl tableSlot = TableSlotControl.FindSelected();
        if (tableSlot == null)
            return;

        int price = tableSlot.GetPrice();
        if (price > playerMoney)
            return;

        playerMoney -= price;
        moneyTextComponent.text = "" + playerMoney;

        PerkType perk = tableSlot.GetPerkType();
        activatorPerks.ActivatePerk(perk);
        
        tableSlot.DropSelect();
        tableSlot.SetBackgroundColor(blockingColor);
        tableSlot.ScriptDestroy();

        descriptionComponent.text = "Куплено";
    }
}
