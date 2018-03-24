using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackISU_2018
{
    class World
    {

        public enum BlockType
        {
            DIRT, GRASS
        };

        public struct Blocks
        {
            BlockType type;
            Vector2 size; // In Blocks - which means size.x and size.y times blocksize.
        };

        public Texture2D simpleBlock;
        public Vector2 offset; // In Blocks
        public Vector2 WORLD_SIZE;
        public Blocks[] blocks;
        public const int BLOCK_SIZE = 16;

        public World()
        {
            WORLD_SIZE = new Vector2(200, 200);
            offset = new Vector2(0, 0);
            blocks = new Blocks[(int) WORLD_SIZE.X * (int) WORLD_SIZE.Y];

        }

        public void Load(ContentManager Content)
        {
            simpleBlock = Content.Load<Texture2D>("WhiteSquare100x100");
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int y = 0; y < WORLD_SIZE.X; y++)
            {
                for (int x = 0; x < WORLD_SIZE.Y; x++)
                {
                    //Console.WriteLine("Test");
                    Rectangle destination = new Rectangle((int) ((x * BLOCK_SIZE) - (offset.X * BLOCK_SIZE)), (int) ((y * BLOCK_SIZE) - (offset.Y * BLOCK_SIZE)), (int) BLOCK_SIZE, (int) BLOCK_SIZE);
                    spriteBatch.Draw(simpleBlock, destination, Color.Red);
                }
            }

        }

    }
}
