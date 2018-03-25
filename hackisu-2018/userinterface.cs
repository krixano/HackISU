using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    class UserInterface
    {
        static public Game1.Menu startingMenu, pausedMenu, optionMenu, levels;
        static public Rectangle areaMenu, areaPaused;
        const int MAX_Bottons = 3, GAP = 50;
        static public MouseState mouse1;
        static public void InitializeMenus()
        {
            Rectangle screen = Game1.screenRectangle;
            areaMenu = new Rectangle(screen.X, screen.Y, screen.Width, screen.Height);
            areaPaused = new Rectangle(areaMenu.X + GAP, areaMenu.Y + GAP, areaMenu.Width - 100, areaMenu.Height - 100);
            startingMenu.bottons = new Rectangle[MAX_Bottons + 1];
            startingMenu.textures = new Texture2D[MAX_Bottons + 1];
            pausedMenu.bottons = new Rectangle[MAX_Bottons];
            pausedMenu.textures = new Texture2D[MAX_Bottons];
            optionMenu.bottons = new Rectangle[MAX_Bottons - 1];
            optionMenu.textures = new Texture2D[MAX_Bottons - 1];

            levels.bottons = new Rectangle[5];
            levels.textures = new Texture2D[5];
            optionMenu.textures = new Texture2D[MAX_Bottons - 1];

            levels.bottons = new Rectangle[5];
            levels.textures = new Texture2D[5];

            for (int r = 0; r <= MAX_Bottons; r++)
            {
                startingMenu.bottons[r].X = areaMenu.X + Game1.screenRectangle.Width /2;
                startingMenu.bottons[r].Width = areaMenu.Width /3-200;
                startingMenu.bottons[r].Height = (areaMenu.Height - ((MAX_Bottons + 1) * GAP)) / (MAX_Bottons + 1);
                startingMenu.bottons[r].Y = areaMenu.Y + (r * startingMenu.bottons[r].Height) + GAP * (r + 1);
            }

            startingMenu.color = Color.White;

            pausedMenu.color = Color.White;
            for (int r = 0; r < MAX_Bottons; r++)
            {
                pausedMenu.bottons[r ].X = areaPaused.X + GAP;
                pausedMenu.bottons[r ].Width = areaPaused.Width - (2 * GAP);
                pausedMenu.bottons[r ].Height = (areaPaused.Height - (MAX_Bottons * GAP)) / MAX_Bottons;
                pausedMenu.bottons[r ].Y = areaPaused.Y + ((r ) * pausedMenu.bottons[r].Height) + GAP * (r+1);
            }

            optionMenu.color = Color.White;
            for (int k = 0; k < 2; k++)
            {
                optionMenu.bottons[k] = pausedMenu.bottons[k];
            }

            levels.color = Color.White;
            for (int r = 0; r < 5; r++)
            {
                levels.bottons[r].X = areaPaused.X + GAP;
                levels.bottons[r].Width = areaPaused.Width - (2 * GAP);
                levels.bottons[r].Height = (areaPaused.Height - (5 * GAP)) / 5;
                levels.bottons[r].Y = areaPaused.Y + ((r) * levels.bottons[r].Height) + GAP * (r + 1);
            }
        }
        static public void LoadTexture()
        {
            for (int r = 0; r <= MAX_Bottons; r++)
            {

                if (r == 1)
                    startingMenu.textures[r] = Game1.load;
                else if (r == 2)
                    startingMenu.textures[r] = Game1.settings;
                else if (r == 3)
                    startingMenu.textures[r] = Game1.quit;
                else
                {
                    if (r == 0)
                        startingMenu.textures[r] = Game1.newGame;
                }
            }
            for (int r = 0; r < MAX_Bottons; r++)
            {
                switch (r)
                {

                    case 1:
                        pausedMenu.textures[r] = Game1.settings;
                        break;
                    case 2:
                        pausedMenu.textures[r] = Game1.quit;
                        break;
                    default:
                        pausedMenu.textures[r] = Game1.resume;
                        break;
                }
            }
            for (int k = 0; k < 2; k++)
            {
                switch(k)
                {
                    case 0:
                        optionMenu.textures[k] = Game1.resume;
                        break;
                    case 1:
                        optionMenu.textures[k] = Game1.quit;
                        break;
                }
            }
            for(int i=0; i<5; i++)
            {
                switch(i)
                {
                    case 0:
                        levels.textures[i] = Game1.l1;                        
                        break;
                    case 1:
                        levels.textures[i] = Game1.l2;
                        break;
                    case 2:
                        levels.textures[i] = Game1.l3;
                        break;
                    case 3:
                        levels.textures[i] = Game1.l4;
                        break;
                    case 4:
                        levels.textures[i] = Game1.l5;
                        break;
                }
            }
        }

        static public void UpdateButtonsStart()
        {
            

            
            mouse1 = Game1.mouse;
            if (mouse1.LeftButton == ButtonState.Pressed && Game1.prevMouse.LeftButton == ButtonState.Released)
            {
                if (startingMenu.bottons[0].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.PLAYING;
                    //World.fileName = "map2.txt";
                }
                else if (startingMenu.bottons[1].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.Levels;
                }
                else if (startingMenu.bottons[2].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.OPTIONS;
                }
                else if (startingMenu.bottons[3].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.Exit;
                }
            }

        }

        public static void UpdateButtonsPaused()
        {
            mouse1 = Game1.mouse;
            if (mouse1.LeftButton == ButtonState.Pressed && Game1.prevMouse.LeftButton == ButtonState.Released)
            {
                if (pausedMenu.bottons[0].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.PLAYING;
                }
                else if (pausedMenu.bottons[1].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.OPTIONS;
                }
                else if (pausedMenu.bottons[2].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.Exit;

                }
            }
        }

        public static void UpdateButtonsOptions()
        {
            mouse1 = Game1.mouse;
            if (mouse1.LeftButton == ButtonState.Pressed && Game1.prevMouse.LeftButton == ButtonState.Released)
            {
                if (optionMenu.bottons[0].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.PLAYING;
                }
                else if (optionMenu.bottons[1].Contains(mouse1.X, mouse1.Y))
                {
                    Game1.gameState = Game1.GameStates.Exit;
                }
                
            }
        }
        static public void DrawStartingMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.testTexture, areaMenu, Color.DimGray);
            for (int r = 0; r <= MAX_Bottons; r++)
            {
                spriteBatch.Draw(startingMenu.textures[r], startingMenu.bottons[r], startingMenu.color);
            }

        }
        static public void DrawPauseMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.testTexture, areaPaused, Color.DimGray);
            for (int r = 0; r < MAX_Bottons; r++)
            {
                spriteBatch.Draw(pausedMenu.textures[r], pausedMenu.bottons[r], pausedMenu.color);
            }

        }
        static public void DrawOptionsMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.testTexture, areaPaused, Color.DimGray);
            for (int r = 0; r < MAX_Bottons-1; r++)
            {
                spriteBatch.Draw(optionMenu.textures[r], optionMenu.bottons[r], optionMenu.color);
            }
        }

        static public void UpdateLevels()
        {

            mouse1 = Game1.mouse;
            //Console.WriteLine("Update Levels Activated!");
            if (mouse1.LeftButton == ButtonState.Pressed && Game1.prevMouse.LeftButton == ButtonState.Released)
            {
                if (levels.bottons[0].Contains(mouse1.X, mouse1.Y))
                {
                    World.fileName = "map1.txt";
                    Game1.gameState = Game1.GameStates.PLAYING;
                }
                else if (levels.bottons[1].Contains(mouse1.X, mouse1.Y))
                {
                    World.fileName = "map2.txt";
                    Game1.gameState = Game1.GameStates.PLAYING;
                }
                else if (levels.bottons[2].Contains(mouse1.X, mouse1.Y))
                {
                    World.fileName = "map3.txt";
                    Game1.gameState = Game1.GameStates.PLAYING;
                }
                else if (levels.bottons[3].Contains(mouse1.X, mouse1.Y))
                {
                    World.fileName = "map4.txt";
                    Game1.gameState = Game1.GameStates.PLAYING;
                }
                else if (levels.bottons[4].Contains(mouse1.X, mouse1.Y))
                {
                    //Game1.gameState = Game1.GameStates.PLAYING;
                    //World.fileName = "map5.txt";
                }
            }    
                   
                   
              
           
        }

        static public void DrawLevels(SpriteBatch sprite)
        {
            sprite.Draw(Game1.testTexture, areaMenu, Color.DimGray);
            //Console.WriteLine("drawing.");
            for (int r = 0; r < MAX_Bottons+2; r++)
            {
                sprite.Draw(levels.textures[r], levels.bottons[r], levels.color);
            }
        }
    }
}