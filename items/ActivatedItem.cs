using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class ActivatedItem : Item
{
    public Action<Player> ActivateAction { get; private set; }

    public ActivatedItem(string name, Texture2D texture, Rectangle position, Action<Player> activateAction)
        : base(name, texture, position, true)
    {
        ActivateAction = activateAction;
    }

    public override void Use(Player player)
    {
        Console.WriteLine($"{Name} activated from inventory!");
        ActivateAction?.Invoke(player);
    }
}
