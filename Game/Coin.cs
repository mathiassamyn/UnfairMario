using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Coin : Sprites
    {
        Rectangle visibleRectangle;

        public Coin(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\randomBlocks\coin.png");            
            X = x;
            Y = y * Height;
            visibleRectangle = new Rectangle(0, 0, Width, Height);
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
    }
}
