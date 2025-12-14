using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class PowerUp : Item
{
    public float Duration { get; private set; }     // How long the effect lasts
    public bool IsActive { get; private set; }      // Whether it’s currently active
    private float timer;                            // Time remaining for effect

    public Action<Player> ApplyEffect;              // Function to apply effect to player
    public Action<Player> RemoveEffect;             // Function to remove effect from player

    private Rectangle srcRect;                      // Source rectangle from sprite sheet

    public PowerUp(string name, Texture2D texture, Rectangle position, float duration,
                   Action<Player> applyEffect, Action<Player> removeEffect = null,
                   Rectangle? sourceRect = null)
        : base(name, texture, position, true)
    {
        Duration = duration;
        ApplyEffect = applyEffect;
        RemoveEffect = removeEffect;
        srcRect = sourceRect ?? new Rectangle(0, 0, 32, 32); // default if not provided
    }

    public override void Use(Player player)
    {
        if (IsUsable && !IsActive)
        {
            IsActive = true;
            timer = Duration;
            ApplyEffect?.Invoke(player);
            Console.WriteLine($"{Name} activated!");
        }
    }

    public override void Update(GameTime gameTime, Player player)
    {
        if (IsActive)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0)
            {
                IsActive = false;
                RemoveEffect?.Invoke(player);
                Console.WriteLine($"{Name} expired.");
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, srcRect, Color.White);
    }
}