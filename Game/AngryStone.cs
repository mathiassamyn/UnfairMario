using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class AngryStone : MovingEnemy
    {
        int startY, startX;
        bool drop;
        
        public AngryStone(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\enemies\restingStone.png");
            startX = x;
            startY = y;
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Image.Width, Image.Height);
            Velocity = 25;
            CollDet = new CollisionDetection();
            
            drop = false;
        }

        public override void UpdatePosition(GameLevel level)
        {
            if (level.hero.position.X + level.hero.Width >= X)
            {
                drop = true;
            }
            if (drop)
            {
                Image = new Surface(@"Sprites\enemies\angryStone.png");
                Y += Velocity;
                Collrect = new Rectangle(X, Y, Image.Width, Image.Height);
                int block = CollDet.CollDetection(Collrect, "down", level);
                if (block >= 0)
                {
                    //Y = block - Image.Height;
                    Y -= block;
                    level.spriteArray[startX, startY] = new AngryStoneGround(X, Y, Video); 
                    drop = false;
                }
            }
        }
    }
}
