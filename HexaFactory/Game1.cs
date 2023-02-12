using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HexaFactory
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Texture2D hexagonTile;
        Vector2 ballPosition;
        float movementSpeed;
        SpriteFont openDyslexic;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Vector2 playerCords = new Vector2(0, 0);

        int zoom = 64;

        int Render_Distance_X = 10;
        int Render_Distance_Y = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here

            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            movementSpeed = 1f;
            Debug.WriteLine("Startin");
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowSizeChanged;

            base.Initialize();
        }

        protected void WindowSizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Window has been resized!");
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height;
            decimal temp = GraphicsDevice.Viewport.Width / zoom;
            int Render_Distance_X = (int)(Math.Ceiling(temp) + 2);
            temp = GraphicsDevice.Viewport.Height / zoom;
            int Render_Distance_Y = (int)(Math.Ceiling(temp) + 2);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
            hexagonTile = Content.Load<Texture2D>("Hexagon_Tile");
            openDyslexic = Content.Load<SpriteFont>("opendyslexic");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                playerCords.Y -= movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                playerCords.Y += movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                playerCords.X -= movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                playerCords.X += movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        protected bool cordCorrection(float coords, int y)
        {
            return ((int)coords + y) % 2 == 0;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkViolet);

            Vector2 ChunkPosition = playerCords;

            _spriteBatch.Begin();

            float offsetRending = 0f;

            for (int y = 0; y < 6; y++)
            {
                if (playerCords.Y + y < 0)
                {
                    offsetRending = 0f;
                }
                else
                {
                    offsetRending = 0f;
                }
                for (int x = -1; x < 1; x++)
                {
                    ChunkPosition.Y = -playerCords.Y * zoom % zoom + y * zoom;
                    Debug.WriteLine(playerCords.Y + y + offsetRending);
                    if ((int)(playerCords.Y + y + offsetRending) % 2 == 0)
                    {
                        ChunkPosition.X = -playerCords.X * zoom % zoom + x * zoom;
                    }
                    else
                    {
                        ChunkPosition.X = -playerCords.X * zoom % zoom + x * zoom + zoom / 2;
                    }
                    _spriteBatch.Draw(
                    hexagonTile,
                    ChunkPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(hexagonTile.Width / 2, hexagonTile.Height / 2),
                    new Vector2(zoom / 512f, zoom / 512f),
                    SpriteEffects.None,
                    0f
                    );
                }
            }
            _spriteBatch.DrawString(openDyslexic, "LOL", new Vector2(25, 25), Color.Cyan);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}