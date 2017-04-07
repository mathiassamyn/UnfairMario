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
    class Hero: Sprites
    {
        public Point position;
        private int xVelocity;       
        private Surface imageWalkLeft;
        private Surface imageWalkRight;
        private Surface imageJumpLeft;
        private Surface imageJumpRight;
        private Rectangle visibleRectangle;
        private bool left, right, up, down;
        private Key leftKey, rightKey, upKey;
        public int gravity;
        public Rectangle CollRect;
        private bool upEnable;
        CollisionDetection collDet;
        public bool dead, win;
        Surface lastPositionMario;

        public Hero(Surface video, int x, int y)
        {
            Video = video;
            imageWalkLeft = new Surface(@"Sprites\mario\marioWalkLeft.png");
            imageWalkRight = new Surface(@"Sprites\mario\marioWalkRight.png");
            imageJumpLeft = new Surface(@"Sprites\mario\marioJumpLeft.png");
            imageJumpRight = new Surface(@"Sprites\mario\marioJumpRight.png");
            visibleRectangle = new Rectangle(0, 0, 39, 66);
            position = new Point(x, y);
            xVelocity = 20;
            Events.KeyboardDown += Events_KeyboardDown;
            Events.KeyboardUp += Events_KeyboardUp;
            leftKey = Key.LeftArrow;
            rightKey = Key.RightArrow;
            upKey = Key.Space;
            Image = imageWalkRight;
            gravity = 0;
            CollRect = new Rectangle(position.X, position.Y, visibleRectangle.Width, visibleRectangle.Height);
            upEnable = true;
            collDet = new CollisionDetection();
            dead = win = false;
        }

        //indien we een toets loslaten
        private void Events_KeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (e.Key == leftKey)
            {
                left = false;
            }
            if (e.Key == rightKey)
            {
                right = false;
            }

        }

        //indien we een toets indrukken
        private void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (!dead && !win)
            {

                if (!up)
                {
                    down = true; //nodig om continu een downforce te hebben, zodat mario valt wanneer hij niet meer op een blok staat en niet aan het springen is
                }

                if (e.Key == leftKey)
                {

                    left = true;
                    Image = imageWalkLeft;
                }
                if (e.Key == rightKey)
                {

                    right = true;
                    Image = imageWalkRight;
                }
                if (upEnable) // nodig om geen jump in midair te kunnen starten
                {
                    if (e.Key == upKey)
                    {
                        down = false; //indien we gaan springen moet de downforce af, zodat we in de lucht kunnen springen
                        up = true;
                    }
                }
            }
        }       
     

        public void Update(GameLevel level)
        {
            if (!dead)
            {

                if (left) //indien we het linkerpijltje indrukken
                {
                    spriteUpdate(xVelocity, 1, level);

                    int block = collDet.CollDetection(CollRect, "left", level); //check of de collision rectangle snijdt met een blok

                    if (block >= 0) //indien ja
                    {
                        spriteUpdate(block, -1, level);
                    }

                }
                if (right) //indien we het rechterpijltje indrukken
                {
                    spriteUpdate(xVelocity, -1, level);

                    int block = collDet.CollDetection(CollRect, "right", level); //check of de collision rectangle snijdt met een blok

                    if (block >= 0) //indien ja
                    {
                        spriteUpdate(block, 1, level);
                    }

                    
                }


                if (up) // indien we spatie indrukken
                {
                    jumpImage();

                    if (upEnable) // indien we net aan de jump beginnen
                    {
                        upEnable = false;
                        gravity = 42; // we stellen de gravity in op 42, bij een normale jump zal dit de max. gravity zijn
                    }

                    gravityAndPositionUpdate(-1);

                    int block = collDet.CollDetection(CollRect, "up", level); // check of de bovenste collision rectangle snijdt met een blok
                    if (block >= 0 || gravity <= 0) 
                    {
                        up = false; // aangezien we een blok raken of op ons hoogste punt zijn, moeten we stoppen met springen en zetten we up dus op false
                        down = true; // aangezien we gestopt zijn met springen moet mario naar beneden vallen, dus we zettend down op true                   
                    }

                    visibleRectangle.X = 0; // aangezien we bij het springen een andere sprite gebruiken, moeten we de X coö van de visible rectangle op 0 zetten

                }

                if (down) // indien we vallen
                {
                    jumpImage();

                    upEnable = false;

                    gravityAndPositionUpdate(1);

                    int block = collDet.CollDetection(CollRect, "down", level); // check of de onderste collision rectangle snijdt met een blok

                    touchingGround(block);

                    if (!upEnable) //voor rocket
                    {
                        bool checkHit = true;

                        foreach (Rocket rocket in level.rocketList)
                        {
                            int rocketY = rocket.UpdatePosition(level, checkHit);

                            touchingGround(rocketY);
                        }
                    }

                    if (position.Y >= Video.Height)
                    {
                        marioDeath();
                    }


                }
                if (left || right ) // indien we de rechterpijl- of linkerpijltoets indrukken en we niet aan het springen zijn
                {
                    visibleRectangle.X += visibleRectangle.Width;
                    if (visibleRectangle.X >= Image.Width) // indien de visible rectangle aan het einde van de sprite is
                    {
                        visibleRectangle.X = 0; // X coö van de visible rectangle wordt weer naar de eerste afbeelding van de sprite gezet
                    }
                }
                if (!left && !right)
                {
                    visibleRectangle.X = 0;
                }
            }
            else
            {

                position.Y -= xVelocity;
                if (position.Y <= 100)
                {
                    xVelocity = 0;
                }

            }


        }

        
        private void spriteUpdate(int xValue, int posNeg, GameLevel level)
        {
            foreach (Sprites sprite in level.spriteArray)
            {
                if (sprite != null)
                {
                    sprite.X += xValue * posNeg;
                    sprite.Collrect = new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Height);
                    if (sprite.GetType() == typeof(Finish) && position.X >= sprite.X)
                    {
                        marioWin();
                    }
                }

            }
            foreach (Rocket rocket in level.rocketList)
            {
                rocket.X += xValue * posNeg;
            }
        }

        private void jumpImage()
        {
            if (left) 
            {
                Image = imageJumpLeft; 
            }
            else if (right) 
            {
                Image = imageJumpRight; 
            }
        }

        private void gravityAndPositionUpdate(int posNeg)
        {
            gravity += 6 * posNeg; 
            position.Y += gravity * posNeg; 
            CollRect.Y = position.Y;
        }

        private void touchingGround(int yValue)
        {
            if (yValue >= 0) 
            {
         
                position.Y -= yValue;
                CollRect.Y = position.Y;

                gravity = 0; // we resetten gravity naar 0, zodat bij een gewone val mario ook van 0 gravity begint te vallen

                if (Image == imageJumpLeft)
                {
                    Image = imageWalkLeft;
                }
                if (Image == imageJumpRight)
                {
                    Image = imageWalkRight;
                }

                upEnable = true;
            }
        }

        // teken mario
        public override void Draw()
        {
            Video.Blit(Image, position, visibleRectangle);
            if (dead)
            {
                Video.Blit(lastPositionMario, new Point(CollRect.X, CollRect.Y), visibleRectangle);
            }            

        }


        public void marioDeath()
        {
            dead = true;
            lastPositionMario = new Surface(Image);
            if (Image == imageWalkLeft || Image == imageJumpLeft)
            {
                Image = new Surface(@"Sprites\mario\marioDeadLeft.png");
            }
            else if (Image == imageWalkRight || Image == imageJumpRight)
            {
                Image = new Surface(@"Sprites\mario\marioDeadRight.png");
            }

        }

        public void marioWin()
        {
            win = true;
        }
    }
}
