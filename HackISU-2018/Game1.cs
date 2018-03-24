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
        public static Rectangle screenRectangle, playingAreaRectangle;
        public static Texture2D testTexture;

        enum GameStates
        {
            
        }        

        public struct SpriteStruct
        {
            public Vector2 position;
            public Rectangle rectangle;
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
            // TODO: Add your initialization logic here
            Console.WriteLine("Test!");
            screenRectangle = new Rectangle(0, 0, 1280, 720);
            playingAreaRectangle = new Rectangle(0, 0, 1280, 720);
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testTexture = Content.Load<Texture2D>("WhiteSquare100x100");

            // TODO: use this.Content to load your game content here
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {         
            pad1 = GamePad.GetState(PlayerIndex.One);

            
            //Put update code here

            prevPad1 = pad1;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            {

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
