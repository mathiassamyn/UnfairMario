using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class EnemyShell : MovingEnemy
    {
        Rectangle visibleRectangle;
        int gravity;

        public EnemyShell(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\enemies\shellAnimation.png");
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
            Velocity = -15;
            CollDet = new CollisionDetection();
            visibleRectangle = new Rectangle(0, 0, Width, Height);
            gravity = 0;
        }

        public override void Draw()
        {
            Video.Blit(Image, new Point(X, Y), visibleRectangle);
            visibleRectangle.X += Width;
            if (visibleRectangle.X >= Image.Width)
            {
                visibleRectangle.X = 0;
            }
        }

        public override void UpdatePosition(GameLevel level)
        {
            X += Velocity;          
            Collrect = new Rectangle(X, Y, Width, Height);
            
            if (Velocity > 0)
            {
                int block = CollDet.CollDetection(Collrect, "right", level);
                if (block >= 0)
                {
                    X -= block; 
                    Velocity *= -1;                   
                }
            } else
            {
                int block = CollDet.CollDetection(Collrect, "left", level);
                if (block >= 0)
                {
                    X += block; 
                    Velocity *= -1;
                }
            }

            Y += gravity;
            Collrect = new Rectangle(X, Y, Width, Height);

            int blockY = CollDet.CollDetection(Collrect, "down", level);
            if (blockY >= 0)
            {
                //Y = blockY - Height;
                Y -= blockY;
                Collrect = new Rectangle(X, Y, Width, Height);
                gravity = 0;
            } else
            {
                gravity += 5;
            }


        }

       
    }
}
