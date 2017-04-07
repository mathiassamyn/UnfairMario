using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class Sprites
    {
        public Surface Image { get; set; }
        public Surface Video { get; set; }

        public Rectangle Collrect { get; set; }


        public int X { get; set; }
        public int Y { get; set; }

        public int Width = 33;
        public int Height = 33;


        public virtual void Draw()
        {
            Video.Blit(Image, new Point(X, Y));
        }
    }
}
