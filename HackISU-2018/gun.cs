﻿using Microsoft.Xna.Framework;
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

        static public void gunInit()
        {
            bulletSpeed = Game1.screenRectangle.Width / 20;
            gunArm.size.X = player.sprite.size.X;
            gunArm.size.Y = gunArm.size.X / 3;
            gunArm.position.X = player.sprite.position.X + (player.sprite.size.X / 2);
            gunArm.position.Y = player.sprite.position.Y + (player.sprite.size.Y / 2);

            bullet = new Game1.SpriteStruct[100]; 
            for (int i=0; i< bullet.Length; i++)
            {
                bullet[i].isFired = false;
                bullet[i].size.X = gunArm.size.X / 4;
                bullet[i].size.Y = gunArm.size.Y / 2;
                bullet[i].position.X = gunArm.position.X + gunArm.size.X / 2 - bullet[i].size.X / 2;
                bullet[i].position.Y = gunArm.position.Y + gunArm.size.Y / 2 - bullet[i].size.Y / 2;
            }
            
        }
        public static void gunUpdate()
        {
            gunArm.position.X = player.sprite.position.X + gunArm.size.X / 2;
            gunArm.position.Y = player.sprite.position.Y + gunArm.size.Y;
            gunArm.rotation = (float) getMouseAngle();



            if (Game1.mouse.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < bullet.Length; i++)
                { 
                bullet[i].position.X = gunArm.position.X + gunArm.size.X / 2 - bullet[i].size.X / 2;
                bullet[i].position.Y = gunArm.position.Y + gunArm.size.Y / 2 - bullet[i].size.Y / 2;
                    bullet[i].rotation = gunArm.rotation;
                }
                shootGun();
            }

            for (int i = 0; i < bullet.Length; i++)
            {

                if (bullet[i].isFired)
                {

                    bullet[i].position.X += bulletSpeed * (float)Math.Cos(getMouseAngle());
                    bullet[i].position.Y += bulletSpeed * (float)Math.Sin(getMouseAngle());
                    if (bullet[i].position.X > Game1.screenRectangle.Width * 2 || bullet[i].position.Y > Game1.screenRectangle.Height * 2
                        || bullet[i].position.X < Game1.screenRectangle.Width * -2 || bullet[i].position.Y < Game1.screenRectangle.Height * -2)
                        bullet[i].isFired = false;
                }
            }
        }
        public static double getMouseAngle()
        {
            Vector2 distance;
            double angle;
            
            distance.X = Game1.mouse.X - (gunArm.position.X - World.offset.X * World.BLOCK_SIZE);
            distance.Y = Game1.mouse.Y - (gunArm.position.Y - World.offset.Y * World.BLOCK_SIZE);
                                  
            angle = Math.Atan2(distance.Y , distance.X);            
            
            return angle;
        }
        public static void shootGun()
        {
            for (int i=0; i<bullet.Length; i++)
            {
                if (!bullet[i].isFired)
                {
                    bullet[i].isFired = true;
                    bullet[i].rotation = (float)getMouseAngle();
                    break;
                }
            }
        }
    }
}
