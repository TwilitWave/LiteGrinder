using LiteGrinder.MapObject;
using LiteGrinder.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsTesting;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder
{
    class demoLevelOne
    {
        public static void CreateTestStage(World world, Texture2D texture2d)
        {
            new Block(world, new Vector2(0f, 3f), ConvertUnits.ToSimUnits(300), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, texture2d);
            new Block(world, new Vector2(4f, 1f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(800), 1f, 0, BodyType.Static, texture2d);
            new Block(world, new Vector2(10f, 4f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, texture2d);
            new Block(world, new Vector2(10f, 7f), ConvertUnits.ToSimUnits(500), ConvertUnits.ToSimUnits(100), 1f, 0, BodyType.Static, texture2d);
            new Block(world, new Vector2(15f, 0f), ConvertUnits.ToSimUnits(600), ConvertUnits.ToSimUnits(600), 1f, 0, BodyType.Static, texture2d);
            new Block(world, new Vector2(15f, 10f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(500), 1f, 0, BodyType.Static, texture2d);
            new Block(world, new Vector2(19f, 6f), ConvertUnits.ToSimUnits(150), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, texture2d);

            new CollectableItem(world, 30, 2f, new Vector2(4f, 8f), BodyType.Static);
            new CollectableItem(world, 30, 2f, new Vector2(10f, 3f), BodyType.Static);
            new CollectableItem(world, 30, 2f, new Vector2(15f, 7f), BodyType.Static);

            new JetArea(world, 60, 2f, new Vector2(7f, 6f), BodyType.Static);

            new NoDrawArea(world, new Vector2(10f, 3f), ConvertUnits.ToSimUnits(75), ConvertUnits.ToSimUnits(800), 0, texture2d);
        }
    }
}
