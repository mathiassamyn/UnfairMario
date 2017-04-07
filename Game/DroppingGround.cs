using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class DroppingGround : MovingEnemy
    {
        public DroppingGround(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\enemies\groundDropping.png");
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Image.Width, Image.Height);
            Velocity = 0;
        }

        /*public override void UpdatePosition(GameLevel level)
        {         
            if (level.hero.position.X >= X && level.hero.position.X + level.hero.Width <= X + Image.Width && level.hero.position.Y + level.hero.Height >= Y + 10)
            {
                level.hero.gravity = 0;
                //level.hero.position.Y = Y - level.hero.Height;
                Velocity = 40;
            }
            Y += Velocity;
            Collrect = new Rectangle(X, Y, Image.Width, Image.Height);
        }*/
    }
}
