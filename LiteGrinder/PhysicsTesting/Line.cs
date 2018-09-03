using System.Collections.Generic;
using System.Diagnostics;
using LiteGrinder.Object;
using LyteGrinder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder
{
    /// <summary>
    /// Line class credit to Stan on stackoverflow
    /// stackoverflow.com/questions/13017448/drawing-a-line-where-the-mouse-went
    /// </summary>
    public class Line
    {
        Texture2D texture, oldLineText;
        Vector2 start, origin;
        float width, angle, length;
        Body body;
        private static List<Line> lines = new List<Line>();
        private static Line[] oldLines = new Line[0];

        public Line(World world, Texture2D texture, Texture2D oldText, Vector2 oldMousePos, Vector2 mousePos, Vector2 camOffset, float width, float length, float angle, out bool success)
        {
            this.texture = texture;
            this.oldLineText = oldText;
            this.start = oldMousePos + camOffset;
            this.width = width;
            this.length = length;
            this.angle = angle;
            this.origin = new Vector2(length/2,width/2);
            
            if (length > 25)
            {
                int fract = (int)length / 25;
                fract += 2;
                float newLength = 25;
                Vector2[] points = new Vector2[fract];
                points[0] = oldMousePos;
                points[fract-1] = mousePos;
                Vector2 fractVec = new Vector2(fract, fract);
                Vector2 divVec = Vector2.Divide(points[fract - 1] - points[0], fractVec);
                bool t;
                for (int i = 1; i < fract; i++)
                {
                    if (i != fract - 1)
                    {
                        points[i] = points[i - 1] + divVec;
                    }
                    newLength = Vector2.Distance(points[i], points[i - 1]);

                    if (newLength > 0)
                    {
                        new Line(world, texture, oldText, points[i - 1], points[i], camOffset, width, newLength, angle, out t);
                        if(!t)
                        {
                            Game1.totalLength -= newLength;
                        }
                    }
                }
                success = true;
                return;
            }

            foreach (NoDrawArea area in NoDrawArea.noDrawAreas)
            {
                if (area.rect.Contains(mousePos))
                {
                    success = false;
                    return;
                }
            }

            Body box = world.CreateRectangle(ConvertUnits.ToSimUnits(length),
              ConvertUnits.ToSimUnits(width), 10f, oldMousePos, angle);

            box.BodyType = BodyType.Static;
            box.SetRestitution(0f);
            box.SetFriction(1f);

            float neg1 = 1;
            float neg2 = 1;
            neg1 = (mousePos.X - oldMousePos.X < 0) ? -1 : 1;
            neg2 = (mousePos.Y - oldMousePos.Y < 0) ? -1 : 1;

            Vector2 boxPos = ConvertUnits.ToSimUnits(oldMousePos + ((mousePos - oldMousePos) / 2));
            box.Position = boxPos - ConvertUnits.ToSimUnits(new Vector2(neg2 * (width / 2), neg1 * (-(width / 2))));
            box.Position += ConvertUnits.ToSimUnits(camOffset);

            box.Tag = this;
            body = box;

            lines.Add(this);
            success = true;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Line line in oldLines)
            {
                spriteBatch.Draw(line.oldLineText, line.start, null, Color.Gray, line.angle, Vector2.Zero, new Vector2(line.length, line.width), SpriteEffects.None, 0);
            }
            foreach (Line line in lines)
            {
                spriteBatch.Draw(line.texture, line.start, null, Color.Yellow, line.angle, Vector2.Zero, new Vector2(line.length, line.width), SpriteEffects.None, 0);
            }
        }

        public static void Reset(World world)
        {
            if (lines.Count > 0)
            {
                foreach (Line b in lines)
                {
                    world.Remove(b.body);
                }
                oldLines = new Line[lines.Count];
                lines.CopyTo(oldLines);
                lines.Clear();
            }
        }

        public static void ClearGhostLine()
        {
            oldLines = new Line[0];
        }

        public void DeleteLine(World world)
        {
            Game1.totalLength -= length;
            world.Remove(body);
            lines.Remove(this);
        }
    }
}
