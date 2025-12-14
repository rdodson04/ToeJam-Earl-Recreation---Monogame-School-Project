using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ToeJamEarl;

public class Player : GameObject
{
    // -----------------------------
    // CORE REFERENCES
    // -----------------------------
    private Game1 root;
    private Vector2 position;             
    private AnimatedSprite animatedSprite;

    public override Vector2 Position => position;   

    // -----------------------------
    // MOVEMENT
    // -----------------------------
    public float MovementSpeed { get; set; } = 4.0f;
    private readonly float sneakMultiplier = 0.5f;
    private bool isSneaking = false;

    // -----------------------------
    // STATS
    // -----------------------------
    public int Currency { get; set; } = 0;
    public int Health { get; set; } = 100;
    public bool IsDead { get; private set; } = false;

    public const int MaxInventorySize = 20;

    // -----------------------------
    // INVENTORY
    // -----------------------------
    public List<Item> Inventory { get; } = new();

    // -----------------------------
    // CONSTRUCTOR
    // -----------------------------
    public Player(Game1 root, Texture2D spriteTexture, Vector2 startPosition, string playerType)
    {
        this.root = root;
        position = startPosition;

        var animations = PlayerAnimations.GetAnimations(playerType);
        animatedSprite = new AnimatedSprite(spriteTexture, animations, scale: 4.0f);
        animatedSprite.Position = position;
    }

    // =====================================================
    // UPDATE LOOP
    // =====================================================
    public void Update(GameTime gameTime, Stage stage)
    {
        if (IsDead)
        {
            animatedSprite.Play("Idle");
            return;
        }

        if (stage.IsHole(position))
        {
            Console.WriteLine("Player fell into a hole!");
            GameManager.Instance.PlayerFellInHole(this);
            return;
        }

        HandleInput(gameTime, stage);
        UpdatePowerUps(gameTime);
    }

    // =====================================================
    // INPUT & MOVEMENT
    // =====================================================
    private void HandleInput(GameTime gameTime, Stage stage)
    {
        InputManager.Update();

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            animatedSprite.Play("Idle");
            return;
        }

        Vector2 moveDir = InputManager.GetMovementVector();

        if (moveDir != Vector2.Zero)
        {
            isSneaking = InputManager.isSneakButtonPressed();
            float speed = isSneaking ? MovementSpeed * sneakMultiplier : MovementSpeed;

            Vector2 newPos = position + moveDir * speed;

            if (!stage.IsBlocked(newPos))
                position = newPos;

            string animation = InputManager.GetPrimaryDirection() switch
            {
                InputManager.Direction.Up => "Up",
                InputManager.Direction.Down => "Down",
                InputManager.Direction.Left => "Left",
                InputManager.Direction.Right => "Right",
                _ => "Idle"
            };

            animatedSprite.Play(animation);
        }
        else
        {
            animatedSprite.Play("Idle");
        }

        animatedSprite.Position = position;
        animatedSprite.Update(gameTime);
    }

    // =====================================================
    // DAMAGE & DEATH
    // =====================================================
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        Health = Math.Max(0, Health - amount);

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        IsDead = true;
        animatedSprite.Play("Idle");
        GameManager.Instance.TriggerGameOver();
    }

    // =====================================================
    // POWER UPS
    // =====================================================
    private void UpdatePowerUps(GameTime gameTime)
    {
        foreach (var item in Inventory)
        {
            if (item is PowerUp powerUp)
                powerUp.Update(gameTime, this);
        }
    }

    // =====================================================
    // DRAW
    // =====================================================
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsDead)
            animatedSprite.Draw(spriteBatch);
    }

    // =====================================================
    // RESET
    // =====================================================
    public void Reset()
    {
        position = Vector2.Zero;
        animatedSprite.Position = position;

        Currency = 0;
        Health = 100;
        IsDead = false;

        Inventory.Clear();
    }

    // =====================================================
    // INVENTORY
    // =====================================================
    public bool TryPickup(Item item)
    {
        if (!BoundingBox.Intersects(item.BoundingBox)) return false;
        if (Inventory.Count >= MaxInventorySize) return false;

        Inventory.Add(item);
        return true;
    }

    

    // =====================================================
    // PROPERTIES
    // =====================================================
    public override Rectangle BoundingBox =>
        new Rectangle(position.ToPoint(), animatedSprite.GetCurrentFrame().Size);

    public bool IsSneaking => isSneaking;

    public void ApplyKnockback(Vector2 force)
    {
        position += force;
    }
}
