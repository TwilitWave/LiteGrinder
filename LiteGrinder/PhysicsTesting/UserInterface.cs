using LiteGrinder.Object;
using LyteGrinder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LiteGrinder
{
    class UserInterface
    {
        private static ContentManager Content;
        private SpriteFont font;

        private int score;
        private float availableInkRate;

        private Texture2D start;
        private Texture2D pause;
        private Texture2D reset;

        private Button startButton;
        private Button pauseButton;
        private Button resetButton;


        public UserInterface(ContentManager contentmanager)
        {
            Content = contentmanager;
            font = Content.Load<SpriteFont>("DiagnosticsFont");
            this.score = 0;
            availableInkRate = 0;

            //start = Content.Load<Texture2D>("Play Button");
            //pause = Content.Load<Texture2D>("Pause Button");
            //reset = Content.Load<Texture2D>("Reset Button");

            //startButton = new Button("start", start, pause, 0, 950);
            //resetButton = new Button("reset", reset, 200, 950);

        }

        public void Update(GameTime gametime, MouseState mouseState, MouseState oldMouseState)
        {
            availableInkRate = 100 - (Game1.totalLength / Game1.maxLength * 100);
            if(availableInkRate < 0) { availableInkRate = 0; }

            //startButton.Update(gametime, mouseState, oldMouseState);
            //resetButton.Update(gametime, mouseState, oldMouseState,3);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Strawberries: " + CollectableItem.numberofcollectable, new Vector2(10, 750), Color.Red);
            spriteBatch.DrawString(font, "Ink: " + (int)availableInkRate + "%", new Vector2(10, 825), Color.Yellow);
            spriteBatch.DrawString(font, "Boost Tubes: " + Game1.numberofJet, new Vector2(10, 900), Color.Green);
            spriteBatch.DrawString(font, "Left-Click: Draw", new Vector2(450, 900), Color.White);
            spriteBatch.DrawString(font, "Right-Click: (Hold) Aim Boost Tube", new Vector2(450, 940), Color.White);
            spriteBatch.DrawString(font, "                   (Release) Place Boost Tube", new Vector2(450, 975), Color.White); 
            spriteBatch.DrawString(font, "Space(After 'Play'): Jump", new Vector2(450, 1010), Color.White);

            spriteBatch.End();
        }

    }
}
