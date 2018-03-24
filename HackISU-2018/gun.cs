using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackISU_2018
{
    class gun
    {
        static public Game1.SpriteStruct gunArm;
        static public void gunInit()
        {
            gunArm.size.X = player.sprite.size.X;
            gunArm.size.Y = gunArm.size.X / 3;
            gunArm.position.X = player.sprite.position.X + gunArm.size.X / 2;
            gunArm.position.Y = player.sprite.position.Y + gunArm.size.Y / 2;
        }
        public static void gunUpdate()
        {
            gunArm.position.X = player.sprite.position.X + gunArm.size.X;
            gunArm.position.Y = player.sprite.position.Y + gunArm.size.Y;
        }
    }
}
