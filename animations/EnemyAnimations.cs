using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class EnemyAnimations
{
    public static Dictionary<string, Rectangle[]> GetAnimations(string enemyType)
    {
        return enemyType switch
        {
            "Goblin" => new Dictionary<string, Rectangle[]>
            {
                ["Down"] = new[]
                {
                    new Rectangle(10, 5, 30, 30),
                    new Rectangle(56, 4, 30, 30),
                    new Rectangle(103, 5, 30, 30)
                },
                ["Left"] = new[]
                {
                    new Rectangle(7, 52, 30, 30),
                    new Rectangle(55, 51, 30, 30),
                    new Rectangle(102, 54, 30, 30),
                    new Rectangle(150, 52, 30, 30)
                },
                ["Right"] = new[]
                {
                    new Rectangle(7, 52, 30, 30),
                    new Rectangle(55, 60, 30, 30),
                    new Rectangle(102, 54, 30, 30),
                    new Rectangle(150, 52, 30, 30)
                },
                ["Up"] = new[]
                {
                    new Rectangle(6, 100, 30, 30),
                    new Rectangle(53, 101, 30, 30),
                    new Rectangle(101, 101, 30, 30)
                }
            },
            "Orc" => new Dictionary<string, Rectangle[]>
            {
                ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                }
            },
            "Cupid" => new Dictionary<string, Rectangle[]>
            {
                ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                }
            },
            "Angry Bees" => new Dictionary<string, Rectangle[]>
            {
               ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                } 
            },
            "Tornado" => new Dictionary<string, Rectangle[]>
            {
               ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                } 
            },
            "Shark" => new Dictionary<string, Rectangle[]>
            {
              ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                }  
            },
            "Crazed Shopper" => new Dictionary<string, Rectangle[]>
            {
              ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                }  
            },
            "Mole" => new Dictionary<string, Rectangle[]>
            {
               ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                } 
            },
            "Fat Mole & Mower" => new Dictionary<string, Rectangle[]>
            {
                ["Down"] = new[]
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32)
                },
                ["Up"] = new[]
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32)
                }
            },
            _ => throw new ArgumentException($"Unknown enemy type: {enemyType}")
        };
    }
}