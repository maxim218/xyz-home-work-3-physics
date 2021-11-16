using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerkType
{
    Empty,
    JumpPerk,
    SkinPerk,
    RunPerk,
    TeleportPerk
}

public class ActivatorPerks : MonoBehaviour
{
    public void ActivatePerk(PerkType perk)
    {
        switch (perk)
        {
            case PerkType.SkinPerk:
                SkinActivate();
                break;
            case PerkType.JumpPerk:
                JumpActivate();
                break;
            case PerkType.RunPerk:
                RunActivate();
                break;
            case PerkType.TeleportPerk:
                TeleportActivate();
                break;
        }
    }

    [SerializeField] private SpriteRenderer heroSpriteRenderer = null;
    
    private void SkinActivate()
    {
        Color skinColor = Color.black;
        heroSpriteRenderer.color = skinColor;
    }

    [SerializeField] private PlayerMoving playerMoving = null;

    private void JumpActivate()
    {
        const float force = 100;
        playerMoving.SetForceVertical(force);
    }

    private void RunActivate()
    {
        const float horSpeed = 12;
        playerMoving.SetHorizontalSpeed(horSpeed);
    }

    private static void TeleportActivate()
    {
        GameObject hero = GameObject.Find("Hero");
        const int x = 20;
        const int y = 10;
        const int z = 0;
        hero.transform.Translate(x, y, z);
    }
}
