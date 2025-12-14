using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Present : Item
{
    public bool IsOpened { get; private set; }
    private Item containedItem;

    public Present(string name, Texture2D texture, Rectangle position, Item itemInside)
        : base(name, texture, position, true)
    {
        containedItem = itemInside;
        IsOpened = false;
    }

    public override void Use(Player player)
    {
        if (!IsOpened)
        {
            Open(player);
        }
    }

    private void Open(Player player)
    {
        IsOpened = true;
        Console.WriteLine($"{Name} opened!");

        // Add contained item to player’s inventory
        player.Inventory.Add(containedItem);
        Console.WriteLine($"{containedItem.Name} was added to your inventory!");
    }

    public override void Update(GameTime gameTime, Player player)
    {
        if (!IsOpened && player.BoundingBox.Intersects(BoundingBox))
        {
            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                Use(player);
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsOpened)
            spriteBatch.Draw(Texture, Position, Color.White);
    }
}
