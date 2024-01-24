using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

public class GlobalNPCBuff : GlobalNPC
{
    public static bool NightmareMode = false;
    public static bool InfernoMode = false;
    public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
    {
        if (npc.boss && NightmareMode)
        {
            target.AddBuff(BuffID.OnFire, 180);
            target.AddBuff(BuffID.Poisoned, 180);
            target.AddBuff(BuffID.ShadowFlame, 180);
        }
    }
    public override void OnKill(NPC npc)
    {
        // If the NPC being killed is the Moonlord, we make all the verifications to ensure Nightmare/Inferno is started
        if (npc.type == NPCID.MoonLordCore)
        { 
            if (!NightmareMode && !InfernoMode)
            {
                NightmareMode = true;
                Main.hardMode = false;
                Main.NewText($"You are now in Nightmare mode!", Color.DeepPink);
                Main.NewText($"The world has been resetted to pre-hardmode!", Color.Aquamarine);
            }
            else if (NightmareMode)
            {
                NightmareMode = false;
                InfernoMode = true;
                Main.hardMode = false;
                Main.NewText("You are now in Inferno mode!", Color.OrangeRed);
                Main.NewText($"The world has been resetted to pre-hardmode again!", Color.Aquamarine);
            }
        }

    }
    // Modifies NPC stats, depending on the game state.
    public void NPCModifiers(NPC npc)
    {
        if (NightmareMode)
        {
            if (npc.active && npc.boss && !Main.hardMode)
            {
                npc.life *= 20;
                npc.lifeMax *= 20;
                npc.damage *= 8;
                npc.defense *= 15;
            }
            else if (!Main.hardMode)
            {
                npc.damage *= 6;
                npc.defense *= 10;
                npc.lifeMax *= (int)5.5f;
                npc.life *= (int)5.5f;
            }
            else
            {
                npc.damage *= 12;
                npc.defense *= 15;
                npc.lifeMax *= (int)6.2f;
                npc.life *= (int)6.2f;
            }
        }
        if (InfernoMode)
        {
            if (npc.active && npc.boss && !Main.hardMode)
            {
                npc.life *= 30;
                npc.lifeMax *= 30;
                npc.damage *= 15;
                npc.defense *= 30;
                npc.scale = 2.2f;
                npc.velocity *= 8f;
            }
            else if (!Main.hardMode)
            {
                npc.damage *= 9;
                npc.defense *= 9;
                npc.lifeMax *= (int)4.5f;
                npc.life *= (int)4.5f;
            }
            else
            {
                npc.damage *= 16;
                npc.defense *= 25;
                npc.lifeMax *= (int)7.5f;
                npc.life *= (int)7.5f;
            }
        }
    }
    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        NPCModifiers(npc);
    }
    public override void SaveData(NPC npc, TagCompound tag)
    {
        tag["NightmareMode"] = NightmareMode;
        tag["InfernoMode"] = InfernoMode;
    }
    public override void LoadData(NPC npc, TagCompound tag)
    {
        NightmareMode = tag.GetBool("NightmareMode");
        InfernoMode = tag.GetBool("InfernoMode");
    }
}