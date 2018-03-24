using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HackISU_2018
{
    
    public class Game1 : Game
    {
        public static GamePadState pad1, prevPad1;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboard;

        World world;
        public static Rectangle screenRectangle;
        public static Texture2D testTexture;
        public static Texture2D dirtTexture;

        enum GameStates
        {
            MAIN_MENU, PAUSED, PLAYING
        }

        public struct SpriteStruct
        {
            public Vector2 position;
            public Vector2 size;
            public Color color;
            public float rotation;
            public Vector2 origin, speed;
            public float scale;
            public SpriteEffects effect;
            public float layerDepth;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            world = new World();
            screenRectangle = new Rectangle(0, 0, 1280, 720);
            graphics.PreferredBackBufferWidth = screenRectangle.Width;
            graphics.PreferredBackBufferHeight = screenRectangle.Height;
            graphics.ApplyChanges();
            //playingAreaRectangle = new Rectangle(0, 0, 1280, 720);
            player.Init();

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testTexture = Content.Load<Texture2D>("WhiteSquare100x100");
            dirtTexture = Content.Load<Texture2D>("Dirt_Block_Texture_64x64");

            world.Load(Content);
            
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {         
            pad1 = GamePad.GetState(PlayerIndex.One);

            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right)) world.offset.X += .25f;
            if (keyboard.IsKeyDown(Keys.Left)) world.offset.X -= .25f;
            if (keyboard.IsKeyDown(Keys.Up)) world.offset.Y -= .25f;
            if (keyboard.IsKeyDown(Keys.Down)) world.offset.Y += .25f;
            
            //Put update code here

            prevPad1 = pad1;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            {
                world.Draw(spriteBatch);
                spriteBatch.Draw(testTexture, new Rectangle((int) player.sprite.position.X, (int) player.sprite.position.Y, (int) player.sprite.size.X, (int) player.sprite.position.Y), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
