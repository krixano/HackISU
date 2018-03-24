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
            playerXSpeed = Game1.screenRectangle.Width / 80;
            playerYSpeed = Game1.screenRectangle.Height / 80;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 20;
            sprite.position.X = Game1.screenRectangle.Center.X - (sprite.size.X / 2);
            sprite.position.Y = (((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;
                       
                        
        }
        public static void playerUpdate()
        {
            if (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left))
                sprite.position.X -= playerXSpeed;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right))
                sprite.position.X += playerXSpeed;           

            if (Game1.keyboard.IsKeyDown(Keys.Right) && sprite.position.X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width/3)
                World.offset.X += .25f;
            if (Game1.keyboard.IsKeyDown(Keys.Left) && sprite.position.X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3)
                World.offset.X -= .25f;
            if (Game1.keyboard.IsKeyDown(Keys.Up) && sprite.position.Y <= Game1.screenRectangle.Bottom - Game1.screenRectangle.Height / 3)
                World.offset.Y -= .25f;
            if (Game1.keyboard.IsKeyDown(Keys.Down) && sprite.position.Y >= Game1.screenRectangle.Top - Game1.screenRectangle.Height / 3)
                World.offset.Y += .25f;
        }

    }
}
