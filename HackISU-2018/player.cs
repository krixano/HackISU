using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace HackISU_2018
{
    class player
    {
        static private int playerXSpeed, playerYSpeed;
        static public Game1.SpriteStruct sprite;

        static public void playerInit()
        {
            playerXSpeed = Game1.screenRectangle.Width / 20;
            playerYSpeed = Game1.screenRectangle.Height / 20;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 20;
            sprite.position.X = Game1.screenRectangle.Center.X - (sprite.size.X / 2);
            sprite.position.Y = Game1.screenRectangle.Center.Y - (sprite.size.Y / 2);
                       
                        
        }
        public static void playerUpdate()
        {
            if (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left))
                sprite.position.X += playerXSpeed;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right))
                sprite.position.X -= playerXSpeed;
            //if (Game1.pad1.IsButtonDown(Buttons.DPadUp))
            //    player.position.Y += playerYSpeed;
            //if (Game1.pad1.IsButtonDown(Buttons.DPadDown))
            //    player.position.Y -= playerYSpeed;
        }

    }
}
