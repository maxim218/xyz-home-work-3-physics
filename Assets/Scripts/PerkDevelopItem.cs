using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerkDevelopItem : MonoBehaviour
{
    [SerializeField] private SectorLevelDevelop[] sectorsArray = null;

    [SerializeField] private int price = 0;

    [SerializeField] private UnityEvent improvePerkEvent = null;
    
    public void TryBuyNextLevel()
    {
        string operationResult = CallBuyingOperation();
        Debug.Log(operationResult);
    }

    private string CallBuyingOperation()
    {
        int playerMoney = MoneyForShopControl.GetMoneyController().GetMoneyCount();
        if (playerMoney < price)
            return "Not enough money";

        _cubeIndex++;
        if (_cubeIndex >= sectorsArray.Length)
            return "Perk has maximum value";
        sectorsArray[_cubeIndex].MarkAsPlayerHave();
        
        playerMoney -= price;
        MoneyForShopControl.GetMoneyController().SetMoneyCount(playerMoney);

        improvePerkEvent.Invoke();
        return "OK";
    }
    
    private int _cubeIndex = 0;
}
