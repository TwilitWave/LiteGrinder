using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder.Object
{
    class demoLevelTwo
    {
        public Body body;
        public Vector2 Origin;
        public float width, height;

        public demoLevelTwo(World world, Vector2 position, float width, float height, float density, float rotation, BodyType bodytype, Texture2D texture2d, List<demoLevelTwo> obstacles)
        {
            this.Origin = new Vector2(texture2d.Width / 2f, texture2d.Height / 2f);
            this.body = world.CreateRectangle(width, height, density, position, rotation, bodytype);
            this.width = ConvertUnits.ToDisplayUnits(width);
            this.height = ConvertUnits.ToDisplayUnits(height);
            body.SetRestitution(0.2f);
            body.SetFriction(0.5f);
            obstacles.Add(this);
        }

        public static void CreateTestStage(List<demoLevelOne> obstacles, World world, Texture2D texture2d)
        {
            new demoLevelOne(world, new Vector2(0f, 3f), ConvertUnits.ToSimUnits(300), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, texture2d, obstacles);
            new demoLevelOne(world, new Vector2(4f, 1f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(800), 1f, 0, BodyType.Static, texture2d, obstacles);
            new demoLevelOne(world, new Vector2(10f, 4f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, texture2d, obstacles);
            new demoLevelOne(world, new Vector2(10f, 7f), ConvertUnits.ToSimUnits(500), ConvertUnits.ToSimUnits(100), 1f, 0, BodyType.Static, texture2d, obstacles);
            new demoLevelOne(world, new Vector2(15f, 0f), ConvertUnits.ToSimUnits(600), ConvertUnits.ToSimUnits(600), 1f, 0, BodyType.Static, texture2d, obstacles);
            new demoLevelOne(world, new Vector2(15f, 10f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(500), 1f, 0, BodyType.Static, texture2d, obstacles);
            new demoLevelOne(world, new Vector2(19f, 6f), ConvertUnits.ToSimUnits(150), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, texture2d, obstacles);
        }
    }
}
