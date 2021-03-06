﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Glib.XNA;
using Glib;
using Glib.XNA.SpriteLib;


namespace PGCGame
{
    public class Bullet : Sprite
    {
        /// <summary>
        /// The maximum distance a bullet can travel before death.
        /// If null, it can travel indefinently.
        /// </summary>
        public Vector2? MaximumDistance = new Vector2(4000f);


        public Bullet(Texture2D texture, Vector2 location, SpriteBatch spriteBatch)
            : base(texture, location, spriteBatch)
        {
        }
        public int Damage { get; set; }

        private bool _isDead = false;

        /// <summary>
        /// The amount of distance traveled, world coords.
        /// </summary>
        private Vector2 _traveledDistance = Vector2.Zero;

        public override void Update()
        {         
            base.Update();


            //Position += Speed;


            _traveledDistance += Speed;
            if (MaximumDistance.HasValue)
            {
                if (_traveledDistance.LengthSquared() >= MaximumDistance.Value.LengthSquared())
                {
                    _isDead = true;
                }
            }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }
        
         
    }
}
