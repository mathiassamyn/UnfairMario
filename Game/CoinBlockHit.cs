using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class CoinBlockHit : Sprites
    {

        public CoinBlockHit(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\randomBlocks\coinBlockHit.png");
            X = x;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
        }
    }
}
