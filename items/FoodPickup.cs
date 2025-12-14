using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class FoodPickup : Item
{
    public int HealthChange { get; private set; }

    public FoodPickup(string name, Texture2D texture, Rectangle position, int healthChange)
        : base(name, texture, position, true)
    {
        HealthChange = healthChange;
    }

    public override void Use(Player player)
    {
        player.Health += HealthChange;
        Console.WriteLine($"{Name} ate {Name}. Health changed by {HealthChange}. Current health: {player.Health}");
        IsUsable = false;
    }
}
