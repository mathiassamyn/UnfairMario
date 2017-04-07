using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class IntroScreen : Mouse
    {
        private Surface video;
        private Surface title;
        Surface un;
        Rectangle visibleRectangle;
        Surface mario;
        Point position, unPosition, playPoint;

        public bool ended;
        SdlDotNet.Graphics.Font playTxt;
        Surface play;

        public IntroScreen(Surface video)
        {
            title = new Surface(@"Sprites\intro\FairMario.png");
            mario = new Surface(@"Sprites\intro\introLeft.png");
            un = new Surface(@"Sprites\intro\UN.png");
            unPosition = new Point(250, 0);
            position = new Point(0, 120);
            visibleRectangle = new Rectangle(0, 0, 171, mario.Height);
            this.video = video;
            ended = false;
            playTxt = new SdlDotNet.Graphics.Font(@"Sprites\8-BIT WONDER.TTF", 35);
            play = playTxt.Render("Start game", Color.Black);
            playPoint = new Point(video.Width / 2 - play.Width / 2, video.Height / 2 - 3 * play.Height / 2);
        }


        public override void ApplicationMouseButtonEventHandler(object sender, MouseButtonEventArgs args)
        {
            
            if (args.Button == MouseButton.PrimaryButton && cursorPosition.X >= playPoint.X && cursorPosition.X <= playPoint.X + play.Width && cursorPosition.Y >= playPoint.Y && cursorPosition.Y <= playPoint.Y + play.Height)
            {
                ended = true;
            }
               
        }

        public void titleDraw()
        {

            video.Fill(Color.White);

            video.Blit(title, new Point(250, 25));
            video.Blit(play, playPoint);
        }

        public void marioDraw()
        {
            video.Blit(mario, position, visibleRectangle);
            visibleRectangle.X += 171;
            if (visibleRectangle.X >= mario.Width)
            {
                visibleRectangle.X = 0;
            }
            position.X += 40;
            if (position.X >= 280)
            {
                position.X = 280;
                position.Y = 193;
                mario = new Surface(@"Sprites\intro\marioDOWN.png");
                unDraw();
            }

        }

        private void unDraw()
        {
            video.Blit(un, unPosition);


            unPosition.Y += 30;
            if (unPosition.Y >= 25)
            {
                unPosition.Y = 25;
            }
        }
    }
}
