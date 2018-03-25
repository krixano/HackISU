using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    class UserInterface
    {
        static public Game1.Menu startingMenu, pausedMenu, optionMenu;
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

            for (int r = 0; r <= MAX_Bottons; r++)
            {
                startingMenu.bottons[r].X = areaMenu.X + 100;
                startingMenu.bottons[r].Width = areaMenu.Width - 200;
                startingMenu.bottons[r].Height = (areaMenu.Height - ((MAX_Bottons + 1) * GAP)) / (MAX_Bottons + 1);
                startingMenu.bottons[r].Y = areaMenu.Y + (r * startingMenu.bottons[r].Height) + GAP * (r + 1);
            }

            startingMenu.color = Color.White;

            pausedMenu.color = Color.White;
            for (int r = 1; r <= MAX_Bottons; r++)
            {
                pausedMenu.bottons[r - 1].X = areaPaused.X + GAP;
                pausedMenu.bottons[r - 1].Width = areaPaused.Width - (2 * GAP);
                pausedMenu.bottons[r - 1].Height = (areaPaused.Height - (MAX_Bottons * GAP)) / MAX_Bottons;
                pausedMenu.bottons[r - 1].Y = areaPaused.Y + ((r - 1) * pausedMenu.bottons[r - 1].Height) + GAP * (r);
            }

            optionMenu.color = Color.White;
            for(int k =0; k < 2; k++)
            {
                optionMenu.bottons[k]= pausedMenu.bottons[k];
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
            for(int k = 0; k<2; k++)
            {

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
                }
                else if (startingMenu.bottons[1].Contains(mouse1.X, mouse1.Y))
                {

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
            if (startingMenu.bottons[0].Contains(mouse1.X, mouse1.Y))
            {
                Game1.gameState = Game1.GameStates.PLAYING;
            }
            else if (startingMenu.bottons[1].Contains(mouse1.X, mouse1.Y))
            {
                Game1.gameState = Game1.GameStates.OPTIONS;
            }
            else if (startingMenu.bottons[2].Contains(mouse1.X, mouse1.Y))
            {
                Game1.gameState = Game1.GameStates.Exit;
            }
        }

        public static void UpdateButtonsOptions()
        {
            
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

        
    }
}