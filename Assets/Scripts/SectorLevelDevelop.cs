using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorLevelDevelop : MonoBehaviour
{
    [SerializeField] private Color playerHaveColor = Color.green;

    public void MarkAsPlayerHave()
    {
        Image image = GetComponent<Image>();
        image.color = playerHaveColor;
    }
}
