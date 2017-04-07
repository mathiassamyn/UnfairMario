using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class CollisionDetection
    {
       

        public CollisionDetection()
        {
            
        }

        public int CollDetection(Rectangle collRect, string position, GameLevel level)
        {
            if (position == "heroDown")
            {
                if (level.hero.CollRect.IntersectsWith(collRect))
                {
                     return level.hero.CollRect.Y + level.hero.CollRect.Height - collRect.Y; 
                } 
            }
            if (position == "heroNotDown")
            {
                if (level.hero.CollRect.IntersectsWith(collRect))
                {
                    level.hero.marioDeath();
                }
            }

            for (int i = 0; i < level.map.Hoogte; i++)
            {
                for (int j = 0; j < level.map.Breedte; j++)
                {
                    if (level.spriteArray[j, i] != null && level.spriteArray[j, i].Collrect.IntersectsWith(collRect))
                    {
                        if (level.spriteArray[j, i].GetType() == typeof(Coin) && level.hero.CollRect == collRect)
                        {
                            level.spriteArray[j, i] = null;
                            level.CoinCount++;
                        }                       
                        if (level.spriteArray[j, i] != null)
                        {
                            if (level.hero.CollRect.IntersectsWith(collRect)) //nodig omdat collrect om 1 of andere reden de coll rect van de sprite geeft
                            {
                                if (level.spriteArray[j, i].GetType() == typeof(EnemyShell) || level.spriteArray[j, i].GetType() == typeof(AngryStone))
                                {
                                    level.hero.marioDeath();
                                }
                                if (level.spriteArray[j, i].GetType() == typeof(Spikes))
                                {
                                    Spikes spike = (Spikes)level.spriteArray[j, i];
                                    spike.ShowSpikes();
                                    level.hero.marioDeath();
                                }
                               
                            }                            
                            
                            if (!(level.spriteArray[j, i].GetType()).IsSubclassOf(typeof(Enemy)) && level.spriteArray[j, i].GetType() != typeof(Coin)) 
                            {
                                if (level.spriteArray[j, i].GetType() == typeof(InvisibleWall))
                                {
                                    InvisibleWall wall = (InvisibleWall)level.spriteArray[j, i];
                                    wall.ShowWall();
                                }
                                if (position == "left")
                                {
                                    
                                    if (level.hero.CollRect.X == collRect.X || level.spriteArray[j,i].GetType() != typeof(Cannon)) // nodig voor rocket bug
                                    {
                                        return level.spriteArray[j, i].Collrect.X + level.spriteArray[j, i].Width - collRect.X;
                                    }
                                    

                                }
                                if (position == "right")
                                {

                                    return collRect.X + collRect.Width - level.spriteArray[j, i].Collrect.X;

                                }
                                if (position == "up")
                                {
                                    if (level.spriteArray[j, i].GetType() == typeof(CoinBlock))
                                    {
                                        CoinBlock coinBlock = (CoinBlock)level.spriteArray[j, i];
                                        coinBlock.CoinBlockHit(level, j, i);
                                    }
                                    return level.spriteArray[j, i].Y + level.spriteArray[j, i].Height - collRect.Y;
                                    
                                    

                                }
                                if (position == "down")
                                {
                                    return collRect.Y + collRect.Height - level.spriteArray[j, i].Y;
                                

                                }                               
                            }
                        }                                          
                    }

                }
            }
            return -5;

        }
    }
}
