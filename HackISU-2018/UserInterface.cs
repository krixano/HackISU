using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    class UserInterface
    {
        static public Game1.Menu startingMenu, pausedMenu;
        static public MouseState mouse = Game1.mouse;
        static public Rectangle areaMenu, areaPaused;
        const int MAX_Bottons = 3, GAP = 50;
        static public void InitializeMenus()
        {
            Rectangle screen = Game1.screenRectangle;
            areaMenu = new Rectangle(screen.X, screen.Y, screen.Width, screen.Height);
            areaPaused = new Rectangle(areaMenu.X + GAP, areaMenu.Y + GAP, areaMenu.Width - 100, areaMenu.Height - 100);
            startingMenu.bottons = new Rectangle[MAX_Bottons + 1];
            pausedMenu.bottons = new Rectangle[MAX_Bottons];
            for (int r = 0; r <= MAX_Bottons; r++)
            {
                startingMenu.bottons[r].X = areaMenu.X + 100;
                startingMenu.bottons[r].Width = areaMenu.Width - 200;
                startingMenu.bottons[r].Height = (areaMenu.Height - ((MAX_Bottons+1) * GAP)) / (MAX_Bottons+1);
                startingMenu.bottons[r].Y = areaMenu.Y + (r * startingMenu.bottons[r].Height) + GAP * (r + 1);
            }

            startingMenu.color = Color.White;

            pausedMenu.color = Color.White;
            for (int r = 1; r <= MAX_Bottons; r++)
            {
                pausedMenu.bottons[r-1].X = areaPaused.X + GAP;
                pausedMenu.bottons[r-1].Width = areaPaused.Width - (2*GAP);
                pausedMenu.bottons[r-1].Height = (areaPaused.Height - (MAX_Bottons * GAP)) / MAX_Bottons;
                pausedMenu.bottons[r-1].Y = areaPaused.Y + ((r-1) * pausedMenu.bottons[r].Height) + GAP * (r);
            }
        }

        
    }
}