using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Telephone : Item
{
    public Telephone(string name, Texture2D texture, Rectangle position)
        : base(name, texture, position, isUsable: true) { }

    public override void Use(Player player)
    {
        Console.WriteLine($"{Name} rings! Player {Name} answered.");
        // Give currency or trigger event
        player.Currency += 10;
        Console.WriteLine($"{Name} earned 10 coins from answering the phone!");
    }
}
