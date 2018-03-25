using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static HackISU_2018.Game1;

namespace HackISU_2018
{
    class enemy
    {
        static private double enemyXSpeed_p, enemyYSpeed_p; // _p: In Pixels
        static public Game1.SpriteStruct[] enemySprite;
        static public Random rnd;
        static public int spawnRate;
        static public int enemiesLeft, hitsTilDeath;
        static public long tick;
        public enum EnemiesPerLevel
        {
            level1 = 5,
            level2 = 8,
            level3 = 12,
            level4 = 17,
            level5 = 25
        }
        static public EnemiesPerLevel enemiesPerLevel = EnemiesPerLevel.level1;
        static public void enemyInit()
        {
            hitsTilDeath = 3;

            enemySprite = new Game1.SpriteStruct[(int) enemiesPerLevel];
            spawnRate = 9;
            enemy.rnd = new Random();
            spawnEnemy();
            enemiesLeft = (int) enemiesPerLevel;

            enemyXSpeed_p = World.BLOCK_SIZE * .10f;
            enemyYSpeed_p = World.BLOCK_SIZE * .10f;

            for (int i=0; i<enemySprite.Length;i++)
            {
                enemySprite[i].size.Y = player.sprite.size.Y / 2;
                enemySprite[i].size.X = enemySprite[i].size.Y;
                enemySprite[i].position_wp = new Vector2_Double(player.sprite.position_wp.X, player.sprite.position_wp.Y + 50);
                enemySprite[i].source = new Rectangle(0, 0, 160 / 2, 128 / 2);
                enemySprite[i].visible = false;
                enemySprite[i].health = 100d;
            }
        }
        static public void enemyUpdate()
        {
            tick++;
            if (enemiesLeft == 0)
            {
                enemiesPerLevel = EnemiesPerLevel.level2;
                World.fileName = "map2.txt";
            }
            if (rnd.Next(0, 20) >= 8)
                spawnEnemy();

            //Enemy AI
            for (int i = 0; i < enemySprite.Length; i++)
            {
                if (enemySprite[i].visible)
                {
                    double addFalling = World.BLOCK_SIZE * .25f; // TODO
                    bool canGoLeft = true;
                    bool canGoRight = true;

                    if (isCollidingTopLeftSide(enemySprite[i]))
                        canGoLeft = false;
                    if (isCollidingBottomLeftSide(enemySprite[i]))
                        canGoLeft = false;
                    if (isCollidingTopRightSide(enemySprite[i]))
                        canGoRight = false;
                    if (isCollidingBottomRightSide(enemySprite[i]))
                        canGoRight = false;
                    if (isCollidingMiddleLeftSide(enemySprite[i]))
                        canGoLeft = false;
                    if (isCollidingMiddleRightSide(enemySprite[i]))
                        canGoRight = false;

                    if (((isCollidingBottomLeft(enemySprite[i]) || isCollidingBottomRight(enemySprite[i]))))
                    {
                        enemySprite[i].isFalling = false;
                        addFalling = 0;
                        //enemySprite[i].position_wp.Y = (int) (enemySprite[i].position_wp.Y / World.BLOCK_SIZE) * World.BLOCK_SIZE;
                    }

                    if (isCollidingBottomLeftPlus(enemySprite[i]) || isCollidingBottomRightPlus(enemySprite[i]))
                    {
                        enemySprite[i].isFalling = false;
                    }

                    enemySprite[i].position_wp.Y += addFalling;
                    Console.WriteLine(addFalling);
                    Console.WriteLine(enemySprite[i].position_wp.Y);
                    /*if ((isCollidingBottomLeft(enemySprite[i]) || isCollidingBottomRight(enemySprite[i])))
                    {
                        enemySprite[i].position_wp.Y = ((int)(enemySprite[i].position_wp.Y / World.BLOCK_SIZE) * World.BLOCK_SIZE);
                    }*/


                    /*if (enemySprite[i].position_wp.X - player.sprite.position_wp.X < player.sprite.size.X / 2 && enemySprite[i].position_wp.Y - player.sprite.position_wp.Y < player.sprite.size.Y / 2)
                        hitsTilDeath--;*/

                    /*if (enemySprite[i].health <= 0)
                    {
                        enemySprite[i].visible = false;
                        enemy.enemiesLeft--;
                        enemy.spawnRate -= 100;
                        // Reset health back to 100
                        enemySprite[i].health = 100d;
                    }*/

                    /*if (tick % 20 == 0)
                    {
                        if (enemySprite[i].source.X == 0)
                        {
                            enemySprite[i].source.X = 160 / 2;
                            enemySprite[i].source.Y = 0;
                        }  else if (enemySprite[i].source.X == 160 / 2)
                        {
                            enemySprite[i].source.X = 0;
                            enemySprite[i].source.Y = 128 / 2;
                        } else if (enemySprite[i].source.Y == 128 / 2)
                        {
                            enemySprite[i].source.X = 160 / 2;
                            enemySprite[i].source.Y = 128 / 2;
                        } else
                        {
                            enemySprite[i].source.X = 0;
                            enemySprite[i].source.Y = 0;
                        }
                    }*/
                } else
                {
                    if (!enemySprite[i].visible && enemiesLeft > 0)
                        spawnEnemy();
                }
            }

        }

