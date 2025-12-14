using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera2D
{
    public Matrix Transform { get; private set; }
    public Vector2 Position { get; private set; }
    public Viewport Viewport { get; }

    public Camera2D(Viewport viewport)
    {
        Viewport = viewport;
        Position = Vector2.Zero;
    }

    public void Follow(Vector2 target)
    {
        Position = target - new Vector2(Viewport.Width / 2, Viewport.Height / 2);
        Transform = Matrix.CreateTranslation(new Vector3(-Position, 0));
    }

    public Matrix GetViewMatrix()
    {
        return Transform;
    }

    public Rectangle GetVisibleArea()
    {
        return new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            Viewport.Width,
            Viewport.Height
        );
    }
}
