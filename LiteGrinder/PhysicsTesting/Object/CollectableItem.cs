using Microsoft.Xna.Framework;
using PhysicsTesting;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace LiteGrinder
{
    class CollectableItem
    {
        private static List<Body> collectedItems = new List<Body>();

        private CollectableItem(World world)
        {
            Body body = world.CreateCircle(ConvertUnits.ToSimUnits(30), 2f, new Vector2(1f, 8f), BodyType.Static);
            CircleShape circle = new CircleShape(ConvertUnits.ToSimUnits(30), 0f);
            Fixture fixture = body.CreateFixture(circle);
            body.SetIsSensor(true);
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

        public static void UpdataCollactableItem(World world) 
        {
            for( int i = 0; i < collectedItems.Count; i++)
            {
                world.Remove(collectedItems[i]);
            }

            collectedItems.Clear();
        }

        public static void CreateCorrectableItem(World world)
        {
            new CollectableItem(world);
        }
    }
}
