using System.Collections.Generic;
using Microsoft.Xna.Framework;
using quality.UI.ItemPolisher;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

public class ItemPolisherSystem : ModSystem
{
    private UserInterface ItemPolisherUserInterface;
    internal ItemPolisherUI ItemPolisherDisplay;

    public void ShowMyUI()
    {
        ItemPolisherUserInterface?.SetState(ItemPolisherDisplay);
    }
    public void HideMyUI()
    {
        ItemPolisherUserInterface?.SetState(null);
    }
    public override void Load()
    {

        ItemPolisherDisplay = new ItemPolisherUI();
        ItemPolisherDisplay.Activate();
        ItemPolisherUserInterface = new UserInterface();
        //ItemPolisherUserInterface.SetState(ItemPolisherDisplay);
    }
    public override void UpdateUI(GameTime gameTime)
    {
        ItemPolisherUserInterface?.Update(gameTime);

    }
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "quality Mod: Diablo-Like",
                delegate
                {
                    if (ItemPolisherUserInterface?.CurrentState != null)
                    {
                        ItemPolisherUserInterface.Draw(Main.spriteBatch, new GameTime());
                    }
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}