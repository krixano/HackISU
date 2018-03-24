﻿using Microsoft.Xna.Framework;
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
        public static KeyboardState keyboard;

        public static Rectangle screenRectangle;
        public static Texture2D testTexture;
        public static Texture2D dirtTexture;
        public static Texture2D grassTexture;
        public static Texture2D stoneTexture;
        public static Texture2D gunArmTexture;
        public static Texture2D bulletTexture;

        

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
            public bool isFired;
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
            gunArmTexture = testTexture;
            bulletTexture = testTexture;
            gun.gunArm.origin.Y = gunArmTexture.Height / 2;
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
            prevMouse = mouse;
            prevPad1 = pad1;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            {
                World.Draw(spriteBatch);
                spriteBatch.Draw(testTexture, new Rectangle((int) (player.sprite.position.X - (World.offset.X * World.BLOCK_SIZE)), (int) (player.sprite.position.Y - (World.offset.Y * World.BLOCK_SIZE)), (int) player.sprite.size.X, (int) player.sprite.size.Y), Color.White);
                spriteBatch.Draw(gunArmTexture, new Rectangle((int)(gun.gunArm.position.X - (World.offset.X * World.BLOCK_SIZE)), (int)(gun.gunArm.position.Y - (World.offset.Y * World.BLOCK_SIZE)), (int)gun.gunArm.size.X, (int)gun.gunArm.size.Y), null, Color.Red, gun.gunArm.rotation, gun.gunArm.origin, SpriteEffects.None,0);
                for (int i = 0; i < gun.bullet.Length; i++)
                    if (gun.bullet[i].isFired)
                        spriteBatch.Draw(bulletTexture, new Rectangle((int)(gun.bullet[i].position.X - (World.offset.X * World.BLOCK_SIZE)), (int)(gun.bullet[i].position.Y - (World.offset.Y * World.BLOCK_SIZE)), (int)gun.bullet[i].size.X, (int)gun.bullet[i].size.Y), null, Color.White, gun.bullet[i].rotation, gun.bullet[i].origin, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
