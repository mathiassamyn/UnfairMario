using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Sand : Sprites
    {
        //public Rectangle Collrect; //temp

        public Sand(int x, int y, Surface video, string path)
        {
            this.Video = video;
            Image = new Surface(path);
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
        }

        
    }
}
