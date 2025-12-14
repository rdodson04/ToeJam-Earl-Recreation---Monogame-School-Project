using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CupidEnemy : Enemy
{
    public CupidEnemy(Texture2D texture, Vector2 pos)
        : base(texture, pos, "Cupid")
    {
        Damage = 5;
        AttackRange = 200f;
        AttackCooldown = 2.0f;
    }

    protected override void Attack(Player player)
    {
        Vector2 dir = Vector2.Normalize(player.Position - Position);

        Projectile heart = new Projectile
        {
            Position = Position,
            Velocity = dir * 5f,
            Damage = Damage
        };

        GameManager.Instance.SpawnProjectile(heart);
        attackTimer = AttackCooldown;
    }
}
