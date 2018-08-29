using System;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using LiteGrinder;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace PhysicsTesting
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Control variables
        private float maxLength = 3000;
        private float jumpForce = -18;
        private bool gameisproceeding = false;
        private bool cameraFollow = false;

        //deletesencer
        private Body delete;
        private Fixture deleteSensor;
        private List<Body> removeBodies = new List<Body>();

        private World world;
        private List<Body> boxes = new List<Body>();
        private List<Line> lines =  new List<Line>();
        private Player player;
        private List<demoLevelOne> obstacles = new List<demoLevelOne>();

        private Vector2 oldMousePos, mousePos;
<<<<<<< HEAD
        private KeyboardState keystate, oldKeyState;
        private MouseState mouseState, oldMouseState;

        private Matrix projection;

        // Simple camera controls
        private Matrix view;
        private Vector2 cameraPosition;
        private Vector2 screenCenter;
        private Vector2 startPos = ConvertUnits.ToSimUnits(new Vector2(50,50));
=======
        private Vector2 oldCamPos;
        private KeyboardState _oldKeyState;
>>>>>>> master
        private float totallength = 0;

        private DebugView debuginfo;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D pixel;
        private SpriteFont font;
        private Camera2d cam;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // Screen Resolution
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("DiagnosticsFont");

            world = new World(Vector2.UnitY * 9.82f);

            // Debug init
            debuginfo = new DebugView(world);
            debuginfo.LoadContent(graphics.GraphicsDevice, this.Content);
            debuginfo.AppendFlags(DebugViewFlags.Shape);
            debuginfo.RemoveFlags(DebugViewFlags.Controllers);

            // Initialize camera controls
            cam = new Camera2d(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f));

            projection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
            ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("1pixel");

            //Object Initializations
            player = new Player(world, ConvertUnits.ToSimUnits(new Vector2(50, 50)), pixel, jumpForce);
            demoLevelOne.CreateTestStage(obstacles, world, pixel);
            CollectableItem.CreateCorrectableItem(world);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            HandleControls(gameTime);
            CollectableItem.UpdataCollactableItem(world);

            if (cameraFollow)
                cam.Pos = ConvertUnits.ToDisplayUnits(player.GetBody().Position);

            if (gameisproceeding)
                world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, 1 / 30f));
            else
                world.Step(0);

            for (int i = 0; i < removeBodies.Count; i++)
            {
                world.Remove(removeBodies[i]);
            }

            removeBodies.Clear();

            base.Update(gameTime);
        }

        private void HandleControls(GameTime gameTime)
        {
            keystate = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (mousePos == Vector2.Zero)
            {
                mousePos = new Vector2(mouseState.X, mouseState.Y);
                oldMousePos = mousePos;
            }

            if (keystate.IsKeyDown(Keys.Escape))
                Exit();

            // Reset scene
            if (keystate.IsKeyDown(Keys.R))
            {
                cam.Reset();
                gameisproceeding = false;
                CollectableItem.CreateCorrectableItem(world);
                player.ResetPosition();
                totallength = 0;
                if (boxes.Count > 0)
                {
                    foreach (Body b in boxes)
                    {
                        world.Remove(b);
                    }
                    boxes.Clear();
                    lines.Clear();
                }
            }

            if (keystate.IsKeyDown(Keys.W) && oldKeyState.IsKeyUp(Keys.W))
            {
                gameisproceeding = (gameisproceeding ? false : true);
            }

<<<<<<< HEAD
            jumpCD += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keystate.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space) && isGrounded && (jumpCD >= 500))
=======
            // Jump when you press space
            player.jumpCD += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (state.IsKeyDown(Keys.Space) && _oldKeyState.IsKeyUp(Keys.Space))
