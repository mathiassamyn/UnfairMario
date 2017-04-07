using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Cannon : Sprites
    {

        public Cannon(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\enemies\cannon.png");
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
        }
    }
}
