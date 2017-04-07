using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class CoinBlock : Sprites
    {
        Rectangle visibleRectangle;


        public CoinBlock(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\randomBlocks\questionMarkBlock.png");
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
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

        public void CoinBlockHit(GameLevel level, int j, int i)
        {
            level.spriteArray[j, i] = new CoinBlockHit(level.spriteArray[j, i].X, i, level.video);
            level.spriteArray[j, i - 1] = new Coin(level.spriteArray[j, i].X, i - 1, level.video);
        }
    }
}