        static public void EnemyMovement(SpriteStruct enemySprite)
        {
            double addFalling = World.BLOCK_SIZE * .25f; // TODO
            bool canGoLeft = true;
            bool canGoRight = true;

            /*if (isCollidingTopLeftSide(enemySprite))
                canGoLeft = false;
            if (isCollidingBottomLeftSide(enemySprite))
                canGoLeft = false;
            if (isCollidingTopRightSide(enemySprite))
                canGoRight = false;
            if (isCollidingBottomRightSide(enemySprite))
                canGoRight = false;
            if (isCollidingMiddleLeftSide(enemySprite))
                canGoLeft = false;
            if (isCollidingMiddleRightSide(enemySprite))
                canGoRight = false;*/

            /*if (((isCollidingBottomLeft(enemySprite) || isCollidingBottomRight(enemySprite))))
            {
                enemySprite.isFalling = false;
                addFalling = 0;
                enemySprite.position_wp.Y = (int) (enemySprite.position_wp.Y / World.BLOCK_SIZE) * World.BLOCK_SIZE;
            }*/

            if (isCollidingBottomLeftPlus(enemySprite) || isCollidingBottomRightPlus(enemySprite))
            {
                enemySprite.isFalling = false;
            }

            enemySprite.position_wp.Y += addFalling;
            Console.WriteLine(addFalling);
            Console.WriteLine(enemySprite.position_wp.Y);
            /*if ((isCollidingBottomLeft(enemySprite) || isCollidingBottomRight(enemySprite)))
            {
                enemySprite.position_wp.Y = ((int)(enemySprite.position_wp.Y / World.BLOCK_SIZE) * World.BLOCK_SIZE) + 5;
            }*/
        }

        static public void spawnEnemy()
        {
            
            for (int i = 0; i < enemySprite.Length; i++)
            {
                if (!enemySprite[i].visible)
                {
                    //enemySprite[i].position_wp.X = rnd.Next(0, (int) World.WORLD_SIZE.X);
                    enemySprite[i].position_wp.X = enemy.rnd.Next(0, (int) World.WORLD_SIZE.X * World.BLOCK_SIZE);
                    enemySprite[i].position_wp.Y = World.BLOCK_SIZE * 3;
                    enemySprite[i].visible = true;
                }
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemySprite.Length; i++)
            {
                if (enemySprite[i].visible)
                {
                    int x = (int)(enemySprite[i].position_wp.X - World.worldOffsetPixels().X);
                    int y = (int)(enemySprite[i].position_wp.Y - World.worldOffsetPixels().Y);
                    //spriteBatch.Draw(Game1.crabEnemyTexture, new Rectangle(x, y, (int)player.sprite.size.X, (int)player.sprite.size.Y), Color.White);
                    spriteBatch.Draw(Game1.crabEnemyTexture, new Rectangle(x, y, (int) enemySprite[i].size.X, (int) enemySprite[i].size.Y), enemySprite[i].source, Color.White);
                }
            }
        }

        public static bool isCollidingTopLeftSide(SpriteStruct sprite)
        {
            Vector2_Double sideCollisionTopLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionTopLeft.X + (int) sideCollisionTopLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingTopRightSide(SpriteStruct sprite)
        {
            Vector2_Double sideCollisionTopRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionTopRight.X + (int) sideCollisionTopRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingBottomLeftSide(SpriteStruct sprite)
        {
            Vector2_Double sideCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - (sprite.size.Y / 4)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionBottomLeft.X + (int) sideCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingBottomRightSide(SpriteStruct sprite)
        {
            Vector2_Double sideCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y - (sprite.size.Y / 4)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionBottomRight.X + (int) sideCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingMiddleLeftSide(SpriteStruct sprite)
        {
            Vector2_Double sideCollisionMiddleLeft = new Vector2_Double((sprite.position_wp.X - 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + (sprite.size.Y / 2)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionMiddleLeft.X + (int) sideCollisionMiddleLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingMiddleRightSide(SpriteStruct sprite)
        {
            Vector2_Double sideCollisionMiddleRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X + 1) / World.BLOCK_SIZE, (sprite.position_wp.Y + (sprite.size.Y / 2)) / World.BLOCK_SIZE);
            return World.blocks[(int) sideCollisionMiddleRight.X + (int) sideCollisionMiddleRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingBottomLeft(SpriteStruct sprite) // TODO
        {
            Vector2_Double gravityCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 2) / World.BLOCK_SIZE);
            return World.blocks[(int) gravityCollisionBottomLeft.X + (int) gravityCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingBottomRight(SpriteStruct sprite) // TODO
        {
            Vector2_Double gravityCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 2) / World.BLOCK_SIZE);
            return World.blocks[(int) gravityCollisionBottomRight.X + (int) gravityCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingBottomLeftPlus(SpriteStruct sprite) // TODO
        {
            Vector2_Double gravityCollisionBottomLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 5) / World.BLOCK_SIZE);
            return World.blocks[(int) gravityCollisionBottomLeft.X + (int) gravityCollisionBottomLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingBottomRightPlus(SpriteStruct sprite) // TODO
        {
            Vector2_Double gravityCollisionBottomRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X) / World.BLOCK_SIZE, (sprite.position_wp.Y + sprite.size.Y + 5) / World.BLOCK_SIZE);
            return World.blocks[(int) gravityCollisionBottomRight.X + (int) gravityCollisionBottomRight.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingTopLeft(SpriteStruct sprite)
        {
            Vector2_Double collisionTopLeft = new Vector2_Double((sprite.position_wp.X) / World.BLOCK_SIZE, (sprite.position_wp.Y - 1) / World.BLOCK_SIZE);
            return World.blocks[(int) collisionTopLeft.X + (int) collisionTopLeft.Y * (int) World.WORLD_SIZE.X].solid;
        }

        public static bool isCollidingTopRight(SpriteStruct sprite)
        {
            Vector2_Double collisionTopRight = new Vector2_Double((sprite.position_wp.X + sprite.size.X) / World.BLOCK_SIZE, (sprite.position_wp.Y - 1) / World.BLOCK_SIZE);
            return World.blocks[(int) collisionTopRight.X + (int) collisionTopRight.Y * (int) World.WORLD_SIZE.X].solid;
        }
    }
}
