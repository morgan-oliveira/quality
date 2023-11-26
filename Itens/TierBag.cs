using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace quality.Itens;
public class TierBag : ModItem {

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.consumable = true;
        Item.width = 24;
        Item.height = 24;
        Item.rare = ItemRarityID.Orange;

    }
    public override bool CanRightClick() => true;
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AngelicSword>(), 1, 1, 1));
    }
}