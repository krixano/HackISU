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
        static int tmrAmt = 7;
        static int currentTmr = 0;
        static bool isJumping = false;
        static bool isFalling = false;

        static int currentAnimation = 0;
        static long tick = 0;

        static public void playerInit()
        {
            tmr = new Timer(60);

            playerXSpeed_p = World.BLOCK_SIZE * .25f;
            playerYSpeed_p = World.BLOCK_SIZE * .30f;

            sprite.size.X = Game1.screenRectangle.Width / 20;
            sprite.size.Y = Game1.screenRectangle.Width / 10;
            sprite.position_wp.X = ((Game1.screenRectangle.Center.X - (sprite.size.X / 2)) + World.offset_b.X); // In World Pixels
            sprite.position_wp.Y = 28 * World.BLOCK_SIZE; //(((World.WORLD_SIZE.Y / 2) - 1) * World.BLOCK_SIZE) - sprite.size.Y;
            
                       
        }
        public static void playerUpdate()
        {
            isFalling = false;
            bool isMoving = false;

            Double addFalling = playerYSpeed_p;
            bool canGoLeft = true;
            bool canGoRight = true;

            if (isPlayerCollidingTopLeftSide())
                canGoLeft = false;
            if (isPlayerCollidingBottomLeftSide())
                canGoLeft = false;
            if (isPlayerCollidingTopRightSide())
                canGoRight = false;
            if (isPlayerCollidingBottomRightSide())
                canGoRight = false;
            if (isPlayerCollidingMiddleLeftSide())
                canGoLeft = false;
            if (isPlayerCollidingMiddleRightSide())
                canGoRight = false;
            
            //scrolls screen      
            if (canGoRight && Game1.keyboard.IsKeyDown(Keys.Right)
                || Game1.keyboard.IsKeyDown(Keys.D) 
                && playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
            {
                if (World.offset_b.X >= World.WORLD_SIZE.X)
                {
                    World.offset_b.X = World.WORLD_SIZE.X - Game1.screenRectangle.Width / World.BLOCK_SIZE;
                } else
                {
                    World.offset_b.X += .25f;
                }
            }
            if (canGoLeft && Game1.keyboard.IsKeyDown(Keys.Left) 
                || Game1.keyboard.IsKeyDown(Keys.A) 
                && playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
            {
                if (World.offset_b.X <= 0)
                {
                    World.offset_b.X = 0;
                } else
                {
                    World.offset_b.X -= .25f;
                }
            }

            //moves player
            if (canGoLeft && (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left)
                || Game1.keyboard.IsKeyDown(Keys.A)))
            {
                sprite.position_wp.X -= playerXSpeed_p;
                isMoving = true;
                Game1.playerEffect = SpriteEffects.FlipHorizontally;
                if (isPlayerCollidingMiddleLeftSide() || isPlayerCollidingTopLeftSide())
                {
                    double difference = sprite.position_wp.X - (World.offset_b.X * World.BLOCK_SIZE);
                    sprite.position_wp.X = ((int) (sprite.position_wp.X / World.BLOCK_SIZE) * World.BLOCK_SIZE);
                    World.offset_b.X = (sprite.position_wp.X - difference) / World.BLOCK_SIZE;
                }
            }
            if (canGoRight && (Game1.pad1.IsButtonDown(Buttons.DPadRight) ||
                Game1.keyboard.IsKeyDown(Keys.D) || Game1.keyboard.IsKeyDown(Keys.Right)))
            {
                sprite.position_wp.X += playerXSpeed_p;
                isMoving = true;
                Game1.playerEffect = SpriteEffects.None;
                if (isPlayerCollidingMiddleRightSide() || isPlayerCollidingTopRightSide())
                {
                    double difference = sprite.position_wp.X - (World.offset_b.X * World.BLOCK_SIZE);
                    sprite.position_wp.X = ((int) (sprite.position_wp.X / World.BLOCK_SIZE) * World.BLOCK_SIZE) + Math.Abs(World.BLOCK_SIZE - sprite.size.X) + 5;
                    World.offset_b.X = (sprite.position_wp.X - difference) / World.BLOCK_SIZE;
                }
            }

            bool wallJumpOverride = false;
            //WALL JUMPING
            //Slow fall if touching wall
            if ((canGoLeft == false || canGoRight == false) && isFalling)
            {
                wallJumpOverride = true;
                addFalling = playerYSpeed_p / 5;
                //sprite.position_wp.Y += playerYSpeed_p;
                if (Game1.keyboard.IsKeyDown(Keys.Space) && Game1.prevKeyboard.IsKeyUp(Keys.Space))
                {
                    //playerXSpeed_p += playerXSpeed_p;
                    isFalling = false;
                    playerJump();
                }
            }
            

            if (Game1.keyboard.IsKeyDown(Keys.Right) && playerScreenPixels().X >= Game1.screenRectangle.Right - Game1.screenRectangle.Width / 3 + sprite.size.Y / 2)
                World.offset_b.X += .25f;
            if (Game1.keyboard.IsKeyDown(Keys.Left) && playerScreenPixels().X <= Game1.screenRectangle.Left + Game1.screenRectangle.Width / 3 - sprite.size.X / 2)
                World.offset_b.X -= .25f;

            //Scrolls screen
            /*if (Game1.pad1.IsButtonDown(Buttons.DPadLeft) || Game1.keyboard.IsKeyDown(Keys.Left))
                sprite.position_wp.X -= playerXSpeed_p;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight) || Game1.keyboard.IsKeyDown(Keys.Right))
                sprite.position_wp.X += playerXSpeed_p;*/

            /*Vector2_Double gravityCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            Vector2_Double gravityCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);*/
            
            //checking collision for bottom of player, left and right corners
            /*if ((World.blocks[(int)gravityCollisionBottomRight.X + (int)gravityCollisionBottomRight.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomRight.X < 0 || gravityCollisionBottomRight.X > World.WORLD_SIZE.X || gravityCollisionBottomRight.Y < 0 || gravityCollisionBottomRight.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                addFalling = playerYSpeed_p;
                isFalling = true;
            }*//* else
            {
                isFalling = false;
                sprite.position_wp.Y = (gravityCollisionBottomRight.Y * World.BLOCK_SIZE) - 1 - sprite.size.Y;
            }*/
            /*if ((World.blocks[(int)gravityCollisionBottomLeft.X + (int)gravityCollisionBottomLeft.Y * (int)World.WORLD_SIZE.X].solid != true
                || gravityCollisionBottomLeft.X < 0 || gravityCollisionBottomLeft.X > World.WORLD_SIZE.X || gravityCollisionBottomLeft.Y < 0 || gravityCollisionBottomLeft.Y > World.WORLD_SIZE.Y)
                && !isJumping)
            {
                addFalling = playerYSpeed_p;
                isFalling = true;
            }*/




            if (playerScreenPixels().Y >= Game1.screenRectangle.Height / 5 * 3)
            {
                if (World.offset_b.Y >= World.WORLD_SIZE.Y)
                {
                    World.offset_b.Y = World.WORLD_SIZE.Y - (Game1.screenRectangle.Height / World.BLOCK_SIZE);
                } else
                {
                    World.offset_b.Y += playerYSpeed_p / World.BLOCK_SIZE;
                }
            } else if (playerScreenPixels().Y <= Game1.screenRectangle.Height / 5)
            {
                if (World.offset_b.Y <= 0)
                {
                    World.offset_b.Y = 0;
                } else
                {
                    World.offset_b.Y -= playerYSpeed_p / World.BLOCK_SIZE;
                }
            }

            // Jump
            // If pressing jump button, and not jumping or falling
            while ((Game1.pad1.IsButtonDown(Buttons.A) && Game1.prevPad1.IsButtonUp(Buttons.A)
                || Game1.keyboard.IsKeyDown(Keys.Space) && Game1.prevKeyboard.IsKeyUp(Keys.Space)
                || Game1.keyboard.IsKeyDown(Keys.Up) && Game1.prevKeyboard.IsKeyUp(Keys.Up)
                || Game1.keyboard.IsKeyDown(Keys.W)) && Game1.prevKeyboard.IsKeyUp(Keys.W) && !isJumping && !isFalling)
                isJumping = true;
            playerJump();

            if (!wallJumpOverride && (((isPlayerCollidingBottomLeft() || isPlayerCollidingBottomRight()))
                || isJumping))
            {
                Console.WriteLine("Test");
                isFalling = false;
                addFalling = 0;
                sprite.position_wp.Y = (int) (sprite.position_wp.Y / World.BLOCK_SIZE) * World.BLOCK_SIZE;
            }


            sprite.position_wp.Y += addFalling;
            if (!wallJumpOverride && (isPlayerCollidingBottomLeft() || isPlayerCollidingBottomRight()))
            {
                sprite.position_wp.Y = ((int) (sprite.position_wp.Y / World.BLOCK_SIZE) * World.BLOCK_SIZE) + 5;
            }
            Console.WriteLine(addFalling);

            tick++;
            if (isJumping)
            {
                currentAnimation = 3;
                Game1.playerAnimation.Y = 840/4 * currentAnimation;
            }
            else if (isMoving)
            {
                if (tick % 15 == 0)
                {
                    if (currentAnimation == 3) currentAnimation = 0;
                    currentAnimation++;
                    if (currentAnimation == 3) currentAnimation = 1;
                    Game1.playerAnimation.Y = 840/4 * currentAnimation;
                }
            } else
            {
                currentAnimation = 0;
                Game1.playerAnimation.Y = 840/4 * currentAnimation;
            }
        }
        

        public static void playerJump()
        {

            if (isJumping && !isFalling)
            {
                
                currentTmr++;
                sprite.position_wp.Y -= playerYSpeed_p;
                if (isPlayerCollidingTopLeft() || isPlayerCollidingTopRight())
                {
                    isJumping = false;
                    isFalling = true;
                }
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
            //spriteBatch.Draw(Game1.playerTexture, new Rectangle(x, y, (int) player.sprite.size.X, (int) player.sprite.size.Y), Game1.playerAnimation, Color.White);
            spriteBatch.Draw(Game1.playerTexture, new Rectangle(x, y, (int) player.sprite.size.X, (int) player.sprite.size.Y), Game1.playerAnimation, Color.White, 0, new Vector2(0, 0), Game1.playerEffect, 0);
        }

        public static bool isPlayerCollidingTopLeftSide()
        {
            Vector2_Double sideCollisionTopLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionTopLeft.X + (int) sideCollisionTopLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingTopRightSide()
        {
            Vector2_Double sideCollisionTopRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionTopRight.X + (int) sideCollisionTopRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingBottomLeftSide()
        {
            Vector2_Double sideCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - (sprite.size.Y / 4)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionBottomLeft.X + (int) sideCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingBottomRightSide()
        {
            Vector2_Double sideCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - (sprite.size.Y / 4)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionBottomRight.X + (int) sideCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingMiddleLeftSide()
        {
            Vector2_Double sideCollisionMiddleLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + (sprite.size.Y / 2)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionMiddleLeft.X + (int) sideCollisionMiddleLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingMiddleRightSide()
        {
            Vector2_Double sideCollisionMiddleRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + (sprite.size.Y / 2)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionMiddleRight.X + (int) sideCollisionMiddleRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingBottomLeft() // TODO
        {
            Vector2_Double gravityCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            return World.blocks[(int) gravityCollisionBottomLeft.X + (int) gravityCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingBottomRight() // TODO
        {
            Vector2_Double gravityCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 1) / World.BLOCK_SIZE);
            return World.blocks[(int) gravityCollisionBottomRight.X + (int) gravityCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingTopLeft()
        {
            Vector2_Double collisionTopLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y - 1) / World.BLOCK_SIZE);
            return World.blocks[(int) collisionTopLeft.X + (int) collisionTopLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isPlayerCollidingTopRight()
        {
            Vector2_Double collisionTopRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X) / World.BLOCK_SIZE, (sprite.position_wp.Y - 1) / World.BLOCK_SIZE);
            return World.blocks[(int) collisionTopRight.X + (int) collisionTopRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

    }
}
