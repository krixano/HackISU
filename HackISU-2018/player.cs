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
        private int playerXSpeed, playerYSpeed;

        public void Player()
        {
            playerXSpeed = Game1.playingAreaRectangle.Width / 20;
            playerYSpeed = Game1.playingAreaRectangle.Height / 20;

            Game1.SpriteStruct player;
            player.rectangle.Width = Game1.playingAreaRectangle.Width / 20;
            player.rectangle.Height = Game1.playingAreaRectangle.Height / 20;
            player.position.X = Game1.playingAreaRectangle.Width / 2 - player.rectangle.Width;
            player.position.Y = Game1.playingAreaRectangle.Bottom - player.rectangle.Height;

            if (Game1.pad1.IsButtonDown(Buttons.DPadLeft))
                player.position.X += playerXSpeed;
            if (Game1.pad1.IsButtonDown(Buttons.DPadRight))
                player.position.X -= playerXSpeed;
            //if (Game1.pad1.IsButtonDown(Buttons.DPadUp))
            //    player.position.Y += playerYSpeed;
            //if (Game1.pad1.IsButtonDown(Buttons.DPadDown))
            //    player.position.Y -= playerYSpeed;
                        
        }
        
    }
}
