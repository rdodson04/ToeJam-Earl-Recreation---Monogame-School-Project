using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class PlayerAnimations
{
    public static Dictionary<string, Rectangle[]> GetAnimations(string playerType)
    {
        return playerType switch
        {
            "ToeJam" => new Dictionary<string, Rectangle[]>
            {
                ["Sneak"] = new Rectangle[]
                {
                    new Rectangle(21, 200, 30, 30),
                    new Rectangle(49, 200, 30, 30),
                    new Rectangle(81, 200, 30, 30)
                },

                ["Down"] = new Rectangle[]
                {
                    new Rectangle(18, 79, 30, 30),
                    new Rectangle(50, 79, 30, 30),
                    new Rectangle(83, 79, 30, 30),
                    new Rectangle(119, 79, 30, 30),
                    new Rectangle(150, 81, 30, 30),
                    new Rectangle(183, 82, 30, 30)
                },

                ["Left"] = new Rectangle[]
                {
                    new Rectangle(400, 84, 30, 30),
                    new Rectangle(367, 84, 30, 30),
                    new Rectangle(332, 81, 30, 30),
                    new Rectangle(229, 81, 30, 30),
                    new Rectangle(266, 81, 30, 30),
                    new Rectangle(232, 82, 30, 30)
                },

                ["Right"] = new Rectangle[]
                {
                    new Rectangle(240, 130, 30, 30),
                    new Rectangle(270, 130, 30, 30),
                    new Rectangle(305, 130, 30, 30),
                    new Rectangle(334, 130, 30, 30),
                    new Rectangle(366, 130, 30, 30),
                    new Rectangle(399, 128, 30, 30)
                },
                ["Up"] = new Rectangle[]
                {
                    new Rectangle(20, 129, 30, 30),
                    new Rectangle(56, 129, 30, 30),
                    new Rectangle(87, 129, 30, 30),
                    new Rectangle(121, 129, 30, 30),
                    new Rectangle(153, 129, 30, 30),
                    new Rectangle(185, 129, 30, 30)
                },
                ["Idle"] = new Rectangle[]
                {
                    new Rectangle(11, 13, 30, 30),
                    new Rectangle(43, 11, 30, 30),
                    new Rectangle(76, 12, 30, 30)
                }
            },

            "Earl" => new Dictionary<string, Rectangle[]>
            {
                ["Sneak"] = new Rectangle[]
                {
                    new Rectangle(21, 200, 30, 30),
                    new Rectangle(49, 200, 30, 30),
                    new Rectangle(81, 200, 30, 30)
                },

                ["Down"] = new Rectangle[]
                {
                    new Rectangle(18, 79, 30, 30),
                    new Rectangle(50, 79, 30, 30),
                    new Rectangle(83, 79, 30, 30),
                    new Rectangle(119, 79, 30, 30),
                    new Rectangle(150, 81, 30, 30),
                    new Rectangle(183, 82, 30, 30)
                },

                ["Left"] = new Rectangle[]
                {
                    new Rectangle(400, 84, 30, 30),
                    new Rectangle(367, 84, 30, 30),
                    new Rectangle(332, 81, 30, 30),
                    new Rectangle(229, 81, 30, 30),
                    new Rectangle(266, 81, 30, 30),
                    new Rectangle(232, 82, 30, 30)
                },

                ["Right"] = new Rectangle[]
                {
                    new Rectangle(240, 130, 30, 30),
                    new Rectangle(270, 130, 30, 30),
                    new Rectangle(305, 130, 30, 30),
                    new Rectangle(334, 130, 30, 30),
                    new Rectangle(366, 130, 30, 30),
                    new Rectangle(399, 128, 30, 30)
                },
                ["Up"] = new Rectangle[]
                {
                    new Rectangle(20, 129, 30, 30),
                    new Rectangle(56, 129, 30, 30),
                    new Rectangle(87, 129, 30, 30),
                    new Rectangle(121, 129, 30, 30),
                    new Rectangle(153, 129, 30, 30),
                    new Rectangle(185, 129, 30, 30)
                },
                ["Idle"] = new Rectangle[]
                {
                    new Rectangle(11, 13, 30, 30),
                    new Rectangle(43, 11, 30, 30),
                    new Rectangle(76, 12, 30, 30)
                }
            },

            _ => throw new ArgumentException($"Unknown player type: {playerType}")
        };
    }
}