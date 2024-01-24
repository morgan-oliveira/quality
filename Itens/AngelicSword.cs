
using quality.UI.ItemPolisher;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace quality.Itens;

public class AngelicSword : ModItem
{
    public override void SetDefaults()
    {
        Item.height = 40; 
        Item.width = 40;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Cyan;
        Item.damage = 1;
        Item.GetGlobalItem<GlobalQuality>().manualQuality = true;
        Item.GetGlobalItem<GlobalTier>().itemTier = "SSS";
        Item.GetGlobalItem<GlobalQuality>().quality = 99;
        Item.stack = 1;
    }
    public override bool? UseItem(Player player) {
        if (Main.netMode != NetmodeID.Server) {
            Main.NewText("ATAQUEI");
        }
        return true;
    }

}