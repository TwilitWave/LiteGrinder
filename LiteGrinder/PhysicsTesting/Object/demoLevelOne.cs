using LiteGrinder.MapObject;
using LiteGrinder.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PhysicsTesting;
using System;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder
{
    class demoLevelOne
    {
        private ContentManager Content;

        private Texture2D pixel;
        private Texture2D wallTile;
        private Texture2D strawberry;
        private Texture2D noDrawSprite;
        private Player player;

        public demoLevelOne(ContentManager content, Player player)
        {
            this.player = player;
            this.Content = content;
        }

        public void DemoStage1(World world)
        {
            player.SetStartPosition(ConvertUnits.ToSimUnits(new Vector2(50, 50)));
            player.SetGoalPosition(ConvertUnits.ToSimUnits(new Vector2(1870, 520)));

            wallTile = Content.Load<Texture2D>("wallTile");
            new Block(world, new Vector2(0f, 3f), ConvertUnits.ToSimUnits(300), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(4f, 1f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(800), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(10f, 4f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(10f, 7f), ConvertUnits.ToSimUnits(500), ConvertUnits.ToSimUnits(100), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(15f, 0f), ConvertUnits.ToSimUnits(600), ConvertUnits.ToSimUnits(600), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(15f, 10f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(500), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(19f, 6f), ConvertUnits.ToSimUnits(150), ConvertUnits.ToSimUnits(50), 1f, 0, BodyType.Static, wallTile);

            strawberry = Content.Load<Texture2D>("strawberry");
            new CollectableItem(world, 30, 2f, new Vector2(4f, 8f), BodyType.Static, strawberry);
            new CollectableItem(world, 30, 2f, new Vector2(10f, 3f), BodyType.Static, strawberry);
            new CollectableItem(world, 30, 2f, new Vector2(15f, 7f), BodyType.Static, strawberry);

            new JetArea(world, 60, 2f, new Vector2(7f, 6f), BodyType.Static);

            noDrawSprite = Content.Load<Texture2D>("redTransparent");
            new NoDrawArea(world, new Vector2(10f, 3f), ConvertUnits.ToSimUnits(75), ConvertUnits.ToSimUnits(800), 0, noDrawSprite);

            Line.ClearGhostLine();
        }

        public void DemoStage2(World world)
        {
            player.SetStartPosition(ConvertUnits.ToSimUnits(new Vector2(350, 50)));
            player.SetGoalPosition(ConvertUnits.ToSimUnits(new Vector2(1870, 520)));

            wallTile = Content.Load<Texture2D>("wallTile");
            new Block(world, new Vector2(5f, 5f), ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(800), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(13f, 8f), ConvertUnits.ToSimUnits(600), ConvertUnits.ToSimUnits(600), 1f, 0, BodyType.Static, wallTile);

            strawberry = Content.Load<Texture2D>("strawberry");
            new CollectableItem(world, 30, 2f, new Vector2(4f, 8f), BodyType.Static, strawberry);
            new CollectableItem(world, 30, 2f, new Vector2(10f, 3f), BodyType.Static, strawberry);
            new CollectableItem(world, 30, 2f, new Vector2(15f, 7f), BodyType.Static, strawberry);

            new JetArea(world, 60, 2f, new Vector2(7f, 6f), BodyType.Static);

            Line.ClearGhostLine();
        }

        public void DemoStage3(World world)
        {
            player.SetStartPosition(ConvertUnits.ToSimUnits(new Vector2(800, 50)));
            player.SetGoalPosition(ConvertUnits.ToSimUnits(new Vector2(1870, 300)));

            wallTile = Content.Load<Texture2D>("wallTile");
            new Block(world, new Vector2(5f, 5f), ConvertUnits.ToSimUnits(330), ConvertUnits.ToSimUnits(700), 1f, 0, BodyType.Static, wallTile);
            new Block(world, new Vector2(13f, 6f), ConvertUnits.ToSimUnits(600), ConvertUnits.ToSimUnits(500), 1f, 0, BodyType.Static, wallTile);

            strawberry = Content.Load<Texture2D>("strawberry");
            new CollectableItem(world, 30, 2f, new Vector2(4f, 8f), BodyType.Static, strawberry);
            new CollectableItem(world, 30, 2f, new Vector2(10f, 3f), BodyType.Static, strawberry);
            new CollectableItem(world, 30, 2f, new Vector2(15f, 7f), BodyType.Static, strawberry);

            new JetArea(world, 60, 2f, new Vector2(7f, 6f), BodyType.Static);

            Line.ClearGhostLine();
        }
    }
}
