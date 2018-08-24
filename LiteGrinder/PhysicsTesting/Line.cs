using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhysicsTesting
{
    /// <summary>
    /// Line class credit to Stan on stackoverflow
    /// stackoverflow.com/questions/13017448/drawing-a-line-where-the-mouse-went
    /// </summary>
    public class Line
    {
        Texture2D texture;
        Vector2 start, end, origin;
        float width;
        Color color;
        float angle, length;

        public Line(Texture2D texture, Vector2 start, Vector2 end, float width, float length, float angle, Color color, Vector2 origin)
        {
            this.texture = texture;
            this.start = start;
            this.end = end;
            this.width = width;
            this.color = color;
            this.length = length;
            this.angle = angle;
            this.origin = origin;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, start, null, color, angle, Vector2.Zero, new Vector2(length, width), SpriteEffects.None, 0);
        }

    }
}
