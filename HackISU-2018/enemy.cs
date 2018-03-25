using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
                enemySprite[i].position_wp = player.sprite.position_wp;
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
                    if (enemySprite[i].position_wp.X < player.sprite.position_wp.X)
                        enemySprite[i].position_wp.X += enemyXSpeed_p;
                    if (enemySprite[i].position_wp.Y < player.sprite.position_wp.Y)
                        enemySprite[i].position_wp.Y += enemyYSpeed_p;
                    if (enemySprite[i].position_wp.X > player.sprite.position_wp.X)
                        enemySprite[i].position_wp.X -= enemyXSpeed_p;
                    if (enemySprite[i].position_wp.Y > player.sprite.position_wp.Y)
                        enemySprite[i].position_wp.Y -= enemyYSpeed_p;

                    if (enemySprite[i].position_wp.X - player.sprite.position_wp.X < player.sprite.size.X / 2 && enemySprite[i].position_wp.Y - player.sprite.position_wp.Y < player.sprite.size.Y / 2)
                        hitsTilDeath--;

                    if (enemySprite[i].health <= 0)
                    {
                        enemySprite[i].visible = false;
                        enemy.enemiesLeft--;
                        enemy.spawnRate -= 100;
                        // Reset health back to 100
                        enemySprite[i].health = 100d;
                    }

                    if (tick % 20 == 0)
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
                    }
                } else
                {
                    if (!enemySprite[i].visible && enemiesLeft > 0)
                        spawnEnemy();
                }
            }

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
    }
}
