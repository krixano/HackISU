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
        static private float playerXSpeed, playerYSpeed; // In Pixels
        static public Game1.SpriteStruct sprite;

        static public void playerInit()
        {
            playerXSpeed = World.BLOCK_SIZE * .25f;
            playerYSpeed = World.BLOCK_SIZE * .25f;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 20;
            sprite.position.X = Game1.screenRectangle.Center.X - (sprite.size.X / 2); // In World Pixels
            sprite.position.Y = (((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;

        }
        public static void playerUpdate()
        {
            //Controls player moving left and right        
            if (Game1.keyboard.IsKeyDown(Keys.Right) && playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
                World.offset.X += .25f;
            if (Game1.keyboard.IsKeyDown(Keys.Left) && playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
                World.offset.X -= .25f;
            //else if (Game1.keyboard.IsKeyDown(Keys.Up) && sprite.position.Y <= Game1.screenRectangle.Bottom - Game1.screenRectangle.Height / 3)
            //    World.offset.Y -= .25f;
            //else if (Game1.keyboard.IsKeyDown(Keys.Down) && sprite.position.Y >= Game1.screenRectangle.Top - Game1.screenRectangle.Height / 3)
            //    World.offset.Y += .25f;

            //Scrolls screen
            if (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left))
                sprite.position.X -= playerXSpeed;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right))
                sprite.position.X += playerXSpeed;

            Vector2 playerBlocks = new Vector2((sprite.position.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            Console.WriteLine(playerBlocks);

            if (World.blocks[(int)playerBlocks.X + (int)playerBlocks.Y * (int)World.WORLD_SIZE.X].solid != true
                || playerBlocks.X < 0 || playerBlocks.X > World.WORLD_SIZE.X || playerBlocks.Y < 0 || playerBlocks.Y > World.WORLD_SIZE.Y)
                sprite.position.Y += playerYSpeed;

            //jump
            //if (Game1.pad1.IsButtonDown(Buttons.A) || Game1.keyboard.IsKeyDown(Keys.Space))
            //playerJump();
        }
        void playerJump()
        {            
            Timer tmr = new Timer(30);
            tmr.start();
            if (tmr.running == true)
                sprite.position.Y -= playerYSpeed;
            //if (tmr.Update() == false)

        }

        public static Vector2 playerScreenPixels()
        {
            return new Vector2(player.sprite.position.X - (World.offset.X * World.BLOCK_SIZE), player.sprite.position.Y - (World.offset.Y * World.BLOCK_SIZE));
        }

        public static Vector2 playerWorldBlocks()
        {
            return new Vector2(player.sprite.position.X / World.BLOCK_SIZE, player.sprite.position.Y / World.BLOCK_SIZE);
        }

    }
}
