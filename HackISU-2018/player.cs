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
    class player
    {
        static private int playerXSpeed, playerYSpeed;
        static public Game1.SpriteStruct sprite;

        static public void Init()
        {
            playerXSpeed = Game1.playingAreaRectangle.Width / 20;
            playerYSpeed = Game1.playingAreaRectangle.Height / 20;

            
            sprite.rectangle.Width = Game1.playingAreaRectangle.Width / 20;
            sprite.rectangle.Height = Game1.playingAreaRectangle.Height / 20;
            sprite.position.X = Game1.playingAreaRectangle.Width / 2 - sprite.rectangle.Width;
            sprite.position.Y = Game1.playingAreaRectangle.Bottom - sprite.rectangle.Height;


            if (Game1.pad1.IsButtonDown(Buttons.DPadLeft))
                sprite.position.X += playerXSpeed;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight))
                sprite.position.X -= playerXSpeed;
            //if (Game1.pad1.IsButtonDown(Buttons.DPadUp))
            //    player.position.Y += playerYSpeed;
            //if (Game1.pad1.IsButtonDown(Buttons.DPadDown))
            //    player.position.Y -= playerYSpeed;
                        
        }
        static public void Update()
        {

        }
        
    }
}
