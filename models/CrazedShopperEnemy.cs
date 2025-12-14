using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class CrazedShopperEnemy : Enemy
{
    private float burstTimer = 0f;

    public CrazedShopperEnemy(Texture2D texture, Vector2 pos)
        : base(texture, pos, "CrazedShopper")
    {
        Damage = 15;
        AttackRange = 35f;
    }

    public override void Update(GameTime gameTime, Player player, Stage stage)
    {
        burstTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (burstTimer <= 0f)
        {
            movementSpeed = 6f;
            burstTimer = 1.5f;
        }
        else
        {
            movementSpeed = 2f;
        }

        base.Update(gameTime, player, stage);
    }
}
