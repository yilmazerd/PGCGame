﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace GitPractice
{
    public class MovingSprite : BaseSprite
    {
        /* TODO: MATTHEW:
         * 
         * Inherit from BaseSprite
         * CANNOT move offscreen
         * 
         * Protected fields (aka variables): _keyUp, _keyDown, _keyLeft, _keyRight
         *                                   _speed
         *                                   
         */

        public Keys KeyUp { get { return _KeyUp; } set { _KeyUp = value; } }

        public Keys KeyDown { get { return _KeyDown; } set { _KeyDown = value; } }

        public Keys KeyRight { get { return _KeyRight; } set { _KeyRight = value; } }

        public Keys KeyLeft { get { return _KeyLeft; } set { _KeyLeft = value; } }

        protected Keys _KeyUp;
        protected Keys _KeyDown;
        protected Keys _KeyRight;
        protected Keys _KeyLeft;

        protected Vector2 _speed;
        public Vector2 Speed { get{return _speed;} set {_speed = value;} }

        public void LoadContent(ContentManager content, string assetName, Vector2 speed)
        {
             _KeyLeft = Keys.A;
             _KeyDown = Keys.S;
             _KeyRight = Keys.D;
            _KeyUp = Keys.W;

            _speed = speed;

            base.LoadContent(content, assetName);
        }

        public new void LoadContent(ContentManager content, string assetName)
        {
            LoadContent(content, assetName, new Vector2());
        }

        public void Update(KeyboardState keyboard, GameTime gameTime, GameState gameState, Viewport viewport)
        {
            if (keyboard.IsKeyDown(_KeyUp) && Location.Y - Speed.Y > 0)
            {
                Location = new Vector2(Location.X, Location.Y - _speed.Y);
            }
            if (keyboard.IsKeyDown(_KeyDown) && Location.Y + Texture.Height + Speed.Y < viewport.Height)
            {
                Location = new Vector2(Location.X, Location.Y + _speed.Y);
            }
            if (keyboard.IsKeyDown(_KeyRight) && Location.X + Texture.Width + Speed.X < viewport.Width)
            {
                Location = new Vector2(Location.X + _speed.X, Location.Y);
            }
            if (keyboard.IsKeyDown(_KeyLeft) && Location.X - Speed.X > 0)
            {
                Location = new Vector2(Location.X - _speed.X, Location.Y);
            } 
        }
        
    }
}