using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Humanizer;

public class GlobalQuality : GlobalItem {
    public int quality;
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;
    public static bool BrokenQuality = false;
    // Assign the custom field quality to *this* item (the actual instance of an item)
    public override void SetDefaults(Item item)
    {
        if (IsItemValid(item)) {
            item.GetGlobalItem<GlobalQuality>().quality = quality;
        }
        
    }
    // Tooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (IsItemValid(item)) { 
            tooltips.Add(new TooltipLine(Mod, "quality", $"Quality: {quality}%") { OverrideColor = Color.BlueViolet });
            if (IsBrokenItem(item)) {
                if (IsArmorBroken(item)) {
                    if (IsItemValid(item) && IsArmorBroken(item))
                    {
                        item.defense = 0;
                    }
                    tooltips.Add(new TooltipLine(Mod, "broken", "Broken") {OverrideColor = Color.Gray});
                    tooltips.Add(new TooltipLine(Mod, "NoDefense", $"{item.defense} defense") {OverrideColor = Color.Gray});
                    TooltipLine lineToRemove = tooltips.Find(line => line.Name == "quality");
                    TooltipLine removeDefense = tooltips.Find(line => line.Name == "Defense");
                    // Removing quality tooltip from broken items
                    if (lineToRemove != null || removeDefense != null)
                    {
                    tooltips.Remove(lineToRemove);
                    tooltips.Remove(removeDefense);
                    } 
                }
            }
        }       
    }
    // Cannot use item if broken
    public override bool CanUseItem(Item item, Player player)
    {
        if (IsItemValid(item)) {
            if (IsBrokenItem(item)) {
                return false;
            } 
        } else return true;
        return true;
    }
    // Cannot equip accessory if broken
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (IsItemValid(item)) {
            if (IsBrokenItem(item)) {
                return false;
            } 
        } else return true;
        return true;
    }
    public static bool IsItemValid(Item item) {
        if (item.damage > 0 || item.defense > 0) {
            return true;
        } else return false;
    }

    public static bool IsArmorBroken(Item item) {
        if (item.defense > 0 && IsBrokenItem(item)) {
            return true;
        }  else return false;
    }

    // Checks if an item is broken
    public static bool IsBrokenItem(Item item) {
        if (item.GetGlobalItem<GlobalQuality>().quality < 30) {
            return true;
        } else return false;
    }

    // Assign random quality during item spawn (chests/drop/bags)
    public override void OnSpawn(Item item, IEntitySource source)
    {
        quality = Main.rand.Next(0, 100+1);
        //ArmorPenalty(item);
    }
    // Assign random quality during creation (craft)
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        quality = Main.rand.Next(0, 100+1);
        //ArmorPenalty(item);
    }
    // Saving quality data with TagCompound object
    public override void SaveData(Item item, TagCompound tag)
    {
        tag["quality"] = quality;
    }
    // Loading quality data with TagCompound object
    public override void LoadData(Item item, TagCompound tag)
    {
        quality = tag.GetInt("quality");
    }

}
 