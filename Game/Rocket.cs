using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Rocket : MovingEnemy
    {
        int xVelocity;
        int yVelocity;
        bool rocketDown;

        public Rocket(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\enemies\rocket.png");
            X = x;
            Y = y;
            Collrect = new Rectangle(X, Y, Width, Height);
            xVelocity = 15;
            yVelocity = 0;
            CollDet = new CollisionDetection();
            rocketDown = false;
        }

        public int UpdatePosition(GameLevel level, bool checkHit = false)
        {            

            if (!checkHit) // nodig zodat de raket niet 2maal zo snel gaat wanneer er gesprongen wordt
            {
                X -= xVelocity;
                
                Y += yVelocity;
             
                Collrect = new Rectangle(X, Y, Width, Height);
            }


           int heroHit = -5;


               if (!rocketDown)
               {
                   if (checkHit)
                   {
                       heroHit = CollDet.CollDetection(Collrect, "heroDown", level);
                   }
                   else
                   {
                       CollDet.CollDetection(Collrect, "heroNotDown", level);
                   }
               }


               int block = CollDet.CollDetection(Collrect, "left", level);
               if (block >= 0 || heroHit >= 0)
               {
                   Image = new Surface(@"Sprites\enemies\rocketDown.png");
                   xVelocity = 0;
                   yVelocity = 30;
                   rocketDown = true;
      
               }

            return heroHit;

        }
    }
}
