using UnityEngine;

public class SpinashPassive : PassiveItems
{
    
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
