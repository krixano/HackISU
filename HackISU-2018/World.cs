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
        static public string fileName;
        public enum BlockType
        {
            AIR, DIRT, GRASS, STONE, SUNFLOWER, ROSE, WILDGRASS, SHALLOW_OCEAN, DEEP_OCEAN, SPIRAL
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
            fileName = "testWorld.txt";
            int worldHeight = 50;// ((Game1.screenRectangle.Height / 2) / BLOCK_SIZE) + 2;
            WORLD_SIZE = new Vector2(200, worldHeight);
            offset_b = new Vector2_Double(0,28);
            blocks = new Block[(int) WORLD_SIZE.X * (int) WORLD_SIZE.Y];
            Console.WriteLine(worldHeight);

            char[,] map = new char[100,worldHeight];
            string[] lines = System.IO.File.ReadAllLines(@fileName);
            for (int i=0; i< worldHeight; i++)
            {                
                for (int j=0; j<100; j++)
                {
                    char[] mapLine = lines[i].ToString().ToCharArray();
                    map[j, i] = mapLine[j];
                    Console.Write(map[j, i]);                    
                }
                Console.WriteLine();
            }
            for (int i = 0; i < worldHeight; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    int index = (int)(j + i * World.WORLD_SIZE.X);
                    if (map[j, i] == 's')
                    {
                        blocks[index].type = BlockType.STONE;
                        blocks[index].solid = true;
                    }
                    else if (map[j, i] == '1')
                    {
                        blocks[index].type = BlockType.ROSE;
                        blocks[index].solid = false;
                    }
                    else if (map[j, i] == '2')
                    {
                        blocks[index].type = BlockType.SUNFLOWER;
                        blocks[index].solid = false;
                    }
                    else if (map[j, i] == '3')
                    {
                        blocks[index].type = BlockType.WILDGRASS;
                        blocks[index].solid = false;
                    }
                    else if (map[j, i] == 'g')
                    {
                        blocks[index].type = BlockType.GRASS;
                        blocks[index].solid = true;
                    }
                    else if (map[j, i] == 'd')
                    {
                        blocks[index].type = BlockType.DIRT;
                        blocks[index].solid = true;
                    }
                    else if (map[j, i] == 'o')
                    {
                        blocks[index].type = BlockType.SHALLOW_OCEAN;
                        blocks[index].solid = false;
                    }
                    else if (map[j, i] == 'O')
                    {
                        blocks[index].type = BlockType.DEEP_OCEAN;
                        blocks[index].solid = false;
                    }
                    else if (map[j, i] == 'p')
                    {
                        blocks[index].type = BlockType.SPIRAL;
                        blocks[index].solid = true;
                    }
                    else if (map[j, i] == 'a')
                    {
                        blocks[index].type = BlockType.AIR;
                        blocks[index].solid = false;
                    }
                    
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
                    Color color = Color.White;
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
                        case BlockType.SHALLOW_OCEAN:
                            texture = Game1.shallowOceanTexture;
                            color = new Color(255, 255, 255, 50);
                            break;
                        case BlockType.DEEP_OCEAN:
                            texture = Game1.deepOceanTexture;
                            color = new Color(255, 255, 255, 50);
                            break;                        
                        case BlockType.STONE:
                            texture = Game1.stoneTexture;
                            break;
                        case BlockType.SPIRAL:
                            texture = Game1.spiralPlatformTexture;
                            break;
                        case BlockType.ROSE:
                            texture = Game1.roseTexture;
                            break;
                        case BlockType.SUNFLOWER:
                            texture = Game1.sunflowerTexture;
                            break;
                        case BlockType.WILDGRASS:
                            texture = Game1.wildgrassTexture;
                            break;
                        default:
                            texture = null;
                            break;
                    }
                    if (texture != null)
                    {
                        spriteBatch.Draw(texture, destination, color);
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
