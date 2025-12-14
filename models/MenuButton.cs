using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MenuButton
{
    public string Label { get; }
    private Texture2D texture;
    private Vector2 position;
    private Rectangle bounds;
    public bool IsClicked { get; private set; }

    public MenuButton(string label, Texture2D tex, Vector2 pos)
    {
        Label = label;
        texture = tex;
        position = pos;
        bounds = new Rectangle(pos.ToPoint(), new Point(tex.Width, tex.Height));
    }

    public void Update(MouseState mouse)
    {
        Point mousePos = mouse.Position;
        IsClicked = bounds.Contains(mousePos) && mouse.LeftButton == ButtonState.Pressed;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}