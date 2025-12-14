using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class SharkEnemy : Enemy
{
    public SharkEnemy(Texture2D texture, Vector2 pos)
        : base(texture, pos, "Shark")
    {
        Damage = 20;
        AttackRange = 40f;
        movementSpeed = 3.5f;
    }

    public override void Update(GameTime gameTime, Player player, Stage stage)
    {
        if (!stage.IsWater(Position))
            return; // Shark is harmless outside water

        base.Update(gameTime, player, stage);
    }
}
