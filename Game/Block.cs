using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Block : Sprites
    {
        public Block(int x, int y, Surface video)
        {
            Video = video;
            Image = new Surface(@"Sprites\randomBlocks\blockTry.png"); // TODO: Image veranderen
            X = x * Width;
            Y = y * Height;
            Collrect = new Rectangle(X, Y, Width, Height);
        }
    }
}
