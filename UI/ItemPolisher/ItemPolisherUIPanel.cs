using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace quality.UI.ItemPolisher;

public class ItemPolisherUIPanel : UIPanel
{
    /*
    private Vector2 offset;
    // keep track of whether the panel is being dragged or not
    private bool dragging;

    public override void Click(UIMouseEvent evt)
    {
        base.Click(evt);
        DragStart(evt);

    }
    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);
        DragEnd(evt);
    }
    private void DragStart(UIMouseEvent evt)
    {
        offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
        dragging = true;
    }
    private void DragEnd(UIMouseEvent evt)
    {
        Vector2 FinalMousePosition = evt.MousePosition;
        dragging = false;
        Left.Set(FinalMousePosition.X - offset.X, 0f);
        Top.Set(FinalMousePosition.Y - offset.Y, 0f);
        Recalculate();
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }

        if (Main.mouseLeft && dragging)
        {
            Left.Set(Main.mouseX - offset.X, 0f);
            Top.Set(Main.mouseY - offset.Y, 0f);
            Recalculate();
        }
        else
        {
            dragging = false;
        }

        var parentSpace = Parent.GetDimensions().ToRectangle();
        if (!GetDimensions().ToRectangle().Intersects(parentSpace))
        {
            Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
            Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
            Recalculate();
        }
    }
    */
}
