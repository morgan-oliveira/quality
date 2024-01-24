using quality.UI.ItemPolisher;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;


namespace quality.UI.ItemPolisher;
public class ItemPolisherUI : UIState
{
    public ItemPolisherUIPanel ItemPolisherPanel;
    private UIImageButton CloseButton;
    private UIImageButton itemSlotButton;
    private Item[] singleSlotItem = Main.LocalPlayer.inventory; // Item Array to store data about our item in UIItemSlot
    private UIItemSlot testeItemSlot;
    public override void OnInitialize()
    {
        ItemPolisherPanel = new ItemPolisherUIPanel();
        ItemPolisherPanel.SetPadding(0);
        ItemPolisherPanel.VAlign = 0.5f;
        ItemPolisherPanel.HAlign = 0.9f;
        ItemPolisherPanel.Height.Set(100, 0f);
        ItemPolisherPanel.Width.Set(400, 0f);
        //ItemPolisherPanel.BackgroundColor = Color.DeepPink;

        /* This part of the code is not very well documented and I couldn't find much information about it, so the explanation
        regarding how it works could be misleading. Please bear with me.
        By using Main.LocalPlayer.inventory as the item array, you are essentially creating a link between the UI and the 
        player's inventory. This link allows the UI to display and interact with the items in real-time. If the player obtains 
        a new item, drops an item, or performs any action that changes the inventory, the UIItemSlot will automatically update 
        to reflect these changes.
        In summary, it works because Main.LocalPlayer.inventory provides a live reference to the player's inventory, 
        allowing your UI to dynamically display and interact with the items in the inventory array.
        The UIItemSlot is a subclass of UIElement, and it represents any slot in which you can store items.
        It's constructor requires an Item type array (most likely to store the item's instance data), the item index and the
        itemContext.
        I don't know exactly what the item index is, so I just set it to 1.
        The itemContext part is defined within the ItemSlot class, inside Terraria.UI. For this specific case (in which I
        only needed something similar to the Goblin Tinkerer's UI), I set it to 5 (which is the itemContext for the 
        Goblin Tinkerer indeed).
        If you need more info about itemContext, decompile your Terraria with ILSpy, or check the ItemContext.txt file.
        */
        testeItemSlot = new UIItemSlot(singleSlotItem, 1, 5);  
        testeItemSlot.SetPadding(0);
        testeItemSlot.VAlign = testeItemSlot.HAlign = 0.5f;

        itemSlotButton = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/Reforge_1"));
        itemSlotButton.SetPadding(0);
        itemSlotButton.VAlign = 0.5f;
        itemSlotButton.HAlign = 0.8f;
        itemSlotButton.OnClick += itemSlotButtonClicked;

        CloseButton = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete"));
        CloseButton.SetPadding(0);
        CloseButton.VAlign = 0.1f;
        CloseButton.HAlign = 0.01f;
        CloseButton.Height.Set(25, 0f);
        CloseButton.Width.Set(25, 0f);
        CloseButton.OnClick += CloseButtonClicked;

        
        ItemPolisherPanel.Append(itemSlotButton);
        ItemPolisherPanel.Append(CloseButton);
        ItemPolisherPanel.Append(testeItemSlot);
        Append(ItemPolisherPanel);

    }

    private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
    {
        ModContent.GetInstance<ItemPolisherSystem>().HideMyUI();
    }
    private void itemSlotButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
        Main.NewText(singleSlotItem[1].GetGlobalItem<GlobalQuality>().quality);
    }
}

