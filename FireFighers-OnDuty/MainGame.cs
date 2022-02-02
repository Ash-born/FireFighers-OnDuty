using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;


namespace FFOD
{
    public class MainGame : GameScreen
    {
        private new Game1 Game => (Game1) base.Game;
        private Camera _camera;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        private GraphicsDeviceManager _graphics;
        
        private Player _player;

        public MainGame(Game1 game) : base(game)
        {
            
        }

        public override void LoadContent()
        {
            base.LoadContent();
            // Load map
            _tiledMap = Content.Load<TiledMap>("GFX/Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            // Set player
            _player = new Player(Game, 10, 10, 200);
            // Set camera
            _camera = new Camera(Game, _player, _tiledMap);
        }

        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            _camera.Update(gameTime);
            _player.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Gray);
            Game.SpriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _player.Draw();
            
            Game.SpriteBatch.End();
        }
    }
}
