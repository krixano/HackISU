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

        static public void enemyInit()
        {
            enemySprite = new Game1.SpriteStruct[15];

            rnd = new Random();

            enemyXSpeed_p = World.BLOCK_SIZE * .15f;
            enemyYSpeed_p = World.BLOCK_SIZE * .10f;

            for (int i=0; i<enemySprite.Length;i++)
            {
                enemySprite[i].size.X = player.sprite.size.X;
                enemySprite[i].size.Y = enemySprite[i].size.Y;
                enemySprite[i].position_wp = player.sprite.position_wp;

            }
        }
        static public void enemyUpdate()
        {

            if (rnd.Next(0, 1000) == 50)
                spawnEnemy();

            //Enemy AI
            for (int i = 0; i < enemySprite.Length; i++)
                if (enemySprite[i].position_wp.X < player.sprite.position_wp.X)
                    enemySprite[i].position_wp.X += enemyXSpeed_p;
            for (int i = 0; i < enemySprite.Length; i++)
                if (enemySprite[i].position_wp.Y < player.sprite.position_wp.Y)
                    enemySprite[i].position_wp.Y += enemyYSpeed_p;
            for (int i = 0; i < enemySprite.Length; i++)
                if (enemySprite[i].position_wp.X > player.sprite.position_wp.X)
                    enemySprite[i].position_wp.X -= enemyXSpeed_p;
            for (int i = 0; i < enemySprite.Length; i++)
                if (enemySprite[i].position_wp.Y > player.sprite.position_wp.Y)
                    enemySprite[i].position_wp.Y -= enemyYSpeed_p;

        }
        static public void spawnEnemy()
        {
            for (int i = 0; i < enemySprite.Length; i++)
            {
                enemySprite[i].position_wp.X = player.sprite.position_wp.X + rnd.Next(-5, 28); 
                //enemySprite[i].position_wp.X +=
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemySprite.Length; i++)
            {
                int x = (int)(enemySprite[i].position_wp.X - World.worldOffsetPixels().X);
                int y = (int)(enemySprite[i].position_wp.Y - World.worldOffsetPixels().Y);
                spriteBatch.Draw(Game1.testTexture, new Rectangle(x, y, (int)player.sprite.size.X, (int)player.sprite.size.Y), Color.White);
            }
        }
    }
}
