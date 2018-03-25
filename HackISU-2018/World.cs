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
            AIR, DIRT, GRASS, STONE, SUNFLOWER, ROSE, WILDGRASS, SHALLOW_OCEAN, DEEP_OCEAN, PLATFORM, CAVE, DARK_CAVE, CAVE_ENTRANCE, TOWER, SNOW, COBBLE, COBBLE_LEFT, COBBLE_RIGHT, SHORT_GRASS
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
        public static int BLOCK_SIZE = 45;

        public static void Init(string file)
        {
            fileName = file;
            int worldHeight = 50;// ((Game1.screenRectangle.Height / 2) / BLOCK_SIZE) + 2;
            WORLD_SIZE = new Vector2(200, worldHeight);
            offset_b = new Vector2_Double(0,28);
            blocks = new Block[(int) WORLD_SIZE.X * (int) WORLD_SIZE.Y];
            Console.WriteLine(worldHeight);

            Game1.gameState = Game1.GameStates.MAIN_MENU;
            if (Game1.gameState == Game1.GameStates.MAIN_MENU)
            {
                //BLOCK_SIZE = 15;
            }
            

            char[] map = new char[(int) (World.WORLD_SIZE.Y * World.WORLD_SIZE.X)];
            string[] lines = System.IO.File.ReadAllLines(@fileName);
            Console.WriteLine(lines.Length);
            Console.WriteLine(lines);
            for (int y = 0; y < World.WORLD_SIZE.Y; y++)
            {
                if (y >= lines.Length)
                {
                    Console.WriteLine("Error!"); break;
                }
                char[] mapLine = lines[y].ToString().ToCharArray();
                Console.WriteLine(mapLine.Length);
                for (int x = 0; x < 200; x++) // Width
                {
                    if (x >= mapLine.Length)
                    {
                        Console.WriteLine("Error!"); break;
                    }
                    map[x + y * 200] = mapLine[x];
                }
            }
            Console.WriteLine();
            for (int y = 0; y < World.WORLD_SIZE.Y; y++)
            {
                for (int x = 0; x < World.WORLD_SIZE.X; x++)
                {
                    int index = (int)(x + y * World.WORLD_SIZE.X);
                    if (map[index] == 's')
                    {
                        blocks[index].type = BlockType.STONE;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == '1')
                    {
                        blocks[index].type = BlockType.ROSE;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == '2')
                    {
                        blocks[index].type = BlockType.SUNFLOWER;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == '3')
                    {
                        blocks[index].type = BlockType.WILDGRASS;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'g')
                    {
                        blocks[index].type = BlockType.GRASS;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == 'd')
                    {
                        blocks[index].type = BlockType.DIRT;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == 'o')
                    {
                        blocks[index].type = BlockType.SHALLOW_OCEAN;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'O')
                    {
                        blocks[index].type = BlockType.DEEP_OCEAN;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'p')
                    {
                        blocks[index].type = BlockType.PLATFORM;
                        blocks[index].solid = true;

                    }
                    else if (map[index] == 'c')
                    {
                        blocks[index].type = BlockType.CAVE;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'C')
                    {
                        blocks[index].type = BlockType.DARK_CAVE;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == 'E')
                    {
                        blocks[index].type = BlockType.CAVE_ENTRANCE;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'i')
                    {
                        blocks[index].type = BlockType.TOWER;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'w')
                    {
                        blocks[index].type = BlockType.SNOW;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == 'S')
                    {
                        blocks[index].type = BlockType.COBBLE;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == 'L')
                    {
                        blocks[index].type = BlockType.COBBLE_LEFT;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == 'R')
                    {
                        blocks[index].type = BlockType.COBBLE_RIGHT;
                        blocks[index].solid = true;
                    }
                    else if (map[index] == '4')
                    {
                        blocks[index].type = BlockType.SHORT_GRASS;
                        blocks[index].solid = false;
                    }
                    else if (map[index] == 'a')
                    {
                        blocks[index].type = BlockType.AIR;
                        blocks[index].solid = false;
                    }
                    
                }

            }

        }

        public static void Draw(SpriteBatch spriteBatch)
        {


            int round = 0;
            
            for (int y = 0; y < WORLD_SIZE.Y; y++)
            {
                for (int x = 0; x < WORLD_SIZE.X; x++)
                {
                    if (Game1.gameState == Game1.GameStates.MAIN_MENU)
                    {
                        int index = (int)(x + y * World.WORLD_SIZE.X);
                        int xpixels = (int)(x * World.BLOCK_SIZE - World.offset_b.X * World.BLOCK_SIZE);
                        if (xpixels <= Game1.screenRectangle.Left)
                        {
                            if (x == 0) round++;
                            //xpixels = (int)((World.WORLD_SIZE.X - World.offset_b.X) + World.blocks[(int)(x + y * World.WORLD_SIZE.X)].size.X * x - World.offset_b.X);
                            xpixels = (int)(x * World.BLOCK_SIZE - World.offset_b.X * World.BLOCK_SIZE + ((World.WORLD_SIZE.X - 50) * World.BLOCK_SIZE * round));
                        }
                        Rectangle destination = new Rectangle((xpixels), (int)((y * BLOCK_SIZE) - (offset_b.Y * BLOCK_SIZE)), (int)BLOCK_SIZE, (int)BLOCK_SIZE);
                        Texture2D texture;
                        Color color = Color.White;
                        switch (blocks[x + y * (int)WORLD_SIZE.X].type)
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
                            case BlockType.PLATFORM:
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
                            case BlockType.CAVE:
                                texture = Game1.caveTexture;
                                break;
                            case BlockType.DARK_CAVE:
                                texture = Game1.darkCaveTexture;
                                break;
                            case BlockType.CAVE_ENTRANCE:
                                texture = Game1.caveEntranceTexture;
                                break;
                            case BlockType.TOWER:
                                texture = Game1.spiralTexture;
                                break;
                            case BlockType.SNOW:
                                texture = Game1.snowTexture;
                                break;
                            case BlockType.COBBLE:
                                texture = Game1.cobbleTexture;
                                break;
                            case BlockType.COBBLE_LEFT:
                                texture = Game1.cobbleLeftTexture;
                                break;
                            case BlockType.COBBLE_RIGHT:
                                texture = Game1.cobbleRightTexture;
                                break;
                            case BlockType.SHORT_GRASS:
                                texture = Game1.shortGrassTexture;
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
                    else
                    {
                        Rectangle destination = new Rectangle((int)((x * BLOCK_SIZE) - (offset_b.X * BLOCK_SIZE)), (int)((y * BLOCK_SIZE) - (offset_b.Y * BLOCK_SIZE)), (int)BLOCK_SIZE, (int)BLOCK_SIZE);
                        Texture2D texture;
                        Color color = Color.White;
                        switch (blocks[x + y * (int)WORLD_SIZE.X].type)
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
                            case BlockType.PLATFORM:
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
                            case BlockType.CAVE:
                                texture = Game1.caveTexture;
                                break;
                            case BlockType.DARK_CAVE:
                                texture = Game1.darkCaveTexture;
                                break;
                            case BlockType.CAVE_ENTRANCE:
                                texture = Game1.caveEntranceTexture;
                                break;
                            case BlockType.TOWER:
                                texture = Game1.spiralTexture;
                                break;
                            case BlockType.SNOW:
                                texture = Game1.snowTexture;
                                break;
                            case BlockType.COBBLE:
                                texture = Game1.cobbleTexture;
                                break;
                            case BlockType.COBBLE_LEFT:
                                texture = Game1.cobbleLeftTexture;
                                break;
                            case BlockType.COBBLE_RIGHT:
                                texture = Game1.cobbleRightTexture;
                                break;
                            case BlockType.SHORT_GRASS:
                                texture = Game1.shortGrassTexture;
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
