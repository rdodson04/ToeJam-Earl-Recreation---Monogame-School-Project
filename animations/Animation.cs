using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

public class AnimatedSprite
{
    private Texture2D texture;
    private Dictionary<string, Rectangle[]> animations;
    private string currentAnimation;
    private int currentFrame;
    private float frameTimer;
    private readonly float timePerFrame;

    public Vector2 Position { get; set; }
    public float Scale { get; set; }
    public SpriteEffects Effects { get; set; }

    public AnimatedSprite(Texture2D texture, Dictionary<string, Rectangle[]> animations, float scale = 1f, float frameDuration = 0.1f)
    {
        this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
        this.animations = animations ?? throw new ArgumentNullException(nameof(animations));
        Scale = scale;
        timePerFrame = frameDuration;
        Effects = SpriteEffects.None;

        // Default to first animation if none specified
        currentAnimation = animations.Keys.FirstOrDefault() ?? throw new InvalidOperationException("No animations provided.");
    }

    public void Play(string animationName)
    {
        if (currentAnimation == animationName || !animations.ContainsKey(animationName))
            return;

        currentAnimation = animationName;
        currentFrame = 0;
        frameTimer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        if (!animations.ContainsKey(currentAnimation)) return;

        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (frameTimer >= timePerFrame)
        {
            frameTimer -= timePerFrame;
            currentFrame = (currentFrame + 1) % animations[currentAnimation].Length;
        }
    }

    
    public void Draw(SpriteBatch spriteBatch)
    {
        if (spriteBatch == null)
            throw new ArgumentNullException(nameof(spriteBatch));
        if (!animations.TryGetValue(currentAnimation, out var frames) || frames.Length == 0)
            return;

        Rectangle source = frames[currentFrame];
        spriteBatch.Draw(
            texture,
            Position,
            source,
            Color.White,
            rotation: 0f,
            origin: Vector2.Zero,
            scale: Scale,
            effects: Effects,
            layerDepth: 0f
        );
    }


    // --- NEW: Swap animations or texture dynamically ---
    public void SetAnimations(Dictionary<string, Rectangle[]> newAnimations)
    {
        if (newAnimations == null || newAnimations.Count == 0)
            throw new ArgumentException("Animations cannot be empty.", nameof(newAnimations));

        animations = newAnimations;
        currentAnimation = newAnimations.Keys.First();
        currentFrame = 0;
        frameTimer = 0f;
    }

    public void SetSprite(Texture2D newTexture, Dictionary<string, Rectangle[]> newAnimations)
    {
        texture = newTexture ?? throw new ArgumentNullException(nameof(newTexture));
        SetAnimations(newAnimations);
    }
    public Rectangle GetCurrentFrame()
    {
        if (animations.TryGetValue(currentAnimation, out var frames))
            return frames[currentFrame];
        return Rectangle.Empty;
    }
}
