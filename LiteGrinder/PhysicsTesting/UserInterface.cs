﻿using LyteGrinder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LiteGrinder
{
    class UserInterface
    {
        private static ContentManager Content;
        private SpriteFont font;

        private int score;
        private float availableInkRate;

        public UserInterface(ContentManager contentmanager)
        {
            Content = contentmanager;
            font = Content.Load<SpriteFont>("DiagnosticsFont");
            this.score = 0;
            availableInkRate = 0; 
        }

        public void Update()
        {
            availableInkRate = 100 - (Game1.totalLength / Game1.maxLength * 100);
            if(availableInkRate < 0) { availableInkRate = 0; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score:" + score, new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, (int)availableInkRate + "%", new Vector2(100, 200), Color.Black);
            spriteBatch.End();
        }

    }
}