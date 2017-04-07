using SdlDotNet.Audio;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    class Manager
    {
        private Surface video;
        private IntroScreen intro;
        private GameLevel level;
        bool init, game, gameOverMusic, introMusic;
        BackgroundWorker gameThread;
        Thread musicThread;


        public Manager()
        {
            Events.Quit += Events_Quit;
            gameThread = new BackgroundWorker();      

            gameThread.DoWork += GameThread_DoWork;
            video = Video.SetVideoMode(1500, 650);
            init = true;
            game = false;
            intro = new IntroScreen(video);
            Events.Tick += Events_Tick;
            Events.Run();
      
        }

        private void AudioPlaybackThread(string path, int volume)
        {
            Music themeSong = new Music(path);
            MusicPlayer.Volume = volume;
            MusicPlayer.Load(themeSong);
           
            MusicPlayer.Play(true);
            
  
        }
       

        private void GameThread_DoWork(object sender, DoWorkEventArgs e)
        {
            video.Fill(Color.Cyan);
            level.Update();
            level.Draw();
            if (level.hero.dead && !gameOverMusic)
            {
                gameOverMusic = true;
                musicThread = new Thread(() => AudioPlaybackThread(@"Music\Sad Violin - MLG Sound Effects (HD).mp3", 30));
                musicThread.Start();
            }
            if (level.ended)
            {
                if (level.hero.dead)
                {
                    if (level.level == 1)
                    {
                        level = new GameLevel(video, @"Sprites\levels\level1", 1);
                    }
                    else if (level.level == 2)
                    {
                        level = new GameLevel(video, @"Sprites\levels\level2", 2);
                    }                                      
                    
                }
                else if (level.hero.win)
                {
                    if (level.level == 2)
                    {
                        game = false;
                        init = true;
                        intro = new IntroScreen(video);

                    }
                    else
                    {                       
                        level = new GameLevel(video, @"Sprites\levels\level2", 2);
                        
                    }
                    
                }
                musicThread = new Thread(() => AudioPlaybackThread(@"Music\Super MLG Bros. (Air Horn Remix).mp3", 30));
                musicThread.Start();
                gameOverMusic = false;
            }
        }

        private void Events_Tick(object sender, TickEventArgs e)
        {
            

            
            if (init)
            {
                if (!introMusic)
                {
                    introMusic = true;
                    musicThread = new Thread(() => AudioPlaybackThread(@"Music\Bully Intro Music.wav", 15));
                    musicThread.Start();
                }
                intro.titleDraw();
                intro.marioDraw();
                if (intro.ended)
                {
                    init = introMusic = false;
                    game = true;
                    level = new GameLevel(video, @"Sprites\levels\level1", 1);
                    musicThread = new Thread(() => AudioPlaybackThread(@"Music\Super MLG Bros. (Air Horn Remix).mp3", 30));
                    musicThread.Start();
                }
            } else if (game)
            {
           
                if (!gameThread.IsBusy)
                {
                    gameThread.RunWorkerAsync();
               }
                
                
            }
            SdlDotNet.Core.Timer.DelayTicks(60);
            video.Update();
        }

        private void Events_Quit(object sender, QuitEventArgs e)
        {
            Events.QuitApplication();

        }


    }
}
