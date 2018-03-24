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
            AIR, DIRT, GRASS
        };

        public struct Block
        {
            public BlockType type;
            public Vector2 size; // In Blocks - which means size.x and size.y times blocksize.
        };

        public Texture2D simpleBlock;
        public Vector2 offset; // In Blocks
        public Vector2 WORLD_SIZE;
        public Block[] blocks;
        public const int BLOCK_SIZE = 64;

        public World()
        {
            int worldHeight = ((Game1.screenRectangle.Height / 2) / BLOCK_SIZE) + 2;
            WORLD_SIZE = new Vector2(200, worldHeight * 2);
            offset = new Vector2(0, 0);
            blocks = new Block[(int) WORLD_SIZE.X * (int) WORLD_SIZE.Y];

            for (int y = 0; y < WORLD_SIZE.Y; y++)
            {
                for (int x = 0; x < WORLD_SIZE.X; x++)
                {
                    if (y < (WORLD_SIZE.Y / 2) - 1)
                        blocks[x + y * (int) WORLD_SIZE.X].type = BlockType.AIR;
                    else if (y == (WORLD_SIZE.Y / 2) - 1)
                        blocks[x + y * (int) WORLD_SIZE.X].type = BlockType.GRASS;
                    else blocks[x + y * (int) WORLD_SIZE.X].type = BlockType.DIRT;
                    blocks[x + y * (int) WORLD_SIZE.X].size = new Vector2(1, 1);
                }
            }

        }

        public void Load(ContentManager Content)
        {
            simpleBlock = Content.Load<Texture2D>("WhiteSquare100x100");
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int y = 0; y < WORLD_SIZE.Y; y++)
            {
                for (int x = 0; x < WORLD_SIZE.X; x++)
                {
                    Rectangle destination = new Rectangle((int) ((x * BLOCK_SIZE) - (offset.X * BLOCK_SIZE)), (int) ((y * BLOCK_SIZE) - (offset.Y * BLOCK_SIZE)), (int) BLOCK_SIZE, (int) BLOCK_SIZE);
                    Texture2D texture;
                    switch (blocks[x + y * (int) WORLD_SIZE.X].type)
                    {
                        case BlockType.AIR:
                            texture = null;
                            break;
                        case BlockType.DIRT:
                            texture = Game1.dirtTexture;
                            break;
                        case BlockType.GRASS:
                            texture = Game1.grassTexture;
                            break;
                        default:
                            texture = null;
                            break;
                    }
                    if (texture != null)
                    {
                        spriteBatch.Draw(texture, destination, Color.White);
                    }
                }
            }

        }

    }
}
