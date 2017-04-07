using SdlDotNet.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SdlDotNet.Core;

namespace Game
{
    class Mouse
    {
        public Point cursorPosition;

        public Mouse()
        {
            Events.MouseMotion += new EventHandler<MouseMotionEventArgs>(ApplicationMouseMotionEventHandler);
            Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(ApplicationMouseButtonEventHandler);
        }

        public virtual void ApplicationMouseButtonEventHandler(object sender, MouseButtonEventArgs args)
        {
            
        }

        private void ApplicationMouseMotionEventHandler(object sender, MouseMotionEventArgs args)
        {
            cursorPosition = args.Position;
            cursorPosition.X -= 6; // Because the Visual Pointer of the Cursor is 6 pixels inside


        }
    }
}
