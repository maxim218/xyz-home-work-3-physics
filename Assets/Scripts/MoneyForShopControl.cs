using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyForShopControl : MonoBehaviour
{
    [SerializeField] private Text textComponent = null;

    public void SetMoneyCount(int count)
    {
        string text = "" + count;
        textComponent.text = text;
    }
    
    public int GetMoneyCount()
    {
        string text = textComponent.text;
        string trim = text.Trim();
        int count = int.Parse(trim);
        return count;
    }
    
    public static MoneyForShopControl GetMoneyController()
    {
        MoneyForShopControl moneyForShopControl = FindObjectOfType<MoneyForShopControl>();
        return moneyForShopControl;
    }
}
