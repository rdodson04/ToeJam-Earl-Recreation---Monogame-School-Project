using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StartScreen
{
    private readonly SpriteFont _font;
    private readonly Texture2D _bg;

    public StartScreen(SpriteFont font, Texture2D background)
    {
        _font = font;
        _bg = background;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_bg != null)
            spriteBatch.Draw(_bg, new Rectangle(0, 0, 1280, 720), Color.White);

        spriteBatch.DrawString(_font, "ToeJam & Earl - Prototype", new Vector2(80, 120), Color.Yellow);
        spriteBatch.DrawString(_font, "Press Enter to Start", new Vector2(80, 200), Color.White);
    }
}