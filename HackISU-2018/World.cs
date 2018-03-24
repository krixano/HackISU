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
            AIR, DIRT, GRASS, STONE, SUNFLOWER, ROSE, WILDGRASS
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
            int worldHeight = 50;// ((Game1.screenRectangle.Height / 2) / BLOCK_SIZE) + 2;
            WORLD_SIZE = new Vector2(100, worldHeight * 2);
            offset_b = new Vector2_Double(0,28);
            blocks = new Block[(int) WORLD_SIZE.X * (int) WORLD_SIZE.Y];
            Console.WriteLine(worldHeight);

            char[,] map = new char[100,worldHeight];
            string[] lines = System.IO.File.ReadAllLines(@"testWorld.txt");
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
                    else if (map[j, i] == 'a')
                    {
                        blocks[index].type = BlockType.AIR;
                        blocks[index].solid = true;
                    }
                    
                }

            }

            //for (int y = 0; y < WORLD_SIZE.Y; y++)
            //{
            //    for (int x = 0; x < WORLD_SIZE.X; x++)
            //    {
            //        int i = x + y * (int) WORLD_SIZE.X;
            //        if (y == 3)
            //        {
            //            blocks[i].type = BlockType.STONE;
            //            blocks[i].solid = true;
            //        }
            //        else if ((y == (WORLD_SIZE.Y / 2 - 2) && (x == 9 || x == 10)))
            //        {
            //            blocks[i].type = BlockType.WILDGRASS;
            //            blocks[i].solid = false;

            //        }
            //        else if ((y == (WORLD_SIZE.Y / 2 - 2) && (x == 8 || x == 14)))
            //        {
            //            blocks[i].type = BlockType.SUNFLOWER;
            //            blocks[i].solid = false;

            //        }
            //        else if ((y == (WORLD_SIZE.Y / 2 - 2 ) && (x == 7 || x == 16)))
            //        {
            //            blocks[i].type = BlockType.ROSE;
            //            blocks[i].solid = false;

            //        }
            //        else if (y < (WORLD_SIZE.Y / 2) - 1)
            //        {
            //            if (x == 5 || x == 20)
            //            {
            //                blocks[i].type = BlockType.STONE;
            //                blocks[i].solid = true;
            //            } 
            //            else
            //            {
            //                blocks[i].type = BlockType.AIR;
            //                blocks[i].solid = false;
            //            }
            //        }
            //        else if (y == (WORLD_SIZE.Y / 2) - 1)
            //        {
            //            blocks[i].type = BlockType.GRASS;
            //            blocks[i].solid = true;
            //        }                    
            //        else {
            //            blocks[i].type = BlockType.DIRT;
            //            blocks[i].solid = true;
            //        }
            //blocks[i].size = new Vector2(1, 1);

            //    }
            //}

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

    }
}
