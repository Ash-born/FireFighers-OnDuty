using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace FFOD
{
    public class Player
    {
        private Rectangle _rectangle;
        private TextureRegion2D _texture;
        private Vector2 _velocity;
        private int _speed;
        
        public Game1 Game;
        public SpriteBatch Batch => Game.SpriteBatch;

        public int X
        {
            get => _rectangle.X;
            set => _rectangle.X = value;
        }

        public int Y
        {
            get => _rectangle.Y;
            set => _rectangle.Y = value;
        }

        public int Width
        {
            get => _rectangle.Width;
            set => _rectangle.Width = value;
        }

        public int Height
        {
            get => _rectangle.Height;
            set => _rectangle.Height = value;
        }

        public Player(Game1 game, int x, int y, int speed)
        {
            Game = game;
            X = x;
            Y = y;
            _speed = speed;
            int regionWidth = 32;
            int regionHeight = 32;
            Width = regionWidth;
            Height = regionHeight;
            var spriteSheet = Game.Content.Load<Texture2D>("GFX/Sprite/sprite");
            TextureAtlas atlas = TextureAtlas.Create("player", spriteSheet, regionWidth, regionHeight);
            _texture = atlas.GetRegion(0);
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        public void Move(GameTime gameTime)
        {
            _velocity = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                _velocity += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                _velocity -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                _velocity -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                _velocity += Vector2.UnitX;
            }
            
            _velocity *= _speed * gameTime.GetElapsedSeconds();
            X += (int) _velocity.X;
            Y += (int) _velocity.Y;
            Debug.WriteLine(_velocity);
        }

        public void Draw()
        {
            Batch.Draw(_texture, _rectangle, Color.White);
        }
    }
}
