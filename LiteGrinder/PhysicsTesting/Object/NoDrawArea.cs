using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace LiteGrinder.Object
{

    class NoDrawArea : LiteGrinder.Object.MapObject
    {
        public static List<NoDrawArea> noDrawAreas = new List<NoDrawArea>();

        private static List<Line> deleteLines = new List<Line>();

        public Rectangle rect;
        private Vector2 position;
        private float width, height;
        private float rotation;
        private Texture2D text;
        private Body body;
        private World world;
        
        public NoDrawArea()
        {
        }

        public NoDrawArea(World world, Vector2 position, float width, float height, float rotation, Texture2D texture2d)
        {
            rect = new Rectangle((int)ConvertUnits.ToDisplayUnits(position.X - width/2), (int)ConvertUnits.ToDisplayUnits(position.Y - height/2)
                , (int)ConvertUnits.ToDisplayUnits(width), (int)ConvertUnits.ToDisplayUnits(height));
            this.width = ConvertUnits.ToDisplayUnits(width);
            this.height = ConvertUnits.ToDisplayUnits(height);
            this.position = position;
            this.rotation = rotation;
            this.text = texture2d;
            this.body = world.CreateRectangle(width, height, 10f, position, rotation);
            this.body.SetIsSensor(true);

            noDrawAreas.Add(this);
        }

        public override void Update(World world)
        {
            foreach (NoDrawArea area in noDrawAreas)
            {
                foreach (Body body in world.BodyList)
                {
                    if(body.Tag != null && body.Tag is Line)
                    {
                        if (area.rect.Contains(ConvertUnits.ToDisplayUnits(body.Position)))
                        {
                            deleteLines.Add((Line)body.Tag);
                        }
                    }
                }
            }

            for (int i = 0; i < deleteLines.Count; i++)
            {
                deleteLines[i].DeleteLine(world);
            }
            deleteLines.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(NoDrawArea area in noDrawAreas)
            {
                if (area.text != null)
                {
                    spriteBatch.Draw(area.text, ConvertUnits.ToDisplayUnits(area.position), new Rectangle(0, 0, (int)area.width, (int)area.height),
                        Color.White, rotation, new Vector2(area.width / 2, area.height / 2), new Vector2(1, 1), SpriteEffects.None, 0f);
                }
            }
        }

        public override void Delete(World world)
        {
            foreach (NoDrawArea area in noDrawAreas)
            {
                world.Remove(area.body);
            }
            noDrawAreas.Clear();
        }
    }
}
