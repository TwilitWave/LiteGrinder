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
<<<<<<< HEAD
            spriteBatch.DrawString(font, "NumberofCollectable:" + CollectableItem.numberofcollectable, new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, (int)availableInkRate + "%", new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(font, "Jet Area : " + Game1.numberofJet, new Vector2(100, 300), Color.Black);

            //startButton.Draw(spriteBatch);
            //resetButton.Draw(spriteBatch);

=======
            spriteBatch.DrawString(font, "Strawberries: " + CollectableItem.numberofcollectable, new Vector2(100, 100), Color.Red);
            spriteBatch.DrawString(font, "Ink: " + (int)availableInkRate + "%", new Vector2(100, 200), Color.Yellow);
            spriteBatch.DrawString(font, "Boost Tubes: " + Game1.numberofJet, new Vector2(100, 300), Color.Green);
>>>>>>> 08106152da6311f68084b4fcee4b81ed54d0c069
            spriteBatch.End();
        }

    }
}
