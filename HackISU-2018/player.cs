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
            playerYSpeed_p = World.BLOCK_SIZE * .50f;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 10;
            sprite.position_wp.X = ((Game1.screenRectangle.Center.X - (sprite.size.X / 2)) + World.offset_b.X); // In World Pixels
            sprite.position_wp.Y = 28 * World.BLOCK_SIZE; //(((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;
            
                       
        }
        public static void playerUpdate()
        {
            if (Game1.keyboard.IsKeyDown(Keys.Right) && playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
                World.offset_b.X += .25f;
            if (Game1.keyboard.IsKeyDown(Keys.Left) && playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
                World.offset_b.X -= .25f;

            //Scrolls screen
            /*if (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left))
                sprite.position_wp.X -= playerXSpeed_p;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right))
                sprite.position_wp.X += playerXSpeed_p;*/

            Vector2_Double gravityCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            Vector2_Double gravityCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            //bool isFalling = false;
            isFalling = false;

            Double addFalling = 0;
            
            //checking collision for bottom of player, left and right corners
            if ((World.blocks[(int)gravityCollisionBottomRight.X + (int)gravityCollisionBottomRight.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomRight.X < 0 || gravityCollisionBottomRight.X > World.WORLD_SIZE.X || gravityCollisionBottomRight.Y < 0 || gravityCollisionBottomRight.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                addFalling = playerYSpeed_p;
                isFalling = true;
            } else
            {
                isFalling = false;
                sprite.position_wp.Y = (gravityCollisionBottomRight.Y * World.BLOCK_SIZE) - 1 - sprite.size.Y;
            }
            if ((World.blocks[(int)gravityCollisionBottomLeft.X + (int)gravityCollisionBottomLeft.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomLeft.X < 0 || gravityCollisionBottomLeft.X > World.WORLD_SIZE.X || gravityCollisionBottomLeft.Y < 0 || gravityCollisionBottomLeft.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                addFalling = playerYSpeed_p;
                isFalling = true;
            }
             Vector2_Double sideCollisionTopLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            Vector2_Double sideCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - 1) / World.BLOCK_SIZE);
            Vector2_Double sideCollisionTopRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            Vector2_Double sideCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - 1) / World.BLOCK_SIZE);

            bool canGoLeft = true;
            bool canGoRight = true;

            if (World.blocks[(int) sideCollisionTopLeft.X + (int) sideCollisionTopLeft.Y * (int) World.WORLD_SIZE.X].solid)
                canGoLeft = false;
            //if (World.blocks[(int) sideCollisionBottomLeft.X + (int) sideCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid)
                //canGoLeft = false;
            if (World.blocks[(int) sideCollisionTopRight.X + (int) sideCollisionTopRight.Y * (int) World.WORLD_SIZE.X].solid)
                canGoRight = false;
            //if (World.blocks[(int) sideCollisionBottomRight.X + (int) sideCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid)
            //canGoRight = false;

            //Slow fall if touching wall
            if ((canGoLeft == false || canGoRight == false) && isFalling)
            {
                addFalling = playerYSpeed_p / 5;
                //sprite.position_wp.Y += playerYSpeed_p;
                if (Game1.keyboard.IsKeyDown(Keys.Space) && Game1.prevKeyboard.IsKeyUp(Keys.Space))
                {
                    sprite.position_wp.X += playerXSpeed_p;
                    isFalling = false;                    
                    playerJump();
                }
            }

            //scrolls screen      
            if (canGoRight && Game1.keyboard.IsKeyDown(Keys.Right)
                || Game1.keyboard.IsKeyDown(Keys.D) 
                && playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
                World.offset_b.X += .25f;
            if (canGoRight && Game1.keyboard.IsKeyDown(Keys.Left) 
                || Game1.keyboard.IsKeyDown(Keys.A) 
                && playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
                World.offset_b.X -= .25f;
            //else if (Game1.keyboard.IsKeyDown(Keys.Up) && sprite.position.Y <= Game1.screenRectangle.Bottom - Game1.screenRectangle.Height / 3)
            //    World.offset.Y -= .25f;
            //else if (Game1.keyboard.IsKeyDown(Keys.Down) && sprite.position.Y >= Game1.screenRectangle.Top - Game1.screenRectangle.Height / 3)
            //    World.offset.Y += .25f;

            //moves player
            if (canGoLeft && (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left)
                || Game1.keyboard.IsKeyDown(Keys.A)))
                sprite.position_wp.X -= playerXSpeed_p;
            if (canGoRight && (Game1.pad1.IsButtonDown(Buttons.DPadRight) ||
                Game1.keyboard.IsKeyDown(Keys.D) || Game1.keyboard.IsKeyDown(Keys.Right)))
                sprite.position_wp.X += playerXSpeed_p;

            if (playerScreenPixels().Y >= Game1.screenRectangle.Height / 5 * 3)
            {
                World.offset_b.Y += playerYSpeed_p / World.BLOCK_SIZE;
            }

            // Jump
            // If pressing jump button, and not jumping or falling
            while ((Game1.pad1.IsButtonDown(Buttons.A) && Game1.prevPad1.IsButtonUp(Buttons.A)
                || Game1.keyboard.IsKeyDown(Keys.Space) && Game1.prevKeyboard.IsKeyUp(Keys.Space)
                || Game1.keyboard.IsKeyDown(Keys.Up) && Game1.prevKeyboard.IsKeyUp(Keys.Up)
                || Game1.keyboard.IsKeyDown(Keys.W)) && Game1.prevKeyboard.IsKeyUp(Keys.W) && !isJumping && !isFalling)
                isJumping = true;
            playerJump();

            sprite.position_wp.Y += addFalling;
        }
        

        public static void playerJump()
        {
            if (isJumping && !isFalling)
            {
                
                currentTmr++;
                double increment = playerYSpeed_p ;
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
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            int x = (int) (player.sprite.position_wp.X - World.worldOffsetPixels().X);
            int y = (int) (player.sprite.position_wp.Y - World.worldOffsetPixels().Y);
            spriteBatch.Draw(Game1.testTexture, new Rectangle(x, y, (int) player.sprite.size.X, (int) player.sprite.size.Y), Color.White);
        }

    }
}
