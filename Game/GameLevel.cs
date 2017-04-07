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
    class GameLevel : Mouse
    {
        public _2_ViewMapEditor.MapModel map;
        public Sprites[,] spriteArray;
        public Surface video;
        public Hero hero;
        public List<Rocket> rocketList = new List<Rocket>();
        SdlDotNet.Graphics.Font font;
        Surface coinCountSurface;
        public int CoinCount;
        Surface coinSingle;
        Surface text;
        public bool ended;
        Point menuPoint;
        public int level;
   


        public GameLevel(Surface video, string path, int level)
        {
            this.video = video;
            map = new _2_ViewMapEditor.MapModel(1, 1);
            map.LoadMap(path);
            spriteArray = new Sprites[map.Breedte, map.Hoogte];
            DrawBackground();
            hero = new Hero(video, video.Width/2 -20, 495);
            CoinCount = 0;
            font = new SdlDotNet.Graphics.Font(@"Sprites\8-BIT WONDER.TTF", 35);
            ended = false;
            coinSingle = new Surface(@"Sprites\randomBlocks\coinSingle.png");
            //text = font.Render("", Color.White);
            //menuPoint = new Point(video.Width / 2 - text.Width / 2, video.Height / 2 + text.Height * 2);
            this.level = level;
        }

        private void DrawBackground()
        {
            for (int i = 0; i < map.Hoogte; i++)
            {
                for (int j = 0; j < map.Breedte; j++)
                {

                    switch (map.GetElement(j, i))
                    {
                        case 0:

                            break;
                        case 1:
                            spriteArray[j, i] = new Grass(j, i, video, @"Sprites\grass\grass.png");
                            break;
                        case 2:
                            spriteArray[j, i] = new Grass(j, i, video, @"Sprites\grass\grassRight.png");
                            break;
                        case 3:
                            spriteArray[j, i] = new Grass(j, i, video, @"Sprites\grass\grassLeft.png");
                            break;
                        case 4:
                            spriteArray[j, i] = new Grass(j, i, video, @"Sprites\grass\grassCornerRight.png");
                            break;
                        case 5:
                            spriteArray[j, i] = new Grass(j, i, video, @"Sprites\grass\grassCornerLeft.png");
                            break;
                        case 6:
                            spriteArray[j, i] = new Sand(j, i, video, @"Sprites\sand\sand.png");
                            break;
                        case 7:
                            spriteArray[j, i] = new Sand(j, i, video, @"Sprites\sand\sandBorderLeft.png");
                            break;
                        case 8:
                            spriteArray[j, i] = new Sand(j, i, video, @"Sprites\sand\sandBorderRight.png");
                            break;
                        case 9:
                            spriteArray[j, i] = new Sand(j, i, video, @"Sprites\sand\sandCornerRight.png");
                            break;
                        case 10:
                            spriteArray[j, i] = new Sand(j, i, video, @"Sprites\sand\sandCornerLeft.png");
                            break;
                        case 11:
                            spriteArray[j, i] = new Finish(j, i, video);
                            break;
                        case 12:
                            spriteArray[j, i] = new CoinBlock(j, i, video);
                            break;
                        case 13:
                            spriteArray[j, i] = new InvisibleWall(j, i, video);
                            break;
                        case 14:
                            spriteArray[j, i] = new Cannon(j, i, video);
                            break;
                        case 15:
                            spriteArray[j,i] = new EnemyShell(j, i, video);
                            break;
                        case 16:
                            spriteArray[j, i] = new Spikes(j, i, video, @"Sprites\enemies\spikesUp.png");
                            break;
                        case 17:
                            spriteArray[j, i] = new Spikes(j, i, video, @"Sprites\enemies\spikesDown.png");
                            break;
                        case 18:
                            spriteArray[j, i] = new Spikes(j, i, video, @"Sprites\enemies\spikesRight.png");
                            break;
                        case 19:
                            spriteArray[j, i] = new Spikes(j, i, video, @"Sprites\enemies\spikesLeft.png");
                            break;
                        case 20:
                            spriteArray[j, i] = new Block(j, i, video);
                            break;
                        case 21:
                            spriteArray[j, i] = new AngryStone(j, i, video);
                            break;
                        case 22:
                            spriteArray[j, i] = new DroppingGround(j, i, video);
                            break;

                    }
                }
            }


        }

        int tellerCannon = 0;

        public void Update()
        {

            

            if (!hero.win)
            {
                hero.Update(this);
            }

            if (!hero.dead)
            {
                for (int i = 0; i < rocketList.Count; i++)
                {
                    rocketList[i].UpdatePosition(this);
                }
                for (int i = 0; i < rocketList.Count; i++)
                {
                    if (rocketList[i].Y >= video.Height)
                    {
                        rocketList.Remove(rocketList[i]);
                    }
                }
            }

            if (!hero.dead)
            {

                IEnumerable<MovingEnemy> movingEnemyList = spriteArray.OfType<MovingEnemy>();
                foreach (MovingEnemy movingEnemy in movingEnemyList)
                {
                    movingEnemy.UpdatePosition(this);
                }
                tellerCannon++;
                IEnumerable<Cannon> cannonList = spriteArray.OfType<Cannon>();
                foreach (Cannon cannon in cannonList)
                {
                    if (tellerCannon % 20 == 0)
                    {
                        rocketList.Add(new Rocket(cannon.X - cannon.Width, cannon.Y, video));
                    }
                }
               
            }


           
            

            coinCountSurface = font.Render(Convert.ToString(CoinCount), Color.White);          

            

        }
        

        public void Draw()
        {

            hero.Draw();
            for (int i = 0; i < map.Hoogte; i++)
            {
                for (int j = 0; j < map.Breedte; j++)
                {
                    if (spriteArray[j, i] != null)
                    {
                        spriteArray[j, i].Draw();
                    }
                }
            }
            for (int i = 0; i < rocketList.Count; i++)
            {
                rocketList[i].Draw();
            }
            video.Blit(coinCountSurface, new Point(video.Width - coinCountSurface.Width, 0));
            video.Blit(coinSingle, new Point(video.Width - coinCountSurface.Width - coinSingle.Width - 10, 0));
            if (hero.dead)
            {
                endText("You are dead", "Try again");

            }

            if (hero.win)
            {
                string menuOption = "Next level";
                if (level == 2)
                {
                    menuOption = "Menu";
                }
                endText("You win", menuOption);
            }
        }

        private void endText(string endText, string secondText)
        {
           
            text = font.Render(endText, Color.White);
            video.Blit(text, new Point(video.Width / 2 - text.Width / 2, video.Height / 2 - text.Height / 2));
            text = font.Render(secondText, Color.White);
            menuPoint = new Point(video.Width / 2 - text.Width / 2, video.Height / 2 + text.Height * 2);
            video.Blit(text, menuPoint);
        }

        public override void ApplicationMouseButtonEventHandler(object sender, MouseButtonEventArgs args)
        {
            if (text != null)
            {
                if (args.Button == MouseButton.PrimaryButton && cursorPosition.X >= menuPoint.X && cursorPosition.X <= menuPoint.X + text.Width && cursorPosition.Y >= menuPoint.Y && cursorPosition.Y <= menuPoint.Y + text.Height)
                {
                    ended = true;
                }
            }

        }
    }
}
