using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

public class GlobalNPCBuff : GlobalNPC
{
    public static bool NightmareMode = false;
    public static bool InfernoMode = false;
    public static int MoonlordCounter = 0;
    public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
    {
        if (npc.boss && NightmareMode)
        {
            target.AddBuff(BuffID.OnFire, 180);
            target.AddBuff(BuffID.Poisoned, 180);
            target.AddBuff(BuffID.ShadowFlame, 180);
        }
    }
    public override bool CheckDead(NPC npc)
    {
        if (npc.type == NPCID.MoonLordCore)
        {
            if (!NightmareMode)
            {
                NightmareMode = true;
                Main.hardMode = false;
            }
            else if (NightmareMode)
            {
                NightmareMode = false;
                InfernoMode = true;
                Main.hardMode = false;
            }
            return true;
        }
        return true;
    }
    // Modifies NPC stats, depending on the game state.
    public void NPCModifiers(NPC npc)
    {
        if (NightmareMode)
        {
            if (npc.active && npc.boss && !Main.hardMode)
            {
                npc.life *= 10;
                npc.lifeMax *= 10;
                npc.damage *= 10;
                npc.defense *= 10;
            }
            else if (!Main.hardMode)
            {
                npc.damage *= 6;
                npc.defense *= 6;
                npc.lifeMax *= (int)3.5f;
                npc.life *= (int)3.5f;
            }
            else
            {
                npc.damage *= 3;
                npc.defense *= 3;
                npc.lifeMax *= (int)2.2f;
                npc.life *= (int)2.2f;
            }
        }
        if (InfernoMode)
        {
            if (npc.active && npc.boss && !Main.hardMode)
            {
                npc.life *= 20;
                npc.lifeMax *= 20;
                npc.damage *= 45;
                npc.defense *= 20;
                npc.scale = 1.7f;
                npc.velocity *= 4f;
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
                npc.damage *= 7;
                npc.defense *= 10;
                npc.lifeMax *= (int)3.5f;
                npc.life *= (int)3.5f;
            }
        }
        if (NightmareMode)
        {
            Main.NewText($"You are now in Nightmare mode! Inferno: {InfernoMode}", 16, 76, 200);
        }
        else if (InfernoMode)
        {
            Main.NewText("You are now in Inferno mode!", 253, 27, 45);
        }
    }
    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        NPCModifiers(npc);
    }
}