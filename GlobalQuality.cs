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
    public static bool ArmorBroken = false;
    public float ItemRoll;
    // Assign the custom field quality to *this* item (the actual instance of an item)
    public override void SetDefaults(Item item)
    {
        if (IsItemValid(item)) {
            item.GetGlobalItem<GlobalQuality>().quality = quality;
        }
        
    }
    // ================================================================================= //
    #region ItemTooltips
    // Tooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (IsItemValid(item)) { 
            tooltips.Add(new TooltipLine(Mod, "quality", $"Quality: {quality}%") { OverrideColor = Color.BlueViolet });
            if (IsBrokenItem(item)) {
                if (IsArmorBroken(item)) {
                    // Set defense to zero when item has defense but is broken
                    if (IsArmorBroken(item))
                    {
                        item.defense = 0;
                        // Broken flag to address double "Broken" tooltip issue
                        ArmorBroken = true;
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
                // Checks if Broken flag is true
                } else if (ArmorBroken) {
                    tooltips.Add(new TooltipLine(Mod, "broken", "Broken") {OverrideColor = Color.Gray});
                    TooltipLine weaponRemove = tooltips.Find(line => line.Name == "quality");
                    if (weaponRemove != null) {
                    tooltips.Remove(weaponRemove);
                    }
                }
            }
        }       
    }
    #endregion
    // ============================================================================ //
    #region Probability
    public void RollQuality(Item item) {
        ItemRoll = Main.rand.NextFloat();
        if (ItemRoll <= 0.8f) {
            quality = Main.rand.Next(30, 50+1); // 80% chance of getting item quality in 30-50 range, inclusive
        }
        if (ItemRoll <= 0.5f) {
            quality = Main.rand.Next(51, 60+1); // 50% chance of getting item quality in 50-60 range, inclusive
        }
        if (ItemRoll <= 0.3f) {
            quality = Main.rand.Next(61, 65+1); // 30% chance of getting item quality in 60-65 range, inclusive
        }
        if (ItemRoll <= 0.2f) {
            quality = Main.rand.Next(66, 70+1); // 20% chance of getting item quality in 65-70 range, inclusive
        }
        if (ItemRoll <= 0.05f) {
            quality = Main.rand.Next(71, 75+1); // 5% chance of getting item quality in 70-75 range, inclusive
        }
        if (ItemRoll <= 0.01f) {
            quality = Main.rand.Next(76, 80+1); // 1% chance of getting item quality in 75-80 range, inclusive
        }
        if (ItemRoll <= 0.005f) {
            quality = Main.rand.Next(81, 85+1); // 0.5% chance of getting item quality in 80-85 range, inclusive
        }
        if (ItemRoll <= 0.0001f) {
            quality = Main.rand.Next(86, 90+1); // 0.01% chance of getting item quality in 85-90 range, inclusive
        }
        if (ItemRoll <= 0.00001f) {
            quality = Main.rand.Next(91, 95+1); // 0.001% chance of getting item quality in 90-95 range, inclusive
        }
        if (ItemRoll <= 0.000001f) {
            quality = Main.rand.Next(96, 99+1); // 0.0001% chance of getting item quality in 90-99 range, inclusive
        }
        if (ItemRoll <= 0.0000001f) {
            quality = 100; // 0.00001% chance of getting item quality 100.
        }
    }
    #endregion
    // ============================================================================ //
    #region ItemRestriction
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
    #endregion
    // ====================================================================== //
    #region ItemCreation

    // Assign random quality during item spawn (chests/drop/bags)
    public override void OnSpawn(Item item, IEntitySource source)
    {
        RollQuality(item);
        //ArmorPenalty(item);
    }
    // Assign random quality during creation (craft)
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        RollQuality(item);
        //ArmorPenalty(item);
    }

    #endregion
    // ========================================================================= //
    #region DataSave
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
    #endregion
}
 