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
        public struct SpriteStruct
        {
            public Texture2D texture;
            public Vector2 position;
            public Rectangle rectangle;
            public Color color;
            public float rotation;
            public Vector2 origin, speed;
            public float scale;
            public SpriteEffects effect;
            public float layerDepth;

        }

        public void Player()
        {
            SpriteStruct player;
            player.rectangle.Width = screenRectangle.Width / 20;
            player.rectangle.Height = screenRectangle.Height / 20;
        }
        
    }
}
