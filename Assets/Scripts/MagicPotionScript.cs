using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "MagicPotionData", menuName = "AssetMenu/MagicPotionData")]
public class MagicPotionScript : ScriptableObject {
   public string potionKey = string.Empty;
   public Sprite potionImage = null;
   public int potionMagic = 0;
}
