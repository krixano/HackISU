using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    class gun
    {
        static public Game1.SpriteStruct gunArm, shell;
        static public Game1.SpriteStruct[] bullet;
        static public int bulletSpeed, shotgunSpread;
        static public float bulletSize, shellSize, rateOfFire, tick;

        
        public enum GunSelections
        {
            HANDGUN = 35,
            SMG = 25,
            ASSAULT_RIFLE = 10,
            SHOTGUN = 45
        }
        static public GunSelections gunSelection = GunSelections.SHOTGUN;

        static public void gunInit()
        {
            //INITIALIZATION            
            shotgunSpread = 5;
            bulletSpeed = Game1.screenRectangle.Width / 15;

            shell.position_wp.X = gunArm.position_wp.X;
            shell.position_wp.Y = gunArm.position_wp.Y;

            //Rate Of Fire: The Higher it is the slower you shoot (out of 60)
            rateOfFire = (float)gunSelection;
            tick = 0;

            //Gun Arm Size
            gunArm.size.X = player.sprite.size.X;
            gunArm.size.Y = gunArm.size.X / 3;
            Console.WriteLine(gunArm.size.X + " " + gunArm.size.Y);

            //Bullet Sizes
            if (gunSelection == GunSelections.HANDGUN)
                bulletSize = gunArm.size.X / 4;
            else if (gunSelection == GunSelections.ASSAULT_RIFLE)
                bulletSize = gunArm.size.X / 8;
            else if (gunSelection == GunSelections.SMG)
                bulletSize = gunArm.size.X / 6;
            else if (gunSelection == GunSelections.SHOTGUN)
            {
                bulletSize = gunArm.size.X / 12;
                shell.size.X = bulletSize;
                shell.size.Y = bulletSize;
                Game1.gunArmTexture = Game1.shotgunTexture;
            }

            bullet = new Game1.SpriteStruct[100]; 
            for (int i=0; i< bullet.Length; i++)
            {
                bullet[i].isFired = false;
                bullet[i].size.X = bulletSize;
                bullet[i].size.Y = bulletSize;
                bullet[i].position_wp.X = gunArm.position_wp.X + gunArm.size.X / 2 - bullet[i].size.X / 2;
                bullet[i].position_wp.Y = gunArm.position_wp.Y + gunArm.size.Y / 2 - bullet[i].size.Y / 2;
            }
            
            //Gun Arm Position Init
            gunArm.position_wp.X = player.sprite.position_wp.X + (player.sprite.size.X / 2);
            gunArm.position_wp.Y = player.sprite.position_wp.Y + (player.sprite.size.Y / 2);            

        }
        public static void gunUpdate()
        {
            checkForBulletCollision();

            //Gun Arm Position Update
            gunArm.position_wp.X = player.sprite.position_wp.X + player.sprite.size.X / 2;
            gunArm.position_wp.Y = player.sprite.position_wp.Y + player.sprite.size.Y / 2;
            gunArm.rotation = (float) getMouseAngle();
            tick++;

            //Shoots SHOTGUN at SHOTGUN (50) Rate of Fire
            if (Game1.mouse.LeftButton == ButtonState.Pressed
                && tick % rateOfFire == 0 && gunSelection == GunSelections.SHOTGUN)
            {
                shootGun();
            }

            

            //Shoots gun at selected Rate of Fire
            else if (Game1.mouse.LeftButton == ButtonState.Pressed
                && tick % rateOfFire == 0)
            {
                shootGun();
                
            }
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].isFired)
                {
                    //This is what happens when a bullet is fired
                    bullet[i].position_wp.X += bulletSpeed * (float)Math.Cos(bullet[i].rotation);
                    bullet[i].position_wp.Y += bulletSpeed * (float)Math.Sin(bullet[i].rotation);
                    shell.position_wp.X = gunArm.position_wp.X;
                    shell.position_wp.Y = gunArm.position_wp.Y;
                    //if (bullet[i].position_wp.X > Game1.screenRectangle.Width * 2 || bullet[i].position_wp.Y > Game1.screenRectangle.Height * 2
                    //    || bullet[i].position_wp.X < Game1.screenRectangle.Width * -2 || bullet[i].position_wp.Y < Game1.screenRectangle.Height * -2)
                    //    bullet[i].isFired = false;
                }
                if (bullet[i].position_wp.X > World.WORLD_SIZE.X || bullet[i].position_wp.X < World.WORLD_SIZE.X
                    || bullet[i].position_wp.Y > World.WORLD_SIZE.Y || bullet[i].position_wp.Y < World.WORLD_SIZE.Y)
                    bullet[i].visible = false;

            }
        }
        public static double getMouseAngle()
        {
            //Returns angle of mouse and aim
            Vector2_Double distance;
            double angle;            
            distance.X = Game1.mouse.X - (gunArm.position_wp.X - World.offset_b.X * World.BLOCK_SIZE);
            distance.Y = Game1.mouse.Y - (gunArm.position_wp.Y - World.offset_b.Y * World.BLOCK_SIZE);                                  
            angle = Math.Atan2(distance.Y, distance.X);                        
            return angle;
        }
        public static void shootGun()
        {
            
            //Do this whenever you click/bullet fired
            for (int i=0; i<bullet.Length; i+= shotgunSpread)
            {              
                
                if (!bullet[i].isFired && gunSelection == GunSelections.SHOTGUN)
                {                    
                    
                    for (int j = 0; j < shotgunSpread; j++)
                    {
                        bullet[j].position_wp.X = player.sprite.position_wp.X + player.sprite.size.X / 2;
                        bullet[j].position_wp.Y = player.sprite.position_wp.Y + player.sprite.size.Y / 2;
                        bullet[j].isFired = true;
                        //gives shotgun spread (Math.PI * 1/3 is how spread out)
                        bullet[j].rotation = (float)( (Math.PI * 1/3) / shotgunSpread * j) + (float) (gunArm.rotation - (Math.PI * 1/3) / 2);
                    }
                    break;
                }
                else if (!bullet[i].isFired)
                    {
                        bullet[i].position_wp.X = player.sprite.position_wp.X + player.sprite.size.X / 2;                    
                        bullet[i].position_wp.Y = player.sprite.position_wp.Y + player.sprite.size.Y / 2;
                        bullet[i].isFired = true;                    
                        bullet[i].rotation = gunArm.rotation;
                        break;
                    }
            }
        }
        public static void checkForBulletCollision()
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                for (int j = 0; j < enemy.enemySprite.Length; j++)
                {
                    if (bullet[i].isFired)
                    {
                        if (bullet[i].position_wp.X >= enemy.enemySprite[j].position_wp.X && bullet[i].position_wp.X <= enemy.enemySprite[j].position_wp.X + enemy.enemySprite[j].size.X
                            && bullet[i].position_wp.Y <= enemy.enemySprite[j].position_wp.Y + enemy.enemySprite[j].size.Y && bullet[i].position_wp.Y >= enemy.enemySprite[j].size.Y)
                        {
                            enemy.enemySprite[j].health -= 5; // 5% hit/decrease
                            bullet[i].isFired = false;
                            //enemy.enemySprite[j].visible = false;
                            //enemy.spawnRate -= 100;
                            //enemy.enemiesLeft--;
                        }
                    }
                    //if (bullet[i].position_wp.X )
                }
            }
        }
        public static void switchWeapons()
        {
            
        }
    }
}
