using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

public class QualityPolisherUI : UIState
{
    private UIPanel blueWindowPanel;

    public override void OnInitialize()
    {
        blueWindowPanel = new UIPanel();
        blueWindowPanel.Width.Set(300f, 0f);
        blueWindowPanel.Height.Set(200f, 0f);
        blueWindowPanel.BackgroundColor = new Color(0, 0, 255); // Blue color

        blueWindowPanel.Left.Set(Main.screenWidth / 2 - blueWindowPanel.Width.Pixels / 2, 0f);
        blueWindowPanel.Top.Set(Main.screenHeight / 2 - blueWindowPanel.Height.Pixels / 2, 0f);
        Append(blueWindowPanel);
    }

}