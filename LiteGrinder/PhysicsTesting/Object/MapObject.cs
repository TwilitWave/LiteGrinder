using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder.Object
{
    abstract class MapObject
    {

        public MapObject()
        {

        }

        public abstract void Update(World world);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Delete(World world);
    }
}
