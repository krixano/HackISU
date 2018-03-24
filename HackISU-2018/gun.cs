using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    class gun
    {
        static public Game1.SpriteStruct gunArm;
        static public void gunInit()
        {
            gunArm.size.X = player.sprite.size.X;
            gunArm.size.Y = gunArm.size.X / 3;
            gunArm.position_wp.X = player.sprite.position_wp.X + (player.sprite.size.X / 2);
            gunArm.position_wp.Y = player.sprite.position_wp.Y + (player.sprite.size.Y / 2);
            //gunArm.origin.X += gunArm.size.Y;
            //gunArm.origin.Y += gunArm.size.Y / 2;
        }
        public static void gunUpdate()
        {
            gunArm.position_wp.X = player.sprite.position_wp.X + gunArm.size.X / 2;
            gunArm.position_wp.Y = player.sprite.position_wp.Y + gunArm.size.Y;
            gunArm.rotation = (float) getMouseAngle();
        }
        public static double getMouseAngle()
        {
            Vector2 distance;
            double angle;
            
            distance.X = Game1.mouse.X - (gunArm.position_wp.X - World.offset_b.X * World.BLOCK_SIZE);
            distance.Y = Game1.mouse.Y - (gunArm.position_wp.Y - World.offset_b.Y * World.BLOCK_SIZE);
                                  
            angle = Math.Atan2(distance.Y , distance.X);            
            
            return angle;
        }
    }
}
