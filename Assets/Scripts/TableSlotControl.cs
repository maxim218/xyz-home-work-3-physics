using System;
using UnityEngine;
using UnityEngine.UI;

public class TableSlotControl : MonoBehaviour
{
    [SerializeField] private Image imageComponent = null;

    [SerializeField] private Color notSelectedColor = Color.grey;
    [SerializeField] private Color selectedColor = Color.green;

    [SerializeField] private Text descriptionComponent = null;

    [SerializeField] private string aboutPerkInfo = string.Empty;

    [SerializeField] private int price = 0;

    [SerializeField] private PerkType perk = PerkType.Empty;
    
    private bool _isSelectedFlag = false;

    public PerkType GetPerkType()
    {
        return perk;
    }
    
    public void ScriptDestroy()
    {
        Destroy(this);
    }
    
    public void SetBackgroundColor(Color color)
    {
        imageComponent.color = color;
    }
    
    public int GetPrice()
    {
        return price;
    }
    
    public void DropSelect()
    {
        _isSelectedFlag = false;
        imageComponent.color = notSelectedColor;
    }

    private void FocusSelect()
    {
        _isSelectedFlag = true;
        imageComponent.color = selectedColor;
    }

    private bool IsSelected()
    {
        return _isSelectedFlag;
    }

    public void ClickMethod()
    {
        Type type = typeof(TableSlotControl);
        TableSlotControl[] arr = FindObjectsOfType(type) as TableSlotControl[];

        if (arr == null) return;
        foreach (TableSlotControl script in arr) script.DropSelect();

        FocusSelect();
        descriptionComponent.text = aboutPerkInfo + '\n' + "Цена - " + price;
    }

    public static TableSlotControl FindSelected()
    {
        Type type = typeof(TableSlotControl);
        TableSlotControl[] arr = FindObjectsOfType(type) as TableSlotControl[];
        
        if (arr == null) 
            return null;
        
        foreach (TableSlotControl script in arr)
            if (script.IsSelected())
                return script;

        return null;
    }
}
