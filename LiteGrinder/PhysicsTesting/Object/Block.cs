using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsTesting;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder.Object
{

    class Block : LiteGrinder.Object.Object
    {
        private World world;
        private static List<Block> blocks = new List<Block>();
        private Body block;
        private Vector2 position;
        private float width, height;
        private float density, rotation;
        private BodyType bodytype;
        private Texture2D texture2d;

        private Vector2 origin;

        private float restitution = 0.0f;
        private float friction = 0.0f;

        public Block()
        {

        }

        public Block(World world, Vector2 position, float width, float height, float density, float rotation, BodyType bodytype, Texture2D texture2d)
        {
            this.world = world;
            this.position = position;
            this.width = ConvertUnits.ToDisplayUnits(width);
            this.height = ConvertUnits.ToDisplayUnits(height);
            this.density = density;
            this.rotation = rotation;
            this.bodytype = bodytype;
            this.texture2d = texture2d;

            this.origin = new Vector2(texture2d.Width / 2f, texture2d.Height / 2f);

            this.block = world.CreateRectangle(width, height, density, position, rotation, bodytype);
            block.SetRestitution(restitution);
            block.SetFriction(friction);

            blocks.Add(this);
        }

        public override void Update(World world)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach(Block block in blocks)
            {
                spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(block.position), new Rectangle(0, 0, (int)block.width, (int)block.height), Color.White, 0, new Vector2(block.width / 2, block.height / 2), new Vector2(1, 1), SpriteEffects.None, 0f);

            }
        }
    }
}
