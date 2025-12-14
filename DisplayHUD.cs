using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DisplayHUD
{
    private Dictionary<string, Rectangle> elements;
    public DisplayHUD()
    {
        elements = new Dictionary<string, Rectangle>
        {
            {"NoPlayerMenu", new Rectangle(8, 48, 20, 20)},
            {"EarlBar", new Rectangle(180, 88, 20, 20)},
            {"ToeJamMenu", new Rectangle(341, 15, 20, 20)},
            {"EarlMenu", new Rectangle(467, 364, 20, 20)},
            {"Present", new Rectangle(2, 9, 20, 20)},
            {"Food", new Rectangle(2, 70, 20, 20)},
        };
    }

    public Rectangle Get(string key)
    {
        return elements.ContainsKey(key) ? elements[key] : Rectangle.Empty;
    }
    
}