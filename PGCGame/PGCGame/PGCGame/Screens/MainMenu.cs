﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Glib.XNA.SpriteLib;
using Glib.XNA;
using Glib;

namespace PGCGame.Screens
{
    public class MainMenu : Screen
    {
        public MainMenu(SpriteBatch spriteBatch)
            : base(spriteBatch, Color.Black)
        {
            
        }

        TextSprite SinglePlayerLabel;
        TextSprite MultiPlayerLabel;
        TextSprite BackLabel;
        TextSprite OptionsLabel;
        TextSprite CreditsLabel;

        public void LoadContent(ContentManager content)
        {
            //TODO: LOAD CONTENT

            //use Sprites to load your sprites
            //EX: Sprites.Add(new Sprite(content.Load<Texture2D>("assetName"), new Vector2(0, 0), Sprites.SpriteBatch));
            //OR
            //EX: Sprites.AddNewSprite(new Vector(0, 0), content.Load<Texture2D("assetName"));
            
            Sprite Title = new Sprite(content.Load<Texture2D>("Gametitle"), new Vector2(25), Sprites.SpriteBatch);
            Sprites.Add(Title);

            Sprite SinglePlayerButton = new Sprite(content.Load<Texture2D>("Button"), new Vector2(50, 100), Sprites.SpriteBatch);
            SinglePlayerLabel = new TextSprite(Sprites.SpriteBatch, new Vector2(72, 110), content.Load<SpriteFont>("TitleFont"), "Singleplayer");
            SinglePlayerLabel.Color = Color.White;
            SinglePlayerButton.MouseEnter += new EventHandler(SinglePlayerButton_MouseEnter);
            SinglePlayerButton.MouseLeave += new EventHandler(SinglePlayerButton_MouseLeave);

            Sprites.Add(SinglePlayerButton);
            AdditionalSprites.Add(SinglePlayerLabel);

            Sprite MultiPlayerButton = new Sprite(content.Load<Texture2D>("Button"), new Vector2(50, 195), Sprites.SpriteBatch);
            MultiPlayerLabel = new TextSprite(Sprites.SpriteBatch, new Vector2(78, 205), content.Load<SpriteFont>("TitleFont"), "Multiplayer");
            MultiPlayerLabel.Color = Color.White;
            MultiPlayerButton.MouseEnter += new EventHandler(MultiPlayerButton_MouseEnter);
            MultiPlayerButton.MouseLeave += new EventHandler(MultiPlayerButton_MouseLeave);

            Sprites.Add(MultiPlayerButton);
            AdditionalSprites.Add(MultiPlayerLabel);  

            Sprite BackButton = new Sprite(content.Load<Texture2D>("Button"), new Vector2(50, 290), Sprites.SpriteBatch);
            BackLabel = new TextSprite(Sprites.SpriteBatch, new Vector2(115, 299), content.Load<SpriteFont>("TitleFont"), "Back");
            BackLabel.Color = Color.White;
            BackButton.MouseEnter += new EventHandler(BackButton_MouseEnter);
            BackButton.MouseLeave += new EventHandler(BackButton_MouseLeave);

            Sprites.Add(BackButton);
            AdditionalSprites.Add(BackLabel);

            Sprite OptionsButton = new Sprite(content.Load<Texture2D>("Button"), new Vector2(290, 100), Sprites.SpriteBatch);
            OptionsLabel = new TextSprite(Sprites.SpriteBatch, new Vector2(340, 110), content.Load<SpriteFont>("TitleFont"), "Options");
            OptionsLabel.Color = Color.White;
            OptionsButton.MouseEnter += new EventHandler(OptionsButton_MouseEnter);
            OptionsButton.MouseLeave += new EventHandler(OptionsButton_MouseLeave);

            Sprites.Add(OptionsButton);
            AdditionalSprites.Add(OptionsLabel);

            Sprite CreditsButton = new Sprite(content.Load<Texture2D>("Button"), new Vector2(290, 195), Sprites.SpriteBatch);
            CreditsLabel = new TextSprite(Sprites.SpriteBatch, new Vector2(340, 205), content.Load<SpriteFont>("TitleFont"), "Credits");
            CreditsLabel.Color = Color.White;
            CreditsButton.MouseEnter += new EventHandler(CreditsButton_MouseEnter);
            CreditsButton.MouseLeave += new EventHandler(CreditsButton_MouseLeave);

            Sprites.Add(CreditsButton);
            AdditionalSprites.Add(CreditsLabel);
        }

        //credits button
        void CreditsButton_MouseLeave(object sender, EventArgs e)
        {
            CreditsLabel.Color = Color.White;
        }
        void CreditsButton_MouseEnter(object sender, EventArgs e)
        {
            CreditsLabel.Color = Color.MediumAquamarine;
        }


        //options button
        void OptionsButton_MouseLeave(object sender, EventArgs e)
        {
            OptionsLabel.Color = Color.White;
        }
        void OptionsButton_MouseEnter(object sender, EventArgs e)
        {
            OptionsLabel.Color = Color.MediumAquamarine;
        }


        //multiplayer button
        void MultiPlayerButton_MouseLeave(object sender, EventArgs e)
        {
            MultiPlayerLabel.Color = Color.White;
        }
        void MultiPlayerButton_MouseEnter(object sender, EventArgs e)
        {
            MultiPlayerLabel.Color = Color.MediumAquamarine;
        }

        bool mouseInBackButton = false;

        //back button
        void BackButton_MouseLeave(object sender, EventArgs e)
        {
            BackLabel.Color = Color.White;
            mouseInBackButton = false;
        }
        void BackButton_MouseEnter(object sender, EventArgs e)
        {
            BackLabel.Color = Color.MediumAquamarine;
            mouseInBackButton = true;
        }


        //singleplayer button
        void SinglePlayerButton_MouseLeave(object sender, EventArgs e)
        {
            SinglePlayerLabel.Color = Color.White;
        }
        void SinglePlayerButton_MouseEnter(object sender, EventArgs e)
        {
            SinglePlayerLabel.Color = Color.MediumAquamarine;
        }

        
        public override void Update(GameTime gameTime)
        {
            //TODO: UPDATE SPRITES
            base.Update(gameTime);
            if (mouseInBackButton && Microsoft.Xna.Framework.Input.Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                StateManager.ScreenState = ScreenState.Title;
            }
        }

    }
}
