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

using PGCGame.CoreTypes;

namespace PGCGame
{
    public abstract class Ship : Sprite, ITimerSprite
    {
        protected const bool CanHoldShootKey = true;

        public static Texture2D DroneBullet;
        public static Texture2D BattleCruiserBullet;
        public static Texture2D FighterCarrierBullet;
        public static Texture2D Torpedo;
        public static Texture2D SpaceMine;
        MouseState ms;
        ProgressBar healthBar;
        MouseState lastms;
        
        public abstract string TextureFolder { get; }

        public bool RotateTowardsMouse = true;

        public Guid PlayerID
        {
            get { return StateManager.PlayerID; }
        }
        

        public Ship(Texture2D texture, Vector2 location, SpriteBatch spriteBatch)
            : base(texture, location, spriteBatch)
        {
            StateManager.ActiveShips.Add(this);
            healthBar = new ProgressBar(new Vector2(X, Y), Color.DarkGreen, Color.Red, spriteBatch);
            healthBar.WidthScale = 1;
            healthBar.HeightScale = 10;
        }

        public PlayerType PlayerType { get; set; }

        public abstract void Shoot();

        //override: 
        //Update
        //DrawNonAuto

        public Texture2D BulletTexture { get; set; }

        public SpriteBatch WorldSb;
        private TimeSpan _elapsedShotTime = new TimeSpan();
        protected KeyboardState _lastKs = new KeyboardState();

        public abstract string FriendlyName { get; }

        protected Stack<SpaceMine> _spaceMines = new Stack<SpaceMine>();
        public SpaceMine ActiveSpaceMine { get; set; }

        public Stack<SpaceMine> SpaceMines
        {
            get { return _spaceMines; }
            set { _spaceMines = value; }
        }

        bool _isFirstUpdate = true;

        public virtual void Update(GameTime gt)
        {
            base.Update();
            if (_isFirstUpdate)
            {
                healthBar.Position = new Vector2(X - (healthBar.Width/2), Y - (Height / 1.5f));
                CurrentHealth = InitialHealth;
            }
            if (RotateTowardsMouse)
            {


                ms = Mouse.GetState();
                Vector2 mousePos = new Vector2(ms.X, ms.Y);
                Vector2 targetPos = mousePos - Position;

                //Rotate towards mouse
                Rotation.Radians = Math.Atan2(targetPos.X, -targetPos.Y).ToFloat();
            }

            foreach (Bullet b in FlyingBullets)
            {
                b.Update();
            }
            //Shoot
            KeyboardState ks = Keyboard.GetState();
            _elapsedShotTime += gt.ElapsedGameTime;
            //Shoot w/ space key

            if (CanShoot)
            {
                if((StateManager.Options.LeftButtonEnabled && ms.LeftButton == ButtonState.Pressed) || (!StateManager.Options.LeftButtonEnabled && ks.IsKeyDown(Keys.Space)))
                {
                    Shoot();
                    _elapsedShotTime = new TimeSpan();
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
            lastms = ms;

            healthBar.Denominator = InitialHealth;
            healthBar.Value = CurrentHealth;
        }

        private Vector2 _worldPos;

        public bool IsPlayerShip = false;

        public Vector2 WorldCoords
        {
            get { return IsPlayerShip ? _worldPos : Position; }
            set {
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
        

        public bool CanShoot
        {
            get
            {
                return _elapsedShotTime >= DelayBetweenShots;
            }
        }

        public override void DrawNonAuto()
        {
            base.DrawNonAuto();

            if (InitialHealth > 1 && (IsPlayerShip || StateManager.HasBoughtScanner))
            {
                healthBar.DrawNonAuto();
            }

            //SpriteBatch.Draw(Texture, Position, DrawRegion, Color.White, Rotation.Radians, Origin, Scale, Effect, 0);

            //TODO: Draw Bullets; currently in each ship class 
        }

        public int DamagePerShot { get; set; }
        public int Cost { get; set; }

        public TimeSpan DelayBetweenShots { get; set; }

        private Vector2 _movementSpeed = Vector2.One;


        public Vector2 MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        public int CurrentHealth { get; set; }

        protected int _initHealth;

        public int InitialHealth
        {
            get { return _initHealth; }
            set { _initHealth = value; }
        }
        

        public int Shield { get; set; }

        public int Armor { get; set; }

        private List<Bullet> _flyingBullets = new List<Bullet>();


        private ShipTier _shipTier = ShipTier.Tier1;

        public event EventHandler TierChanged;

        /// <summary>
        /// Gets or sets the tier of the ship.
        /// </summary>
        public ShipTier Tier
        {
            get { return _shipTier; }
            set {
                _shipTier = value;
                if (TierChanged != null)
                {
                    TierChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// The percentage of the total height of the ship that the nose is from the center.
        /// </summary>
        public float DistanceToNose;

        public List<Bullet> FlyingBullets
        {
            get { return _flyingBullets; }
            set { _flyingBullets = value; }
        }
    }
}
