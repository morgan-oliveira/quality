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
public class GlobalTier : GlobalItem {
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;
    public List<String> tiers = new List<String>() {"D", "C", "B", "A", "S", "SS", "SSS"};
    public List<String> levels = new List<string>() {"I", "II", "III", "IV", "V", "VI"};
    public String itemTier;
    public String skillLevel;
     int tierKey;
    int skillKey;
    public float SkillProb;
    // ================================================================== //
    #region Skills
    public bool hasFireSkill;
    public static bool hasIceSkill;
    public static bool hasWaterSkill;
    public static bool hasLifeSkill;
    public static bool hasManaSkill;
    public static bool hasMSSKill;
    public static bool hasEarthSkill;
    public static bool hasIchorSkill;
    public static bool hasCursedFlameSkill;
    #endregion
    // ================================================================== //
    public override void SetDefaults(Item item)
    {
        item.GetGlobalItem<GlobalTier>().itemTier = itemTier;
        item.GetGlobalItem<GlobalTier>().skillLevel = skillLevel;
    }
    // ================================================================== //
    #region TierItemCreation
    public override void OnSpawn(Item item, IEntitySource source)
    {
        tierKey = Main.rand.Next(0, tiers.Count);
        skillKey = Main.rand.Next(0, levels.Count);
        RollSkill(item);
    }
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        tierKey = Main.rand.Next(0, tiers.Count);
        skillKey = Main.rand.Next(0, levels.Count);
        RollSkill(item);
    }
    #endregion
    // ================================================================== //
    #region TierTooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        
        if ((item.damage > 0) || (item.defense > 0)) {
                itemTier = tiers[tierKey];
                if (!GlobalQuality.IsBrokenItem(item)) {
                    tooltips.Add(new TooltipLine(Mod, "itemTier", $"Tier: {itemTier}") {OverrideColor = Color.Gold});
                }
        }
        if (item.GetGlobalItem<GlobalTier>().hasFireSkill) {
            skillLevel = levels[skillKey];
            tooltips.Add(new TooltipLine(Mod, "FireSkillLevelOne", $"[Fire {skillLevel}]"){OverrideColor = Color.OrangeRed});
        }
    }
    #endregion
    // ================================================================== //
    #region TierModifiers
    public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
    {
        if (item.GetGlobalItem<GlobalTier>().hasFireSkill) {
            target.AddBuff(BuffID.OnFire, 60);
        }
    }
    public void SetFireSkill(Item item) {
        itemTier = tiers[tierKey];
        if (item.GetGlobalItem<GlobalTier>().itemTier == "D" || item.GetGlobalItem<GlobalTier>().itemTier == "C" || item.GetGlobalItem<GlobalTier>().itemTier == "B") {
            item.GetGlobalItem<GlobalTier>().hasFireSkill = true;
        }
        
    }
    #endregion
    // ================================================================== //
    #region SkillProbability
    public void RollSkill(Item item) {
    
        SkillProb = Main.rand.NextFloat();
        if (SkillProb < 0.7f) {
            SetFireSkill(item);
            Main.NewText($"{item.GetGlobalItem<GlobalTier>().hasFireSkill}");
        }
    }
    #endregion
    // ================================================================== //
    #region TierSaveData
    public override void SaveData(Item item, TagCompound tag)
    {
        tag["tierKey"] = tierKey;
        tag["skillKey"] = skillKey;
        tag["skillLevel"] = skillLevel;
        tag["hasFireSkill"] = hasFireSkill;
    }

    public override void LoadData(Item item, TagCompound tag)
    {
        tierKey = tag.GetInt("tierKey");
        skillKey = tag.GetInt("skillKey");
        skillLevel = tag.GetString("skillLevel");
        hasFireSkill = tag.GetBool("hasFireSkill");
    }
    #endregion

}