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
            gunArm.position.X = player.sprite.position.X + (player.sprite.size.X / 2);
            gunArm.position.Y = player.sprite.position.Y + (player.sprite.size.Y / 2);
            //gunArm.origin.X += gunArm.size.Y;
            //gunArm.origin.Y += gunArm.size.Y / 2;
        }
        public static void gunUpdate()
        {
            gunArm.position.X = player.sprite.position.X + gunArm.size.X / 2;
            gunArm.position.Y = player.sprite.position.Y + gunArm.size.Y;
            gunArm.rotation = (float) getMouseAngle();
        }
        public static double getMouseAngle()
        {
            Vector2 distance;
            double angle;

            
            distance.X = Math.Abs(Game1.mouse.X - (gunArm.position.X - World.offset.X * World.BLOCK_SIZE));
            distance.Y = Game1.mouse.Y - (gunArm.position.Y - World.offset.Y * World.BLOCK_SIZE);

            if (Game1.mouse.X < gunArm.position.X)
            {
                angle = 3.14159 - Math.Atan(distance.Y / distance.X);
            } else
            {
                angle = Math.Atan(distance.Y / distance.X);
            }
            
            return angle;
        }
    }
}