>>>>>>> master
            {
                player.Jump();
            }

            if (keystate.IsKeyDown(Keys.O))
                circle.ApplyTorque(-10);

            if (keystate.IsKeyDown(Keys.P))
                circle.ApplyTorque(10);

            // Reset just player circle
            if (keystate.IsKeyDown(Keys.S))
            {
                player.ResetPosition();
            }

            // Drawing
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                MouseDrawing(mouseState, oldMouseState);
            }

            if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                delete = world.CreateCircle(ConvertUnits.ToSimUnits(30), 10f, ConvertUnits.ToSimUnits(mousePos), BodyType.Static);
                deleteSensor = delete.CreateCircle(ConvertUnits.ToSimUnits(30), 10f);
                deleteSensor.IsSensor = true;

                deleteSensor.OnCollision += (fixtureA, fixtureB, contact) =>
                {
                    Body body2 = fixtureB.Body;
                    if (!removeBodies.Contains(body2))
                        removeBodies.Add(body2);
                    return true;
                };
                for (int i = 0; i < removeBodies.Count; i++)
                {
                    world.Remove(removeBodies[i]);
                }
                removeBodies.Clear();
            }

            if (mouseState.RightButton == ButtonState.Released)
            {
                if (world.BodyList.Contains(delete))
                {
                    world.Remove(delete);
                }
            }

            if (mouseState.LeftButton == ButtonState.Released || mouseState.RightButton == ButtonState.Released)
            {
                mousePos = new Vector2(mouseState.X, mouseState.Y);
            }

            // Move camera - Only moving the sprites ATM
<<<<<<< HEAD
            if (keystate.IsKeyDown(Keys.Left))
                cameraPosition.X += 1.5f;
            if (keystate.IsKeyDown(Keys.Right))
                cameraPosition.X -= 1.5f;
            if (keystate.IsKeyDown(Keys.Up))
                cameraPosition.Y += 1.5f;
            if (keystate.IsKeyDown(Keys.Down))
                cameraPosition.Y -= 1.5f;
            view = Matrix.CreateTranslation(new Vector3(cameraPosition -  screenCenter, 0f)) * Matrix.CreateTranslation(new Vector3(screenCenter, 0f));

            oldKeyState = keystate;
            oldMouseState = mouseState;
=======
            if (state.IsKeyDown(Keys.Left))
                cam.Pos += new Vector2(-4f,0);
            if (state.IsKeyDown(Keys.Right))
                cam.Pos += new Vector2(4f, 0);
            if (state.IsKeyDown(Keys.Up))
                cam.Pos += new Vector2(0f, -4f);
            if (state.IsKeyDown(Keys.Down))
                cam.Pos += new Vector2(0f, 4f);
>>>>>>> master
        }

        private void MouseDrawing(MouseState mouseState ,MouseState oldMousState)
        {
            oldMousePos = new Vector2(oldMouseState.X, oldMouseState.Y);
            mousePos = new Vector2(mouseState.X, mouseState.Y);
            var camOffset = (cam.Pos - new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f));

            float angle = (float)Math.Atan2(mousePos.Y - oldMousePos.Y, mousePos.X - oldMousePos.X);
            float length = Vector2.Distance(oldMousePos, mousePos);
            float width = 6;

            if(totallength > maxLength)
            {
                return;
            }

            if( length > 0)
            {   
                Vector2 origin = new Vector2(length / 2, width / 2);

                Body box = world.CreateRectangle(ConvertUnits.ToSimUnits(length),
                  ConvertUnits.ToSimUnits(width), 10f, oldMousePos, angle);

                box.BodyType = BodyType.Kinematic;
                box.SetRestitution(0f);
                box.SetFriction(1f);

                float neg1 = 1;
                float neg2 = 1;
                neg1 = (mousePos.X - oldMousePos.X < 0) ? -1 : 1;
                neg2 = (mousePos.Y - oldMousePos.Y < 0) ? -1 : 1;

                Vector2 boxPos = ConvertUnits.ToSimUnits(oldMousePos + ((mousePos - oldMousePos) / 2));
                box.Position = boxPos - ConvertUnits.ToSimUnits(new Vector2(neg2 * (width / 2), neg1 * (-(width / 2))));
                box.Position += ConvertUnits.ToSimUnits(camOffset);

                boxes.Add(box);

                Line newLine = new Line(pixel, oldMousePos + camOffset, mousePos, width, length, angle, Color.White, origin);
                lines.Add(newLine);

                totallength += length;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.get_transformation(GraphicsDevice));
            player.Draw(spriteBatch);

            foreach (demoLevelOne obstacle in obstacles)
            {
                spriteBatch.Draw(pixel, ConvertUnits.ToDisplayUnits(obstacle.body.Position), null, Color.Green, 0, obstacle.Origin, new Vector2(obstacle.width, obstacle.height), SpriteEffects.None, 0f);
            }

            foreach (Line line in lines)
            {
                line.Draw(spriteBatch);
            }

<<<<<<< HEAD
            debuginfo.RenderDebugData(ref projection);
=======
            var projection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
             ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);
            debuginfo.RenderDebugData(projection, cam.get_transformation2(GraphicsDevice));
>>>>>>> master
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
