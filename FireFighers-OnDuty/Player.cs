using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace FireFighers_OnDuty
{
    public class Player
    {
        public Vector2 pos;
        public OrthographicCamera _camera;
        public Vector2 _cameraPosition; 

        public Player( Vector2 pos,OrthographicCamera Camera,Vector2 cameraPosition)
        {
            this.pos = pos;
            this._camera = Camera;
            this._cameraPosition = cameraPosition;
        }
        public void  draw(){
        
        
        
        }
        
        

   




    }
}