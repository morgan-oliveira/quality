using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

public class GlobalSkill : GlobalItem {
    private static float SkillProb;
    private bool hasFireSkill;
    private int BuffDuration;
    private static int SkillLevel;
    private static int SkillAmount;
    public Dictionary<int, String> RomanSkillLevel = new Dictionary<int, String>{
        {1, "I"}, {2, "II"}, {3, "III"}, {4, "IV"}, {5, "V"}, {6, "VI"}, {7, "VII"}, {8, "VIII"}, {9, "IX"}, {10, "X"}
    };
    private String SkillName;
    private bool ItemHaveSkill;

    protected override bool CloneNewInstances => true;
    public override bool InstancePerEntity => true;
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (ItemHaveSkill) {
            tooltips.Add(new TooltipLine(Mod, "skill", $"{SkillName} {SkillLevel}"));
        }
    }

    public static void RollSkill(Item item) {
        SkillProb = Main.rand.NextFloat();
        if (SkillProb < 0.7f) {
            if (item.GetGlobalItem<GlobalTier>().itemTier == "D" || item.GetGlobalItem<GlobalTier>().itemTier == "C") {
                item.GetGlobalItem<GlobalSkill>().ItemHaveSkill = true;
                float ChooseSkill = Main.rand.NextFloat();
                if (ChooseSkill < 0.7f) {
                    //AddFireSkill(item);
                }
            }
        }
        if (SkillProb < 0.5f) {

        }
    }
    public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
    {
        if (item.GetGlobalItem<GlobalSkill>().hasFireSkill) {
            target.AddBuff(BuffID.OnFire, BuffDuration);
        }
    }
    public static void RollSkillLevel(Item item) {
        if (item.GetGlobalItem<GlobalTier>().itemTier == "D" || item.GetGlobalItem<GlobalTier>().itemTier == "C") {
            SkillLevel = Main.rand.Next(8, 10+1);
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "B") {
            SkillLevel = Main.rand.Next(7, 10+1);
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "A") {
            SkillLevel = Main.rand.Next(6, 10+1);
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "S") {
            SkillLevel = Main.rand.Next(4, 9);
            SkillAmount = 2;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "SS") {
            SkillLevel = Main.rand.Next(2, 6+1);
            SkillAmount = Main.rand.Next(2, 3+1);
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "SSS") {
            SkillLevel = Main.rand.Next(1, 6);
            SkillAmount = Main.rand.Next(2,4+1);
        }
    }
    #region Skills

    public void AddFireSkill(Item item, NPC target) {
        SkillName = "Fire";
        target.AddBuff(BuffID.OnFire, 60 * SkillLevel);
    }
    public void AddPoisonSkill(Item item, NPC target) {
        SkillName = "Poison";
        target.AddBuff(BuffID.Poisoned, SkillLevel);
    }
    public void AddCursedInfernoSkill(Item item, NPC target) {
        SkillName = "Cursed Flames";
        target.AddBuff(BuffID.CursedInferno, SkillLevel);
    }
    public void AddIchorSkill(Item item, NPC target) {
        SkillName = "Ichor";
        target.AddBuff(BuffID.Ichor, SkillLevel);
    }

    #endregion
}