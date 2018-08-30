using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder.Object
{
    abstract class Object
    {
        private World world;
        private SpriteBatch spritBatch;
        private Texture2D texture;

        public Object()
        {

        }

        public abstract void Update(World world);

        public abstract void Draw(SpriteBatch spriteBatch, Texture2D texture);
    }
}
