﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PGCGame.Ships;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Glib;
using Glib.XNA;
using Glib.XNA.SpriteLib;
using PGCGame.CoreTypes;
using Glib.XNA.InputLib;

namespace PGCGame.Ships.Allies
{
    public abstract class BaseAllyShip : Ship
    {
        private TimeSpan _elapsedShotTime = TimeSpan.Zero;

        private MouseState ms;
        private MouseState lastms;

        Rectangle WCRectangle = new Rectangle();

        protected Vector2 _worldPos;
        protected bool _isPlayerShip;
        protected bool _rotateTowardMouse;

        protected Stack<SpaceMine> _spaceMines = new Stack<SpaceMine>();

        public bool IsPlayerShip
        {
            get
            {
                return _isPlayerShip;
            }
            set
            {
                _isPlayerShip = value;
            }
        }


        public override Vector2 WorldCoords
        {
            get
            {
                return IsPlayerShip ? _worldPos : Position;
            }
            set
            {
                if (!IsPlayerShip)
                {
                    Position = value;
                }
                else
                {
                    _worldPos = value;
                }
            }
        }

        public bool RotateTowardsMouse
        {
            get
            {
                return _rotateTowardMouse;
            }
            set
            {
                _rotateTowardMouse = value;
            }
        }

        public bool CanShoot
        {
            get
            {
                return _elapsedShotTime >= DelayBetweenShots;
            }
        }

        public SpaceMine ActiveSpaceMine { get; set; }

        public Stack<SpaceMine> SpaceMines
        {
            get
            {
                return _spaceMines;
            }
            set
            {
                _spaceMines = value;
            }
        }

        public List<SecondaryWeapon> SecondaryWeapons;

        public BaseAllyShip(Texture2D texture, Vector2 location, SpriteBatch spriteBatch)
            : base(texture, location, spriteBatch)
        {
            PlayerType = CoreTypes.PlayerType.Ally;
        }

        public override void Update(GameTime gt)
        {
            if (CurrentHealth <= 0)
            {
                base.Update(gt);

                if (FlyingBullets.Count == 0)
                {
                    _isDead = true;
                }

                return;
            }
            foreach (Ship enemyShip in StateManager.ActiveShips)
            {
                if (enemyShip.PlayerType != CoreTypes.PlayerType.Ally)
                {
                    foreach (Bullet b in enemyShip.FlyingBullets)
                    {
                        WCRectangle = new Rectangle(this.WorldCoords.X.ToInt(), this.WorldCoords.Y.ToInt(), this.Width.ToInt(), this.Height.ToInt());
                        if (b.Rectangle.Intersects(WCRectangle))
                        {
                            this.CurrentHealth -= b.Damage;
                            b.IsDead = true;
                        }
                    }
                }
            }

            if (RotateTowardsMouse)
            {
#if WINDOWS
                ms = MouseManager.CurrentMouseState;
                Vector2 mousePos = new Vector2(ms.X, ms.Y);
                Vector2 targetPos = mousePos - Position;

                //Rotate towards mouse
                Rotation.Radians = Math.Atan2(targetPos.X, -targetPos.Y).ToFloat();

                lastms = ms;
#elif XBOX
                if (new Vector2(Math.Round(GamePadManager.One.Current.ThumbSticks.Right.X * 10).ToFloat() / 10.ToFloat(), Math.Round(GamePadManager.One.Current.ThumbSticks.Right.Y * 10).ToFloat() / 10.ToFloat()) != Vector2.Zero)
                {
                    Rotation.Vector = new Vector2(GamePadManager.One.Current.ThumbSticks.Right.X, -GamePadManager.One.Current.ThumbSticks.Right.Y);
                }
#endif
            }

            //Shoot
            KeyboardState ks = Keyboard.GetState();
            _elapsedShotTime += gt.ElapsedGameTime;
            //Shoot w/ space key

            if (CanShoot)
            {
                if ((StateManager.Options.LeftButtonEnabled && ms.LeftButton == ButtonState.Pressed) || (!StateManager.Options.LeftButtonEnabled && ks.IsKeyDown(Keys.Space)))
                {
                    Shoot();
                    _elapsedShotTime = TimeSpan.Zero;
                }
            }

            //Deploy mine?
            if (SpaceMines.Count > 0 && ks.IsKeyDown(Keys.RightShift) && _lastKs != null && !_lastKs.IsKeyDown(Keys.RightShift))
            {
                ActiveSpaceMine = SpaceMines.Pop();
                ActiveSpaceMine.SpaceMineState = SpaceMineState.Deploying;
                ActiveSpaceMine.Update(gt);
            }

            if (ActiveSpaceMine != null)
            {
                ActiveSpaceMine.Update(gt);
            }

            _lastKs = ks;

            base.Update(gt);
        }
    }
}
