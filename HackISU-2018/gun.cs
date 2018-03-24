using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    class gun
    {
        static public Game1.SpriteStruct gunArm;
        static public Game1.SpriteStruct[] bullet;
        static public int bulletSpeed;
        static public float bulletSize, rateOfFire, tick;     
        
        enum gunSelection
        {
            HANDGUN = 40,
            SMG = 30,
            ASSAULT_RIFLE = 10,
            SHOTGUN = 50;

        }

        static public void gunInit()
        {
            //INITIALIZATION
            bulletSpeed = Game1.screenRectangle.Width / 20;
            bulletSize = gunArm.size.X / 4;
            //Rate Of Fire: The Higher it is the slower you shoot (out of 60)
            rateOfFire = 40;
            tick = 0;

            gunArm.size.X = player.sprite.size.X;
            gunArm.size.Y = gunArm.size.X / 3;


            bullet = new Game1.SpriteStruct[100]; 
            for (int i=0; i< bullet.Length; i++)
            {
                bullet[i].isFired = false;
                bullet[i].size.X = gunArm.size.X / 4;
                bullet[i].size.Y = gunArm.size.Y / 2;
                bullet[i].position_wp.X = gunArm.position_wp.X + gunArm.size.X / 2 - bullet[i].size.X / 2;
                bullet[i].position_wp.Y = gunArm.position_wp.Y + gunArm.size.Y / 2 - bullet[i].size.Y / 2;
            }
            
            //Gun Arm Position Init
            gunArm.position_wp.X = player.sprite.position_wp.X + (player.sprite.size.X / 2);
            gunArm.position_wp.Y = player.sprite.position_wp.Y + (player.sprite.size.Y / 2);            

        }
        public static void gunUpdate()
        {
            //Gun Arm Position Update
            gunArm.position_wp.X = player.sprite.position_wp.X + player.sprite.size.X / 2;
            gunArm.position_wp.Y = player.sprite.position_wp.Y + player.sprite.size.Y / 2;
            gunArm.rotation = (float) getMouseAngle();
            tick++;


            //Shoots gun at selected Rate of Fire
            if (Game1.mouse.LeftButton == ButtonState.Pressed
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
                                if (bullet[i].position_wp.X > Game1.screenRectangle.Width * 2 || bullet[i].position_wp.Y > Game1.screenRectangle.Height * 2
                                    || bullet[i].position_wp.X < Game1.screenRectangle.Width * -2 || bullet[i].position_wp.Y < Game1.screenRectangle.Height * -2)
                                    bullet[i].isFired = false;
                            }

                        }
                    
        }
        public static double getMouseAngle()
        {
            //Returns angle of mouse and aim
            Vector2 distance;
            double angle;            
            distance.X = Game1.mouse.X - (gunArm.position_wp.X - World.offset_b.X * World.BLOCK_SIZE);
            distance.Y = Game1.mouse.Y - (gunArm.position_wp.Y - World.offset_b.Y * World.BLOCK_SIZE);                                  
            angle = Math.Atan2(distance.Y, distance.X);                        
            return angle;
        }
        public static void shootGun()
        {
            //Do this whenever you click/bullet fired
            for (int i=0; i<bullet.Length; i++)
            {
                if (!bullet[i].isFired)
                {
                    bullet[i].position_wp.X = player.sprite.position_wp.X + player.sprite.size.X / 2;                    
                    bullet[i].position_wp.Y = player.sprite.position_wp.Y + player.sprite.size.Y / 2;
                    bullet[i].isFired = true;                    
                    bullet[i].rotation = gunArm.rotation;
                    break;
                }
            }
        }
    }
}
