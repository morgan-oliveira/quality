using System;
using System.Collections;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using IL.Terraria.GameContent.UI.Chat;
using Microsoft.CodeAnalysis.Operations;
using System.IO;
public class GlobalTier : GlobalItem
{
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;
    public List<String> tiers = new List<String>() { "D", "C", "B", "A", "S", "SS", "SSS" };
    public String itemTier;
    int tierKey;
    // ================================================================== //
    public bool haveSatanicTag;
    public bool haveHeroicTag;
    public bool haveAngelicTag;
    // ================================================================== //
    public override void SetDefaults(Item item)
    {
        item.GetGlobalItem<GlobalTier>().itemTier = itemTier;
    }
    // ================================================================== //
    #region TierItemCreation
    public override void OnSpawn(Item item, IEntitySource source)
    {
        //tierKey = Main.rand.Next(0, tiers.Count);
        TierRoll(item);
    }
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        //tierKey = Main.rand.Next(0, tiers.Count);
        TierRoll(item);
    }
    #endregion
    // ================================================================== //
    #region TierTooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {

        if ((item.damage > 0) || (item.defense > 0))
        {
            itemTier = tiers[tierKey];
            if (!GlobalQuality.IsBrokenItem(item))
            {
                tooltips.Add(new TooltipLine(Mod, "itemTier", $"Tier: {itemTier}") { OverrideColor = Color.Gold });
            }
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "S" && item.GetGlobalItem<GlobalQuality>().quality > 90)
        {
            tooltips.Add(new TooltipLine(Mod, "satanicToolTip", "[satanic]") { OverrideColor = Color.Red });
            haveSatanicTag = true;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "SS" && item.GetGlobalItem<GlobalQuality>().quality > 90)
        {
            tooltips.Add(new TooltipLine(Mod, "heroicToolTip", "[heroic]") { OverrideColor = Color.SeaGreen });
            haveHeroicTag = true;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "SSS" && item.GetGlobalItem<GlobalQuality>().quality > 90)
        {
            tooltips.Add(new TooltipLine(Mod, "angelicToolTip", "[angelic]") { OverrideColor = Color.Yellow });
            haveAngelicTag = true;
        }
    }
    #endregion
    // ================================================================== //
    #region TierProbabilities
    public void TierRoll(Item item)
    {
        float TierRoll = Main.rand.NextFloat();
        if (TierRoll < 0.9f)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "D";
            // Makes sure there are no more D tier items in hardmode
            if (Main.hardMode)
            {
                item.GetGlobalItem<GlobalTier>().itemTier = "C";
            }
        }
        if (TierRoll < 0.8f)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "C";
        }
        if (TierRoll < 0.5f)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "B";
        }
        if (TierRoll < 0.2f)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "A";
        }
        if (TierRoll < 0.1f && Main.hardMode)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "S";
        }
        if (TierRoll < 0.01f && Main.hardMode)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "SS";
        }
        if (TierRoll < 0.001f && Main.hardMode)
        {
            item.GetGlobalItem<GlobalTier>().itemTier = "SSS";
        }
    }
    #endregion
    // ================================================================== //
    #region TierSaveData
    public override void SaveData(Item item, TagCompound tag)
    {
        tag["tierKey"] = tierKey;
    }
    public override void NetSend(Item item, BinaryWriter writer)
    {
        writer.Write(tierKey);
    }
    public override void NetReceive(Item item, BinaryReader reader)
    {
        int receivedTierKey = reader.ReadInt32();
        // Process the received data and update the fields
        tierKey = receivedTierKey;

    }

    public override void LoadData(Item item, TagCompound tag)
    {
        tierKey = tag.GetInt("tierKey");
    }
    #endregion

}