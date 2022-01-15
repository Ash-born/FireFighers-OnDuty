using System;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;


namespace FireFighers_OnDuty
{
    public class MainGame : GameScreen
    {
        private new Game1 Game => (Game1) base.Game;
        private OrthographicCamera _camera;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _cameraPosition;
        public MainGame(Game1 game) : base(game) { }
        public override void LoadContent()
        {
            base.LoadContent();
            _tiledMap = Content.Load<TiledMap>("GFX/Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            var viewportadapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(viewportadapter.VirtualWidth /2 , viewportadapter.VirtualHeight / 2);
        }
        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
                
            MoveCamera(gameTime);
            _camera.LookAt(_cameraPosition);


        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Gray);

         //   _tiledMapRenderer.Draw(_camera.GetInverseViewMatrix());
            
            _tiledMapRenderer.Draw( _camera.GetViewMatrix());

        }
        private void MoveCamera(GameTime gameTime)
        {
            var speed = 200;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = GetMovementDirection();
            
            _cameraPosition += speed * movementDirection * seconds;
            if (_cameraPosition.X >= _tiledMap.WidthInPixels -GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width /2)
            {
                _cameraPosition = new Vector2(_tiledMap.WidthInPixels - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2 , _cameraPosition.Y);

            }
            if (_cameraPosition.Y >= _tiledMap.HeightInPixels  - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2 )
            {
                _cameraPosition = new Vector2(_cameraPosition.X,_tiledMap.HeightInPixels - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2 );

            }

            if (_cameraPosition.Y <  GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2)
            {
                _cameraPosition = new Vector2(_cameraPosition.X, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2 );

            }
            if (_cameraPosition.X <  GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2)
            {
                _cameraPosition = new Vector2( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2 , _cameraPosition.Y );

            }
        }
        
        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }
    
            // Can't normalize the zero vector so test for it before normalizing
            if (movementDirection != Vector2.Zero)
            {
                movementDirection.Normalize(); 
            }
    
            return movementDirection;
        }


    }
}