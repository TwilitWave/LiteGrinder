using LyteGrinder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LiteGrinder.Object
{
    public class Button 
    {
        public int buttonX, buttonY;
        public string name;
        public Texture2D texture;
        public Texture2D texture1;
        public Texture2D texture2;
        public int textureSizeX = 200;
        public int textureSizeY = 100;

        public int ButtonX
        {
            get
            {
                return buttonX;
            }
        }

        public int ButtonY
        {
            get
            {
                return buttonY;
            }
        }

        public Button(string name, Texture2D texture1, Texture2D texture2, int buttonX, int buttonY)
        {
            this.name = name;
            this.texture1 = texture1;
            this.texture = this.texture1;
            this.texture2 = texture2;
            this.buttonX = buttonX;
            this.buttonY = buttonY;
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        public bool enterButton(MouseState mouseState)
        {
            if (mouseState.X < buttonX + textureSizeX &&
                    mouseState.X > buttonX &&
                    mouseState.Y < buttonY + textureSizeY &&
                    mouseState.Y > buttonY)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
                if(texture == texture1)
                {
                    Console.WriteLine(texture);
                    texture = texture2;
                    Game1.gameisproceeding = true;
                } else if(texture == texture2)
                {
                    Console.WriteLine(texture);
                    texture = texture1;
                    Game1.gameisproceeding = false;
                }

        }

        public void WhenYouHitResetButton()
        {
            if (texture == texture2)
            {
                Console.WriteLine(texture);
                texture = texture1;
                Game1.gameisproceeding = false;
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            Rectangle destinationRectangle = new Rectangle(buttonX, buttonY, textureSizeX, textureSizeY);
            sprite.Draw(texture, destinationRectangle, Color.White);
        }
    }
}
