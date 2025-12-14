using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TornadoEnemy : Enemy
{
    public TornadoEnemy(Texture2D texture, Vector2 pos)
        : base(texture, pos, "Tornado")
    {
        Damage = 0;
        AttackRange = 60f;
        AttackCooldown = 1.5f;
    }

    protected override void Attack(Player player)
    {
        Vector2 knockback =
            Vector2.Normalize(player.Position - Position) * 30f;

        player.ApplyKnockback(knockback);
        attackTimer = AttackCooldown;
    }
}
