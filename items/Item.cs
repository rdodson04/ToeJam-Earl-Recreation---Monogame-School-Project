using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Item : GameObject
{
    public string Name { get; private set; }
    public bool IsUsable { get; protected set; }
    private Rectangle itemRect;
    public virtual void Update(GameTime gameTime, Player player) { }

    public Item(string name, Texture2D texture, Rectangle position, bool isUsable = true)
    {
        Name = name;
        Texture = texture;
        itemRect = position;
        IsUsable = isUsable;
    }
    public override Vector2 Position => itemRect.Location.ToVector2();
    public override Rectangle BoundingBox => itemRect;
    public virtual void Use(Player player)
    {
        if (IsUsable)
        {
            Console.WriteLine($"{Name} was used!");
        }
    }
}