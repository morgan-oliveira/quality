using IL.Terraria.GameContent.Bestiary;
using IL.Terraria.ID;
using On.Terraria.ID;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace quality
{
	public class quality : Mod
	{
        public override void Load()
        {
            GlobalNPCBuff.NightmareMode = false;
            GlobalNPCBuff.InfernoMode = false;
        }
    }
}