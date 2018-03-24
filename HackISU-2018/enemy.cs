using Microsoft.Xna.Framework.Graphics;
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
    class enemy
    {
        static private double enemyXSpeed_p, enemyYSpeed_p; // _p: In Pixels
        static public Game1.SpriteStruct[] enemySprite;
        static public Random rnd;
        static public int spawnRate;
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
            
            enemySprite = new Game1.SpriteStruct[(int) enemiesPerLevel];
            spawnRate = 900;
            rnd = new Random();

            enemyXSpeed_p = World.BLOCK_SIZE * .10f;
            enemyYSpeed_p = World.BLOCK_SIZE * .10f;

            for (int i=0; i<enemySprite.Length;i++)
            {
                enemySprite[i].size.X = player.sprite.size.X;
                enemySprite[i].size.Y = enemySprite[i].size.X;
                enemySprite[i].position_wp = player.sprite.position_wp;
                enemySprite[i].visible = false;
            }
        }
        static public void enemyUpdate()
        {

            if (rnd.Next(0, 1000) >= spawnRate)
                spawnEnemy();

            //Enemy AI
            for (int i = 0; i < enemySprite.Length; i++)
            {
                if (enemySprite[i].position_wp.X < player.sprite.position_wp.X)
                    enemySprite[i].position_wp.X += enemyXSpeed_p;
                else if (enemySprite[i].position_wp.Y < player.sprite.position_wp.Y)
                    enemySprite[i].position_wp.Y += enemyYSpeed_p;
                else if (enemySprite[i].position_wp.X > player.sprite.position_wp.X)
                    enemySprite[i].position_wp.X -= enemyXSpeed_p;
                else if (enemySprite[i].position_wp.Y > player.sprite.position_wp.Y)
                    enemySprite[i].position_wp.Y -= enemyYSpeed_p;
                if (enemySprite[i].visible = false)
                    spawnEnemy();
            }

        }
        static public void spawnEnemy()
        {
            
            for (int i = 0; i < enemySprite.Length; i++)
            {

                enemySprite[i].position_wp.X = rnd.Next(0, (int) World.WORLD_SIZE.X);
                enemySprite[i].position_wp.Y = 0;
                enemySprite[i].visible = true;
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
                    spriteBatch.Draw(Game1.testTexture, new Rectangle(x, y, (int)player.sprite.size.X, (int)player.sprite.size.Y), Color.White);
                }
            }
        }
    }
}
