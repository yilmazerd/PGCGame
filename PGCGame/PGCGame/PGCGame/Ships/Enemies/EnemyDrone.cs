﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Glib.XNA;
using PGCGame.CoreTypes;

namespace PGCGame.Ships.Enemies
{
    public class EnemyDrone : BaseEnemyShip
    {
        public EnemyDrone(Texture2D texture, Vector2 location, SpriteBatch spriteBatch)
            : base(texture, location, spriteBatch)
        {
            Scale = new Vector2(.75f);

            DamagePerShot = 5;

            _initHealth = 1;

            BulletTexture = GameContent.GameAssets.Images.Ships.Bullets[CoreTypes.ShipType.Drone, ShipTier.Tier1];
        }

        public override ShipType ShipType
        {
            get { return ShipType.Drone; }
        }

        public override string FriendlyName
        {
            get { return "Enemy Ship"; }
        }
    }
}





