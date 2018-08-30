using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsTesting;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace LiteGrinder
{
    class CollectableItem : LiteGrinder.Object.Object
    {
        public static List<Body> collectedItems = new List<Body>();

        private World world;
        private Body body;
        private float radius, density;
        private Vector2 position;
        private BodyType bodytype;
        private CircleShape circle;
        private Fixture fixture;

        public CollectableItem()
        {

        }

        public CollectableItem(World world, float radius, float density, Vector2 position, BodyType bodytype)
        {
            this.body = world.CreateCircle(ConvertUnits.ToSimUnits(radius), density, position, bodytype);
            this.circle = new CircleShape(ConvertUnits.ToSimUnits(radius), density);
            this.fixture = body.CreateFixture(circle);
            this.body.SetIsSensor(true);
            fixture.OnCollision += OnCollision;
        }

        public bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            Body body1 = fixtureA.Body;
            Body body2 = fixtureB.Body;

            if (!collectedItems.Contains(body1))
            {
                collectedItems.Add(body1);
                return false;
            }

            return true;
        }

        public override void Update(World world) 
        {
            for( int i = 0; i < collectedItems.Count; i++)
            {
                world.Remove(collectedItems[i]);
            }

            collectedItems.Clear();
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {

        }
    }
}
