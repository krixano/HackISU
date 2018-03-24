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
            sprite.position.X = ((Game1.screenRectangle.Center.X - (sprite.size.X / 2) / World.BLOCK_SIZE) + World.offset.X); // In World Pixels
            sprite.position.Y = (((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;
            
                       
        }
        public static void playerUpdate()
        {
            
            Vector2 gravityCollisionBottomRight = new Vector2((sprite.position.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            Vector2 gravityCollisionBottomLeft = new Vector2((sprite.position.X) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            bool isFalling = false;

            // Checking collision for bottom of player, left and right corners
            if ((World.blocks[(int)gravityCollisionBottomRight.X + (int)gravityCollisionBottomRight.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomRight.X < 0 || gravityCollisionBottomRight.X > World.WORLD_SIZE.X || gravityCollisionBottomRight.Y < 0 || gravityCollisionBottomRight.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                isFalling = true;
                //sprite.position.Y += playerYSpeed;
                float increment = playerYSpeed;
                float x1 = (sprite.position.X) / World.BLOCK_SIZE;
                float x2 = (sprite.position.X + sprite.size.X) / World.BLOCK_SIZE;
                float y = (sprite.position.Y + sprite.size.Y + increment - 4) / World.BLOCK_SIZE;
                int i = (int) (x1 + y * World.WORLD_SIZE.X);
                int i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                while (World.blocks[i].solid || World.blocks[i2].solid)
                {
                    isFalling = false;
                    increment--;
                    y = (sprite.position.Y + sprite.size.Y + increment - 4) / World.BLOCK_SIZE;
                    i = (int) (x1 + y * World.WORLD_SIZE.X);
                    i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                }
            }
            if ((World.blocks[(int)gravityCollisionBottomLeft.X + (int)gravityCollisionBottomLeft.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomLeft.X < 0 || gravityCollisionBottomLeft.X > World.WORLD_SIZE.X || gravityCollisionBottomLeft.Y < 0 || gravityCollisionBottomLeft.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                isFalling = true;
                float increment = playerYSpeed;
                float x1 = (sprite.position.X) / World.BLOCK_SIZE;
                float x2 = (sprite.position.X + sprite.size.X) / World.BLOCK_SIZE;
                float y = (sprite.position.Y + sprite.size.Y + increment - 4) / World.BLOCK_SIZE;
                int i = (int) (x1 + y * World.WORLD_SIZE.X);
                int i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                while (World.blocks[i].solid || World.blocks[i2].solid)
                {
                    isFalling = false;
                    increment--;
                    y = (sprite.position.Y + sprite.size.Y + increment - 4) / World.BLOCK_SIZE;
                    i = (int) (x1 + y * World.WORLD_SIZE.X);
                    i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                }
                sprite.position.Y += increment;
            }

            Vector2 sideCollisionTopLeft = new Vector2((sprite.position.X - 1) / World.BLOCK_SIZE, (sprite.position.Y) / World.BLOCK_SIZE);
            Vector2 sideCollisionBottomLeft = new Vector2((sprite.position.X - 1) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y - 1) / World.BLOCK_SIZE);
            Vector2 sideCollisionTopRight = new Vector2((sprite.position.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position.Y) / World.BLOCK_SIZE);
            Vector2 sideCollisionBottomRight = new Vector2((sprite.position.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position.Y + sprite.size.Y - 1) / World.BLOCK_SIZE);

            bool canGoLeft = true;
            bool canGoRight = true;

            // Checking collision for left and right edges of player
            if (World.blocks[(int) sideCollisionTopLeft.X + (int) sideCollisionTopLeft.Y * (int) World.WORLD_SIZE.X].solid)
                canGoLeft = false;
            if (World.blocks[(int) sideCollisionBottomLeft.X + (int) sideCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid)
                canGoLeft = false;
            if (World.blocks[(int) sideCollisionTopRight.X + (int) sideCollisionTopRight.Y * (int) World.WORLD_SIZE.X].solid)
                canGoRight = false;
            if (World.blocks[(int) sideCollisionBottomRight.X + (int) sideCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid)
                canGoRight = false;

            // Controls player moving left and right
            if (canGoRight && Game1.keyboard.IsKeyDown(Keys.Right) || Game1.keyboard.IsKeyDown(Keys.D) && playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
                World.offset.X += .25f;
            if (canGoRight && Game1.keyboard.IsKeyDown(Keys.Left) || Game1.keyboard.IsKeyDown(Keys.A) && playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
                World.offset.X -= .25f;
            

            // Scrolls screen
            if (canGoLeft && (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left) || Game1.keyboard.IsKeyDown(Keys.A)))
                sprite.position.X -= playerXSpeed;
            if (canGoRight && (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right) || Game1.keyboard.IsKeyDown(Keys.D)))
                sprite.position.X += playerXSpeed;

            // Jump
            // If pressing jump button, and not jumping or falling
            if ((Game1.pad1.IsButtonDown(Buttons.A) || Game1.keyboard.IsKeyDown(Keys.Space) || Game1.keyboard.IsKeyDown(Keys.Up) || Game1.keyboard.IsKeyDown(Keys.W)) && !isJumping && !isFalling)
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
