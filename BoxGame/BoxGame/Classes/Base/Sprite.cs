using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Indiv0.BoxGame.Classes.Base
{
    class Sprite
    {
        #region Class Variables
        private Texture2D _texture;
        private Vector2 _position;
        private TerrainTypes _terrainType;
        //private SpriteBatch _spriteBatch;
        //private GraphicsDevice _graphicsDevice;
        private string _textureString;
        private int _width;
        private int _height;
        private bool _visible = true;

        #endregion

        #region Public Variables
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public string TextureString
        {
            get { return _textureString; }
            set { _textureString = value; }
        }
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public enum TerrainTypes
        {
            Water,
            Grass,
            Mountain,
            None
        }
        public TerrainTypes TerrainType
        {
            get { return _terrainType; }
            set { _terrainType = value; }
        }
        #endregion

        public Sprite(string texString, int x, int y, int texWidth, int texHeight)
        {
            //_graphicsDevice = graphicsDevice;
            //_spriteBatch = spriteBatch;
            _textureString = texString;
            _position.X = x;
            _position.Y = y;
            _width = texWidth;
            _height = texHeight;
            _terrainType = Sprite.TerrainTypes.None;
        }
                        
        //public void DrawSprite()
        //{
        //    if (_visible == true)
        //    {
        //        if (_texture != null)
        //        {
        //            if (_spriteBatch != null)
        //            {
        //                _spriteBatch.Draw(_texture, _position, Color.White);
        //            }
        //        }
        //    }
        //}

        //public void DrawSprite(float scale)
        //{
        //    if (_visible == true)
        //    {
        //        if (_texture != null)
        //        {
        //            if (_spriteBatch != null)
        //            {
        //                _spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        //            }
        //        }
        //    }
        //}
    }
}
