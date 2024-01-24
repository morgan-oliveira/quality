using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Configuration;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace quality.Itens;

public class ItemPolisher : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Expert;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "polisher", "Use this item to polish your weapon, re-rolling its quality") { OverrideColor = Color.Coral });
    }
    public override bool? UseItem(Player player)
    {
        if (Main.netMode != NetmodeID.Server)
        {
            ModContent.GetInstance<ItemPolisherSystem>().ShowMyUI();
        }
        return true;
    }
}