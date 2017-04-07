using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Spikes : Enemy
    {

        string path;

        public Spikes(int x, int y, Surface video, string path)
        {
            Video = video;
            this.path = path;
            Image = new Surface(@"Sprites\randomBlocks\empty.png");     
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
        }

        public void ShowSpikes()
        {
            Image = new Surface(path);
        }
    }
}
