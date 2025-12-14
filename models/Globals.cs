using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public static class Globals {

    public static float TotalSeconds { get; set;    }
    public static ContentManager Content { get; set; }

    public static void update(GameTime gt) {
        TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
    }
}