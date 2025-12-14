using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Projectile
{
    public Vector2 Position;
    public Vector2 Velocity;
    public int Damage;
    public bool IsActive = true;

    public Rectangle Bounds =>
        new Rectangle((int)Position.X, (int)Position.Y, 8, 8);

    public void Update(GameTime gameTime)
    {
        Position += Velocity;

        if (Position.X < 0 || Position.Y < 0)
            IsActive = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Placeholder – draw nothing or debug box
    }
}
