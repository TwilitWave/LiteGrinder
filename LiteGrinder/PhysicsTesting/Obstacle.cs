using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder
{
    class Obstacle
    {
        public Body body;
        public Vector2 Origin;
        public float width, height;

        public Obstacle(World world, Vector2 position, float width, float height, float density, float rotation, BodyType bodytype, Texture2D texture2d, List<Obstacle> obstacles)
        {
            this.Origin = new Vector2(texture2d.Width / 2f, texture2d.Height / 2f);
            this.body = world.CreateRectangle(width, height, density, position, rotation, bodytype);
            this.width = ConvertUnits.ToDisplayUnits(width);
            this.height = ConvertUnits.ToDisplayUnits(height);
            body.SetRestitution(0.2f);
            body.SetFriction(0.5f);
            obstacles.Add(this);
        }

        public static void CreateTestStage(List<Obstacle> obstacles, World world, Texture2D texture2d)
        {
            new Obstacle(world, new Vector2(0f, 3f), ConvertUnits.ToSimUnits(130), ConvertUnits.ToSimUnits(100), 1f, 0, BodyType.Static, texture2d, obstacles);
            //new Obstacle(world, new Vector2(2.5f, 1f), ConvertUnits.ToSimUnits(130), ConvertUnits.ToSimUnits(500), 1f, 0, BodyType.Static, texture2d, obstacles);
            new Obstacle(world, new Vector2(6.0f, 3f), ConvertUnits.ToSimUnits(130), ConvertUnits.ToSimUnits(100), 1f, 0, BodyType.Static, texture2d, obstacles);
            new Obstacle(world, new Vector2(7.0f, 5.0f), ConvertUnits.ToSimUnits(500), ConvertUnits.ToSimUnits(100), 1f, 0, BodyType.Static, texture2d, obstacles);
        }
    }
}
