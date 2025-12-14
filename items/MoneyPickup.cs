using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class MoneyPickup : Item
{
    public int Amount { get; private set; }

    public MoneyPickup(string name, Texture2D texture, Rectangle position, int amount)
        : base(name, texture, position, true)
    {
        Amount = amount;
    }

    public override void Use(Player player)
    {
        player.Currency += Amount;
        Console.WriteLine($"{Name} picked up {Amount} coins! Total: {player.Currency}");
        IsUsable = false; // consumed
    }
}
