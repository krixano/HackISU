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
        static private double playerXSpeed_p, playerYSpeed_p; // _p: In Pixels
        static public Game1.SpriteStruct sprite;
        static Timer tmr;
        //tmrAmt = Jump length
        static int tmrAmt = 10;
        static int currentTmr = 0;
        static bool isJumping = false;
        static bool isFalling = false;

        static public void playerInit()
        {
            tmr = new Timer(60);

            playerXSpeed_p = World.BLOCK_SIZE * .25f;
            playerYSpeed_p = World.BLOCK_SIZE * .25f;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 20;
            sprite.position_wp.X = ((Game1.screenRectangle.Center.X - (sprite.size.X / 2) / World.BLOCK_SIZE) + World.offset_b.X); // In World Pixels
            sprite.position_wp.Y = (((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;
            
                       
        }
        public static void playerUpdate()
        {
            // Gravity/Ground Collision
            if (!isJumping || isFalling)
            {
                double increment = playerYSpeed_p;
                double x1 = (sprite.position_wp.X) / World.BLOCK_SIZE;
                double x2 = (sprite.position_wp.X + sprite.size.X) / World.BLOCK_SIZE;
                double y = (sprite.position_wp.Y + sprite.size.Y + increment) / World.BLOCK_SIZE;
                int i = (int) (x1 + y * World.WORLD_SIZE.X);
                int i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                while (World.blocks[i].solid || World.blocks[i2].solid)
                {
                    isFalling = false;
                    increment--;
                    if (increment <= 0) {
                        increment = 0;
                        break;
                    }

                    y = (sprite.position_wp.Y + sprite.size.Y + increment) / World.BLOCK_SIZE;

                    i = (int) (x1 + y * World.WORLD_SIZE.X);
                    i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                }
                sprite.position_wp.Y += increment;
            }

            /*Vector2 sideCollisionTopLeft = new Vector2((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            Vector2 sideCollisionBottomLeft = new Vector2((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - 1) / World.BLOCK_SIZE);
            Vector2 sideCollisionTopRight = new Vector2((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            Vector2 sideCollisionBottomRight = new Vector2((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - 1) / World.BLOCK_SIZE);*/

            // Controls player moving left and right
            Console.WriteLine("Key Down?: ", Game1.keyboard.IsKeyDown(Keys.Left));
            if (Game1.keyboard.IsKeyDown(Keys.Right) || Game1.keyboard.IsKeyDown(Keys.D))
            {
                double increment = .25f;
                double x1 = (sprite.position_wp.X + increment) / World.BLOCK_SIZE;
                double x2 = (sprite.position_wp.X + sprite.size.X + increment) / World.BLOCK_SIZE;
                double y1 = (sprite.position_wp.Y) / World.BLOCK_SIZE;
                double y2 = (sprite.position_wp.Y + sprite.size.Y - 5) / World.BLOCK_SIZE;
                int i1 = (int) (x1 + y1 * World.WORLD_SIZE.X);
                int i2 = (int) (x1 + y2 * World.WORLD_SIZE.X);
                int i3 = (int) (x2 + y1 * World.WORLD_SIZE.X);
                int i4 = (int) (x2 + y2 * World.WORLD_SIZE.X);
                while (World.blocks[i1].solid || World.blocks[i2].solid || World.blocks[i3].solid || World.blocks[i4].solid)
                {
                    increment -= 1d / (double) World.BLOCK_SIZE;
                    if (increment <= 0) {
                        increment = 0;
                        break;
                    }
                    
                    x1 = (sprite.position_wp.X + increment) / World.BLOCK_SIZE;
                    x2 = (sprite.position_wp.X + sprite.size.X + increment) / World.BLOCK_SIZE;

                    i1 = (int) (x1 + y1 * World.WORLD_SIZE.X);
                    i2 = (int) (x1 + y2 * World.WORLD_SIZE.X);
                    i3 = (int) (x2 + y1 * World.WORLD_SIZE.X);
                    i4 = (int) (x2 + y2 * World.WORLD_SIZE.X);
                }
                Console.WriteLine("Increment: " + increment);
                sprite.position_wp.X += increment * World.BLOCK_SIZE;
                if (playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
                    World.offset_b.X += increment;
            }
            if (Game1.keyboard.IsKeyDown(Keys.Left) || Game1.keyboard.IsKeyDown(Keys.A))
            {
                double increment = .25f;
                double x1 = (sprite.position_wp.X - increment) / World.BLOCK_SIZE;
                double x2 = (sprite.position_wp.X + sprite.size.X - increment) / World.BLOCK_SIZE;
                double y1 = (sprite.position_wp.Y) / World.BLOCK_SIZE;
                double y2 = (sprite.position_wp.Y + sprite.size.Y - 5) / World.BLOCK_SIZE;
                int i1 = (int) (x1 + y1 * World.WORLD_SIZE.X);
                int i2 = (int) (x1 + y2 * World.WORLD_SIZE.X);
                int i3 = (int) (x2 + y1 * World.WORLD_SIZE.X);
                int i4 = (int) (x2 + y2 * World.WORLD_SIZE.X);
                while (World.blocks[i1].solid || World.blocks[i2].solid || World.blocks[i3].solid || World.blocks[i4].solid)
                {
                    increment -= 1.0d / (double) World.BLOCK_SIZE;
                    if (increment <= 0) {
                        increment = 0;
                        break;
                    }
                    
                    x1 = (sprite.position_wp.X - increment) / World.BLOCK_SIZE;
                    x2 = (sprite.position_wp.X + sprite.size.X - increment) / World.BLOCK_SIZE;

                    i1 = (int) (x1 + y1 * World.WORLD_SIZE.X);
                    i2 = (int) (x1 + y2 * World.WORLD_SIZE.X);
                    i3 = (int) (x2 + y1 * World.WORLD_SIZE.X);
                    i4 = (int) (x2 + y2 * World.WORLD_SIZE.X);
                }
                sprite.position_wp.X -= increment * World.BLOCK_SIZE;
                Console.WriteLine("Increment: " +  increment);
                if (playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
                    World.offset_b.X -= increment;
            }
            

            // Scrolls screen
            /*if (canGoLeft && (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left) || Game1.keyboard.IsKeyDown(Keys.A)))
                sprite.position_wp.X -= playerXSpeed_p;*/
            /*if (canGoRight && (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right) || Game1.keyboard.IsKeyDown(Keys.D)))
                sprite.position_wp.X += playerXSpeed_p;*/

            // Jump
            // If pressing jump button, and not jumping or falling
            if ((Game1.pad1.IsButtonDown(Buttons.A) || Game1.keyboard.IsKeyDown(Keys.Space) || Game1.keyboard.IsKeyDown(Keys.Up) || Game1.keyboard.IsKeyDown(Keys.W)) && !isJumping && !isFalling)
                isJumping = true;
            playerJump();
        }
        

        public static void playerJump()
        {
            if (isJumping && !isFalling)
            {
                currentTmr++;
                double increment = playerYSpeed_p * 2;
                double x1 = (sprite.position_wp.X) / World.BLOCK_SIZE;
                double x2 = (sprite.position_wp.X + sprite.size.X) / World.BLOCK_SIZE;
                double y = (sprite.position_wp.Y - increment - 2) / World.BLOCK_SIZE;
                int i = (int) (x1 + y * World.WORLD_SIZE.X);
                int i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                while (World.blocks[i].solid || World.blocks[i2].solid)
                {
                    isFalling = false;
                    increment--;
                    if (increment <= 0) {
                        increment = 0;
                        break;
                    }

                    y = (sprite.position_wp.Y - increment - 2) / World.BLOCK_SIZE;

                    i = (int) (x1 + y * World.WORLD_SIZE.X);
                    i2 = (int) (x2 + y * World.WORLD_SIZE.X);
                }

                sprite.position_wp.Y -= increment;
                if (currentTmr == tmrAmt)
                {
                    currentTmr = 0;
                    isJumping = false;
                    isFalling = true;
                }
            }
            
        }        

        public static Vector2_Double playerScreenPixels()
        {
            return new Vector2_Double(player.sprite.position_wp.X - (World.offset_b.X * World.BLOCK_SIZE), player.sprite.position_wp.Y - (World.offset_b.Y * World.BLOCK_SIZE));
        }

        public static Vector2_Double playerWorldBlocks()
        {
            return new Vector2_Double(player.sprite.position_wp.X / World.BLOCK_SIZE, player.sprite.position_wp.Y / World.BLOCK_SIZE);
        }

    }
}
