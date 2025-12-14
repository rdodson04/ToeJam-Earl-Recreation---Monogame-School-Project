using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Enemy : GameObject
{
    // -----------------------------
    // POSITION
    // -----------------------------
    private Vector2 _position;
    public override Vector2 Position => _position;

    public override Rectangle BoundingBox =>
        new Rectangle(_position.ToPoint(), animatedSprite.GetCurrentFrame().Size);

    // -----------------------------
    // AI
    // -----------------------------
    private readonly Queue<Vector2> patrolNodes = new();
    private Vector2 currentTarget;

    public float DetectionRadius { get; set; } = 150f;
    protected float movementSpeed = 2.0f;

    // -----------------------------
    // COMBAT (NEW)
    // -----------------------------
    public int Damage { get; protected set; } = 10;
    public float AttackRange { get; protected set; } = 32f;
    public float AttackCooldown { get; protected set; } = 1.0f;

    public float attackTimer = 0f;

    // -----------------------------
    // STATE
    // -----------------------------
    public bool IsActive { get; private set; } = true;
    public bool IsDead { get; private set; } = false;

    // -----------------------------
    // RENDERING
    // -----------------------------
    private AnimatedSprite animatedSprite;

    // -----------------------------
    // CONSTRUCTOR
    // -----------------------------
    public Enemy(Texture2D spriteTexture, Vector2 startPosition, string enemyType)
    {
        _position = startPosition;

        var animations = EnemyAnimations.GetAnimations(enemyType);
        animatedSprite = new AnimatedSprite(spriteTexture, animations, scale: 4.0f);
        animatedSprite.Position = _position;

        currentTarget = startPosition;
        IsActive = true;
    }

    // -----------------------------
    // PATROL
    // -----------------------------
    public void AddPatrolNode(Vector2 node)
    {
        patrolNodes.Enqueue(node);
        if (currentTarget == Vector2.Zero)
            currentTarget = node;
    }

    // -----------------------------
    // UPDATE
    // -----------------------------
    public virtual void Update(GameTime gameTime, Player player, Stage stage)
    {
        if (!IsActive || IsDead || player == null) return;

        attackTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        float distanceToPlayer = Vector2.Distance(player.Position, _position);
        Vector2 dir = Vector2.Zero;

        // Chase player
        if (distanceToPlayer <= DetectionRadius)
        {
            dir = Vector2.Normalize(player.Position - _position);

            // ATTACK
            if (distanceToPlayer <= AttackRange && attackTimer <= 0f)
            {
                Attack(player);
            }
        }
        // Patrol
        else if (patrolNodes.Count > 0)
        {
            if (Vector2.Distance(_position, currentTarget) < 5f)
            {
                currentTarget = patrolNodes.Dequeue();
                patrolNodes.Enqueue(currentTarget);
            }

            dir = Vector2.Normalize(currentTarget - _position);
        }

        Vector2 newPos = _position + dir * movementSpeed;

        if (!stage.IsBlocked(newPos))
            _position = newPos;

        animatedSprite.Position = _position;
        animatedSprite.Update(gameTime);
    }

    // -----------------------------
    // ATTACK
    // -----------------------------
    protected virtual void Attack(Player player)
    {
        player.TakeDamage(Damage);
        attackTimer = AttackCooldown;
    }

    // -----------------------------
    // DEATH
    // -----------------------------
    public void Kill()
    {
        if (IsDead) return;
        IsDead = true;
        IsActive = false;
    }

    // -----------------------------
    // DRAW
    // -----------------------------
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        animatedSprite.Draw(spriteBatch);
    }
}
