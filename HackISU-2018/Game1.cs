using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    
    public class Game1 : Game
    {
        public static GamePadState pad1, prevPad1;
        public static MouseState mouse, prevMouse;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static KeyboardState keyboard, prevKeyboard;

        public static Rectangle screenRectangle;
        public static Texture2D testTexture;
        public static Texture2D dirtTexture;
        public static Texture2D grassTexture;
        public static Texture2D stoneTexture;
        public static Texture2D gunArmTexture;
        public static Texture2D bulletTexture;
        public static Texture2D roseTexture;
        public static Texture2D sunflowerTexture;
        public static Texture2D wildgrassTexture;
        public static Texture2D deepOceanTexture;
        public static Texture2D shallowOceanTexture;
        public static Texture2D skyTexture;
        public static Texture2D spiralPlatformTexture;
        public static Texture2D shotgunShell;



        public enum GameStates
        {
            MAIN_MENU, PAUSED, PLAYING
        }

        static public GameStates gameState = GameStates.PLAYING;

        public struct SpriteStruct
        {
            public Vector2_Double position_wp;
            public Vector2 size;
            public Color color;
            public float rotation;
            public Vector2 origin, speed;
            public float scale;
            public SpriteEffects effect;
            public float layerDepth;
            public bool isFired;
        }

        public struct Menu
        {
            public Rectangle[] bottons;
            public Color color;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            screenRectangle = new Rectangle(0, 0, 1280, 720);
            graphics.PreferredBackBufferWidth = screenRectangle.Width;
            graphics.PreferredBackBufferHeight = screenRectangle.Height;
            graphics.ApplyChanges();

            World.Init();
            player.playerInit();
            gun.gunInit();
            enemy.enemyInit();

            UserInterface.InitializeMenus();

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testTexture = Content.Load<Texture2D>("WhiteSquare100x100");
            dirtTexture = Content.Load<Texture2D>("Dirt_Block_Texture_64x64");
            grassTexture = Content.Load<Texture2D>("Grass_Block_Texture_64x64");
            stoneTexture = Content.Load<Texture2D>("Stone_Block_Texture_64x64");
            roseTexture = Content.Load<Texture2D>("Rose_Block_Texture_64x64");
            sunflowerTexture = Content.Load<Texture2D>("Sunflower_Block_Texture_64x64");
            wildgrassTexture = Content.Load<Texture2D>("Wildgrass_Block_Texture_64x64");
            deepOceanTexture = Content.Load<Texture2D>("Deep_Ocean_Block_Texture_64x64");
            shallowOceanTexture = Content.Load<Texture2D>("Shallow_Ocean_Block_Texture_64x64");
            skyTexture = Content.Load<Texture2D>("Sky_Block_Texture_64x64");
            spiralPlatformTexture = Content.Load<Texture2D>("Spiral_Platform_Texture_256x40");
            shotgunShell = Content.Load<Texture2D>("Shotgun_Shell_Texture_36x64");
            gunArmTexture = testTexture;
            bulletTexture = Content.Load<Texture2D>("Shotgun_Pellet_Texture_32x32");
            gun.gunArm.origin.X = testTexture.Width / 2;
            gun.gunArm.origin.Y = testTexture.Height / 2;
            for (int i = 0; i < gun.bullet.Length; i++)
            {
                gun.bullet[i].origin.Y = bulletTexture.Height / 2 ;                
                gun.bullet[i].origin.X = bulletTexture.Width / 2;
            }
        }

        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {         
            pad1 = GamePad.GetState(PlayerIndex.One);

            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();
            player.playerUpdate();
            gun.gunUpdate();
            enemy.enemyUpdate();
            //prevMouse = mouse;
            prevPad1 = pad1;
            prevKeyboard = keyboard;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            {
                enemy.Draw(spriteBatch);

                World.Draw(spriteBatch);

                player.Draw(spriteBatch);
                

                //spriteBatch.Draw(testTexture, new Rectangle((int) (player.sprite.position_wp.X - (World.offset_b.X * World.BLOCK_SIZE)), (int) (player.sprite.position_wp.Y - (World.offset_b.Y * World.BLOCK_SIZE)), (int) player.sprite.size.X, (int) player.sprite.size.Y), Color.White);
                for (int i = 0; i < gun.bullet.Length; i++)
                {
                    if (gun.bullet[i].isFired && gun.gunSelection == gun.GunSelections.SHOTGUN)
                        spriteBatch.Draw(shotgunShell, new Rectangle((int)(gun.shell.position_wp.X - (World.offset_b.X * World.BLOCK_SIZE)), (int)(gun.shell.position_wp.Y - (World.offset_b.Y * World.BLOCK_SIZE)), (int)gun.shell.size.X, (int)gun.shell.size.Y), Color.White);
                    if (gun.bullet[i].isFired)
                        spriteBatch.Draw(bulletTexture, new Rectangle((int)(gun.bullet[i].position_wp.X - (World.offset_b.X * World.BLOCK_SIZE)), (int)(gun.bullet[i].position_wp.Y - (World.offset_b.Y * World.BLOCK_SIZE)), (int)gun.bullet[i].size.X, (int)gun.bullet[i].size.Y), null, Color.White, gun.bullet[i].rotation, gun.bullet[i].origin, SpriteEffects.None, 0);
                }
                    spriteBatch.Draw(gunArmTexture, new Rectangle((int)(gun.gunArm.position_wp.X - (World.offset_b.X * World.BLOCK_SIZE)), (int)(gun.gunArm.position_wp.Y - (World.offset_b.Y * World.BLOCK_SIZE)), (int)gun.gunArm.size.X, (int)gun.gunArm.size.Y), null, Color.Red, gun.gunArm.rotation, gun.gunArm.origin, SpriteEffects.None,0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
