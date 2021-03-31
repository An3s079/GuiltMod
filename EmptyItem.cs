using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;

namespace GuiltMod
{
    class EmptyItem : PassiveItem
    {
    public override DebrisObject Drop(PlayerController player)
    {
        
        DebrisObject result = base.Drop(player);
        return result;
    }

    public override void Pickup(PlayerController player)
    {
        base.Pickup(player);
    }
}
}
