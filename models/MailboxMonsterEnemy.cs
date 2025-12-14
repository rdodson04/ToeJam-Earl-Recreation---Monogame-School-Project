using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class MailboxMonsterEnemy : Enemy
{
    public MailboxMonsterEnemy(Texture2D texture, Vector2 pos)
        : base(texture, pos, "MailboxMonster")
    {
        DetectionRadius = 30f;
        Damage = 25;
    }
}
