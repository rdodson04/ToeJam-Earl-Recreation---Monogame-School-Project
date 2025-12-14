using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject
{
    public Texture2D Texture { get; protected set; }

    // Used for movement and positioning
    public virtual Vector2 Position { get; protected set; }

    // Used for collision and drawing (especially for items)
    public virtual Rectangle BoundingBox { get;}

    public virtual void Draw(SpriteBatch spriteBatch) { }
}
