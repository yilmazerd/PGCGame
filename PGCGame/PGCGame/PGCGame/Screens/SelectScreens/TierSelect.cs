﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Glib;
using Glib.XNA;
using Glib.XNA.SpriteLib;

namespace PGCGame.Screens.SelectScreens
{
    class TierSelect  : BaseSelectScreen
    {
        public TierSelect(SpriteBatch spriteBatch)
            : base(spriteBatch)
        {
            
        }

        //This screen needs sprites for each tier of ship and descriptions for each tier.
        List<KeyValuePair<Sprite, string>> itemsShown = new List<KeyValuePair<Sprite, string>>();

        public override void LoadContent(ContentManager content)
          {
            Sprite ship1;
            Sprite ship2;
            Sprite ship3;
            Texture2D buttonImage = content.Load<Texture2D>("Images\\Controls\\Button");
            SpriteFont SegoeUIMono = content.Load<SpriteFont>("Fonts\\SegoeUIMono");

            
            ship1 = new Sprite(content.Load<Texture2D>("Images\\Battle Cruiser\\Tier2"), Vector2.Zero, Sprites.SpriteBatch);
            TextSprite text1 = new TextSprite(Sprites.SpriteBatch, Vector2.Zero, content.Load<SpriteFont>("Fonts\\SegoeUIMono"), "\n\n This is the strongest class \n in the fleet, but also the slowest.\n What it lacks in speed it makes \n up for in strength.\n\n Damage Per Shot: 20\n Amount of Health: 120");
            text1.Color = Color.White;
            ship1.Position = new Vector2(Sprites.SpriteBatch.GraphicsDevice.Viewport.Width * 0.81f, Sprites.SpriteBatch.GraphicsDevice.Viewport.Height * .12f);
            ship1.Rotation = new SpriteRotation(90);

            items.Add(new KeyValuePair<Sprite, TextSprite>(ship1, text1));

            itemsShown.Add(new KeyValuePair<Sprite, string>(ship1, "Battle Cruiser"));

            ship2 = new Sprite(content.Load<Texture2D>("Images\\Fighter Carrier\\Tier2"), Vector2.Zero, Sprites.SpriteBatch);
            TextSprite text2 = new TextSprite(Sprites.SpriteBatch, Vector2.Zero, content.Load<SpriteFont>("Fonts\\SegoeUIMono"), "\n\n This class fires an extremely fast\n Flak Cannon and has the ability to\n deploy drones. However, the drones\n and Flak Cannon aren't that powerful.\n After the Carrier gets destroyed, the\n drones die with it.\n\n Damage Per Shot: 2\n Amount of Health: 100\n Amount of Drones: 2\n Damage Per Drone Shot: 1\n Health Per Drone: 10");
            ship2.Position = new Vector2(Sprites.SpriteBatch.GraphicsDevice.Viewport.Width * 0.85f, Sprites.SpriteBatch.GraphicsDevice.Viewport.Height * .01f);
            ship2.Rotation = new SpriteRotation(90);

            items.Add(new KeyValuePair<Sprite, TextSprite>(ship2, text2));

            itemsShown.Add(new KeyValuePair<Sprite, string>(ship2, "Fighter Carrier"));

            ship3 = new Sprite(content.Load<Texture2D>("Images\\Torpedo Ship\\Tier2"), Vector2.Zero, Sprites.SpriteBatch);
            TextSprite text3 = new TextSprite(Sprites.SpriteBatch, Vector2.Zero, content.Load<SpriteFont>("Fonts\\SegoeUIMono"), "\n\n This class is the most balanced\n ship in the game. The torpedos do\n a lot of damage and \n are hard to dodge!\n\n Damege Per Shot: 5\n Amount of Health: 110");
            ship3.Position = new Vector2(Sprites.SpriteBatch.GraphicsDevice.Viewport.Width * 0.81f, Sprites.SpriteBatch.GraphicsDevice.Viewport.Height * .12f);
            ship3.Rotation = new SpriteRotation(90);

            items.Add(new KeyValuePair<Sprite, TextSprite>(ship3, text3));

            itemsShown.Add(new KeyValuePair<Sprite, string>(ship3, "Torpedo Ship"));

            ChangeItem += new EventHandler(TierSelect_ChangeItem);

            base.LoadContent(content);

            acceptLabel.Text = "Buy";
        }

        void TierSelect_ChangeItem(object sender, EventArgs e)
        {
            foreach (KeyValuePair<Sprite, string> item in itemsShown)
            {
                if (item.Key == items[selected].Key)
                {
                    nameLabel.Text = item.Value;
                    break;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
