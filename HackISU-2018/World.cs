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
            AIR, DIRT, GRASS, STONE
        };

        public struct Block
        {
            public BlockType type;
            public Vector2 size; // In Blocks - which means size.x and size.y times blocksize.
            public bool solid;
        };

        public static Vector2_Double offset_b; // _b: In Blocks
        public static Vector2 WORLD_SIZE;
        public static Block[] blocks;
        public const int BLOCK_SIZE = 45;

        public static void Init()
        {
            int worldHeight = ((Game1.screenRectangle.Height / 2) / BLOCK_SIZE) + 2;
            WORLD_SIZE = new Vector2(200, worldHeight * 2);
            offset_b = new Vector2_Double(0, 0);
            blocks = new Block[(int) WORLD_SIZE.X * (int) WORLD_SIZE.Y];

            for (int y = 0; y < WORLD_SIZE.Y; y++)
            {
                for (int x = 0; x < WORLD_SIZE.X; x++)
                {
                    int i = x + y * (int) WORLD_SIZE.X;
                    if (y == 3)
                    {
                        blocks[i].type = BlockType.STONE;
                        blocks[i].solid = true;
                    }
                    else if (y < (WORLD_SIZE.Y / 2) - 1)
                    {
                        if (x == 5 || x == 20)
                        {
                            blocks[i].type = BlockType.STONE;
                            blocks[i].solid = true;
                        } else
                        {
                            blocks[i].type = BlockType.AIR;
                            blocks[i].solid = false;
                        }
                    }
                    else if (y == (WORLD_SIZE.Y / 2) - 1)
                    {
                        blocks[i].type = BlockType.GRASS;
                        blocks[i].solid = true;
                    }
                    else {
                        blocks[i].type = BlockType.DIRT;
                        blocks[i].solid = true;
                    }
                    blocks[i].size = new Vector2(1, 1);
                }
            }

        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            for (int y = 0; y < WORLD_SIZE.Y; y++)
            {
                for (int x = 0; x < WORLD_SIZE.X; x++)
                {
                    Rectangle destination = new Rectangle((int) ((x * BLOCK_SIZE) - (offset_b.X * BLOCK_SIZE)), (int) ((y * BLOCK_SIZE) - (offset_b.Y * BLOCK_SIZE)), (int) BLOCK_SIZE, (int) BLOCK_SIZE);
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
                        case BlockType.STONE:
                            texture = Game1.stoneTexture;
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

        public static Vector2_Double worldPixelsToScreenPixelsPosition(Vector2_Double position)
        {
            return new Vector2_Double(position.X - (World.offset_b.X * World.BLOCK_SIZE), position.Y - (World.offset_b.Y * World.BLOCK_SIZE));
        }

        // Note: Only works for sprites of same size as BLOCK_SIZE
        public static Vector2_Double worldBlocksToScreenPixelsPosition(Vector2_Double position)
        {
            return new Vector2_Double((position.X * BLOCK_SIZE) - (offset_b.X * World.BLOCK_SIZE), (position.Y * BLOCK_SIZE) - (offset_b.Y * World.BLOCK_SIZE));
        }

        public static Point worldOffsetPixels()
        {
            return new Point((int) (offset_b.X * World.BLOCK_SIZE), (int) (offset_b.Y * World.BLOCK_SIZE));
        }

    }
}
