using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace FFOD
{
    public class Camera
    {
        public Game1 Game;
        public Player Player;
        
        private OrthographicCamera _camera;
        private TiledMap _map;

        public Camera(Game1 game, Player player, TiledMap map)
        {
            Game = game;
            Player = player;
            _map = map;
            
            // Setting viewport and camera
            var gd = game.GraphicsDevice;
            var window = game.Window;
            var vp = new BoxingViewportAdapter(window, gd, game.Width, game.Height);
            _camera = new OrthographicCamera(vp);
        }

        private void Follow()
        {
            Vector2 camMin = new Vector2(Game.Width, Game.Height);
            camMin *= _camera.Zoom / 2;

            Vector2 camMax = new Vector2(_map.WidthInPixels, _map.HeightInPixels);
            camMax -= camMin;

            float camX = Math.Min(camMax.X, Math.Max(Player.X, camMin.X));
            float camY = Math.Min(camMax.Y, Math.Max(Player.Y, camMin.Y));
            var camPos = new Vector2(camX, camY);
            
            _camera.LookAt(camPos);
        }
        
        public void Update(GameTime gameTime)
        {
            Follow();
        }

        public Matrix GetViewMatrix() => _camera.GetViewMatrix();
    }
}
