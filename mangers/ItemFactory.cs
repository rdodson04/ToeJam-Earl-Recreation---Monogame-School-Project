using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public static class ItemFactory
{
    private static Dictionary<string, Texture2D> textures = new();
    private static Random rng = new Random();

    public static void RegisterTexture(string key, Texture2D texture)
    {
        if (!textures.ContainsKey(key))
            textures[key] = texture;
    }

    // Factory Methods
    public static Telephone CreateTelephone(Rectangle position) =>
        new Telephone("Telephone", textures["Telephone"], position);

    public static ActivatedItem CreateActivatedItem(Rectangle position, string name, Action<Player> effect) =>
        new ActivatedItem(name, textures["ActivatedItem"], position, effect);

    public static MoneyPickup CreateMoney(Rectangle position, int amount) =>
        new MoneyPickup("Money", textures["Money"], position, amount);

    public static FoodPickup CreateFood(Rectangle position, string name, int healthChange) =>
        new FoodPickup(name, textures["Food"], position, healthChange);

    public static Present CreatePresent(Rectangle position, Item containedItem) =>
        new Present("Present", textures["Present"], position, containedItem);

    public static PowerUp CreatePowerUp(Rectangle position, string name, float duration,
                                        Action<Player> applyEffect, Action<Player> removeEffect = null) =>
        new PowerUp(name, textures["PowerUp"], position, duration, applyEffect, removeEffect);

    // -------------------------------
    // Random Item Generator (for Present contents or spawner use)
    // -------------------------------
    public static Item CreateRandomItem(Rectangle position)
    {
        int roll = rng.Next(3);
        if (roll == 0)
            return CreateFood(position, "Good Food", +15);

        if (roll == 1)
            return CreateMoney(position, 10);

        return CreatePowerUp(position, "Speed Boost", 5f,
            p => p.MovementSpeed *= 2,
            p => p.MovementSpeed /= 2);
    }
}