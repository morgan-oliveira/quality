using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Drawing;
using Microsoft.Xna.Framework;

public class GlobalSkill : GlobalItem
{
    private static float SkillProb;
    private bool hasFireSkill, hasPoisonSkill, hasCursedFlameSkill, hasIchorSkill;
    private int BuffDuration = 60;
    private int SkillLevel = 1;
    private static int SkillAmount;
    public Dictionary<int, String> RomanSkillLevel = new Dictionary<int, String>{
        {1, "I"}, {2, "II"}, {3, "III"}, {4, "IV"}, {5, "V"}, {6, "VI"}, {7, "VII"}, {8, "VIII"}, {9, "IX"}, {10, "X"}
    };
    private String SkillName;
    private bool ItemHaveSkill;
    private Microsoft.Xna.Framework.Color SkillNameColor;

    protected override bool CloneNewInstances => true;
    public override bool InstancePerEntity => true;
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (SkillName == "Fire")
        {
            SkillNameColor = Microsoft.Xna.Framework.Color.Red;
        }
        if (SkillName == "Ichor")
        {
            SkillNameColor = Microsoft.Xna.Framework.Color.Green;
        }
        if (SkillName == "Cursed Flames")
        {
            SkillNameColor = Microsoft.Xna.Framework.Color.YellowGreen;
        }
        if (SkillName == "Poison")
        {
            SkillNameColor = Microsoft.Xna.Framework.Color.ForestGreen;
        }
        if (SkillChecker(item))
        {
            tooltips.Add(new TooltipLine(Mod, "skill", $"{SkillName} {RomanSkillLevel[item.GetGlobalItem<GlobalSkill>().SkillLevel]}") { OverrideColor = SkillNameColor });

            /*
            for (int i = 1; i <= RomanSkillLevel.Count; i++) {
                Main.NewText($"{item.GetGlobalItem<GlobalSkill>().RomanSkillLevel[i]}");
            }
            */
        }
    }
    public static void RollSkill(Item item)
    {
        SkillProb = Main.rand.NextFloat();
        if (SkillProb < 0.9f)
        {
            float ChooseSkill = Main.rand.NextFloat();
            if (item.damage > 0 || item.defense > 0)
            {
                if (ChooseSkill < 0.7f)
                {
                    AddFireSkill(item);
                }
                if (ChooseSkill < 0.5f)
                {
                    AddPoisonSkill(item);
                }
                if (ChooseSkill < 0.3f)
                {
                    AddCursedInfernoSkill(item);
                }
                if (ChooseSkill < 0.1f)
                {
                    AddIchorSkill(item);
                }
            }
        }
        if (SkillProb < 0.5f)
        {
            if (item.GetGlobalItem<GlobalTier>().itemTier == "B")
            {
                float ChooseSkill = Main.rand.NextFloat();
                if (ChooseSkill < 0.5f)
                {
                    AddCursedInfernoSkill(item);
                }
                if (ChooseSkill < 0.2f)
                {
                    AddIchorSkill(item);
                }
            }
        }
        if (SkillProb < 0.2)
        {
            if (item.GetGlobalItem<GlobalTier>().itemTier == "A")
            {
                float ChooseSkill = Main.rand.NextFloat();
                if (ChooseSkill < 0.7f)
                {
                    AddCursedInfernoSkill(item);
                }
                if (ChooseSkill < 0.5f)
                {
                    AddIchorSkill(item);
                }
            }
        }
    }
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        RollSkill(item);
        RollSkillLevel(item);
    }
    public override void OnSpawn(Item item, IEntitySource source)
    {
        RollSkill(item);
        RollSkillLevel(item);

    }
    public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
    {
        if (item.GetGlobalItem<GlobalSkill>().hasFireSkill)
        {
            target.AddBuff(BuffID.OnFire, item.GetGlobalItem<GlobalSkill>().SkillLevel * BuffDuration);
        }
        if (item.GetGlobalItem<GlobalSkill>().hasPoisonSkill)
        {
            target.AddBuff(BuffID.Poisoned, item.GetGlobalItem<GlobalSkill>().SkillLevel * BuffDuration);
        }
        if (item.GetGlobalItem<GlobalSkill>().hasCursedFlameSkill)
        {
            target.AddBuff(BuffID.CursedInferno, item.GetGlobalItem<GlobalSkill>().SkillLevel * BuffDuration);
        }
        if (item.GetGlobalItem<GlobalSkill>().hasIchorSkill)
        {
            target.AddBuff(BuffID.Ichor, item.GetGlobalItem<GlobalSkill>().SkillLevel * BuffDuration);
        }
    }
    public static void RollSkillLevel(Item item)
    {
        if (item.GetGlobalItem<GlobalTier>().itemTier == "D" || item.GetGlobalItem<GlobalTier>().itemTier == "C")
        {
            item.GetGlobalItem<GlobalSkill>().SkillLevel = Main.rand.Next(1, 2);
            return;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "B")
        {
            item.GetGlobalItem<GlobalSkill>().SkillLevel = Main.rand.Next(2, 3);
            return;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "A")
        {
            item.GetGlobalItem<GlobalSkill>().SkillLevel = Main.rand.Next(3, 4);
            return;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "S")
        {
            item.GetGlobalItem<GlobalSkill>().SkillLevel = Main.rand.Next(4, 6);
            SkillAmount = 2;
            return;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "SS")
        {
            item.GetGlobalItem<GlobalSkill>().SkillLevel = Main.rand.Next(6, 8);
            SkillAmount = Main.rand.Next(2, 3 + 1);
            return;
        }
        if (item.GetGlobalItem<GlobalTier>().itemTier == "SSS")
        {
            item.GetGlobalItem<GlobalSkill>().SkillLevel = Main.rand.Next(8, 10 + 1);
            SkillAmount = Main.rand.Next(2, 4 + 1);
            return;
        }
    }
    #region Skills

    public static void AddFireSkill(Item item)
    {
        item.GetGlobalItem<GlobalSkill>().SkillName = "Fire";
        item.GetGlobalItem<GlobalSkill>().hasFireSkill = true;
    }
    public static void AddPoisonSkill(Item item)
    {
        item.GetGlobalItem<GlobalSkill>().SkillName = "Poison";
        item.GetGlobalItem<GlobalSkill>().hasPoisonSkill = true;
    }
    public static void AddCursedInfernoSkill(Item item)
    {
        item.GetGlobalItem<GlobalSkill>().SkillName = "Cursed Flames";
        item.GetGlobalItem<GlobalSkill>().hasCursedFlameSkill = true;
    }
    public static void AddIchorSkill(Item item)
    {
        item.GetGlobalItem<GlobalSkill>().SkillName = "Ichor";
        item.GetGlobalItem<GlobalSkill>().hasIchorSkill = true;
    }
    // checking if item has any of the custom skills
    public static bool SkillChecker(Item item)
    {
        if (item.GetGlobalItem<GlobalSkill>().hasIchorSkill || item.GetGlobalItem<GlobalSkill>().hasFireSkill || item.GetGlobalItem<GlobalSkill>().hasPoisonSkill || item.GetGlobalItem<GlobalSkill>().hasCursedFlameSkill)
        {
            return true;
        }
        else return false;
    }

    #endregion
}