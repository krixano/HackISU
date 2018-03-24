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
        static public Game1.SpriteStruct sprite, gun;
        static Timer tmr;
        //tmrAmt = Jump length
        static int tmrAmt = 10;
        static int currentTmr = 0;
        static bool isJumping = false;

        static public void playerInit()
        {
            tmr = new Timer(60);

            playerXSpeed = World.BLOCK_SIZE * .25f;
            playerYSpeed = World.BLOCK_SIZE * .25f;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 20;
            sprite.position.X = Game1.screenRectangle.Center.X - (sprite.size.X / 2); // In World Pixels
            sprite.position.Y = (((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;

            gun.size.X = sprite.size.X;
            gun.size.Y = gun.size.Y / 2f;
            gun.position.X = sprite.position.X + gun.size.X / 2;
            gun.position.Y = sprite.position.Y + gun.size.Y / 2;

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


            Vector2 gravityCollisionBottomRight = new Vector2((sprite.position.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            Vector2 gravityCollisionBottomLeft = new Vector2((sprite.position.X) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            bool isFalling = false;

            //checking collision for bottom of player, left and right corners
            if ((World.blocks[(int)gravityCollisionBottomRight.X + (int)gravityCollisionBottomRight.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomRight.X < 0 || gravityCollisionBottomRight.X > World.WORLD_SIZE.X || gravityCollisionBottomRight.Y < 0 || gravityCollisionBottomRight.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                sprite.position.Y += playerYSpeed;
                isFalling = true;
            }
            if ((World.blocks[(int)gravityCollisionBottomLeft.X + (int)gravityCollisionBottomLeft.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomLeft.X < 0 || gravityCollisionBottomLeft.X > World.WORLD_SIZE.X || gravityCollisionBottomLeft.Y < 0 || gravityCollisionBottomLeft.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                sprite.position.Y += playerYSpeed;
                isFalling = true;
            }

            //jump
            //if pressing jump button, and not jumping or falling
            if ((Game1.pad1.IsButtonDown(Buttons.A) || Game1.keyboard.IsKeyDown(Keys.Space)) && !isJumping && !isFalling)
                isJumping = true;
            playerJump();
        }
        

        public static void playerJump()
        {
            if (isJumping)
            {
                currentTmr++;
                sprite.position.Y -= playerYSpeed * 2;
                if (currentTmr == tmrAmt)
                {
                    currentTmr = 0;
                    isJumping = false;
                }
            }
            
            /*tmr.start();
            if (tmr.Update() == true)
            {
                tmr.stop();
            } else if (tmr.running)
            {
                sprite.position.Y -= playerYSpeed;
            }*/
            //else
            //    tmr.stop();
        }

        public static void gunUpdate()
        {
            gun.position.X = sprite.position.X + gun.size.X;
            gun.position.Y = sprite.position.Y + gun.size.Y;
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
