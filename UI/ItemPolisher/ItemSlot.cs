using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace quality.UI.ItemPolisher;

public class ItemSlot : UIElement {
    private UIImageButton slotImage;
    private Item storedItem;

    public ItemSlot() {
        slotImage = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/Reforge_0"));
    }
}