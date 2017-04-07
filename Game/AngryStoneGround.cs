using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class AngryStoneGround : Sprites
    {
        public AngryStoneGround(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\enemies\angryStone.png");
            X = x;
            Y = y;
            Width = Image.Width;
            Height = Image.Height;
            Collrect = new Rectangle(X, Y, Width, Height);
        }
    }
}
