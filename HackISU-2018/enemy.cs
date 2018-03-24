using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackISU_2018
{
    class enemy
    {
        static private double enemyXSpeed_p, enemyYSpeed_p; // _p: In Pixels
        static public Game1.SpriteStruct[] enemySprite;
        static public Random rnd;

        static public void enemyInit()
        {
            Game1.SpriteStruct[] enemySprite = new Game1.SpriteStruct[15];
            Random rnd = new Random();

            for (int i=0; i<enemySprite.Length;i++)
            {
                enemySprite[i].size = player.sprite.size;                
            }
        }
        static public void enemyUpdate()
        {
            
                if (rnd.Next(0, 100) == 50)
                    spawnEnemy();
        }
        static public void spawnEnemy()
        {
            for (int i = 0; i < enemySprite.Length; i++)
            {
                enemySprite[i].position_wp.X = player.sprite.position_wp.X + rnd.Next(-28, 28); 
                //enemySprite[i].position_wp.X +=
            }
        }
    }
}
