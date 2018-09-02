using System;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using LiteGrinder;
using System.Diagnostics;
<<<<<<< HEAD
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics.Joints;
=======
using LiteGrinder.Object;
using LiteGrinder.MapObject;
>>>>>>> master

namespace LyteGrinder
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    { 
        //Control variables
        public static float maxLength = 3000;
        public static float totalLength = 0;
        public static float jumpForce = -18;
        public static int numberofJet = 3;
        private Vector2 jetDirection;

        private bool gameisproceeding = false;
        private bool cameraFollow = false;

        //deletesencer
        private Body delete;
        private Fixture deleteSensor;
        private List<Body> removeBodies = new List<Body>();

        private World world;
        private Player player;

        private Vector2 oldMousePos, mousePos;
<<<<<<< HEAD
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
=======
        private KeyboardState keyState, oldKeyState;
        private MouseState mouseState, oldMouseState;

        private UserInterface ui;
>>>>>>> master

        private DebugView debuginfo;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Matrix projection;

        private demoLevelOne createStage;
        private Texture2D pixel;
        private Texture2D oldLineSprite;
        private Texture2D background;
        private Camera2d cam;
        
        //Map objects
        private List<LiteGrinder.Object.MapObject> mapobjects = new List<LiteGrinder.Object.MapObject>();
        private CollectableItem collectableitem = new CollectableItem();
        private Block block = new Block();
        private NoDrawArea noDraw = new NoDrawArea();
        private Vector2 oldCamPos;

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
            world = new World(Vector2.UnitY * 9.82f);

            // Debug init
            debuginfo = new DebugView(world);
            debuginfo.LoadContent(graphics.GraphicsDevice, this.Content);
            debuginfo.AppendFlags(DebugViewFlags.Shape);
            debuginfo.RemoveFlags(DebugViewFlags.Controllers);

            //Something about Projection
            projection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
            ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);

            // Initialize camera controls
            cam = new Camera2d(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f));

            projection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
            ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize UserInterface
            ui = new UserInterface(Content);

            pixel = Content.Load<Texture2D>("1pixel");
            oldLineSprite = Content.Load<Texture2D>("1pixtransparent");

            //Object Initializations
<<<<<<< HEAD
            player = new Player(world, ConvertUnits.ToSimUnits(new Vector2(50, 50)), pixel, jumpForce);
            demoLevelOne.CreateTestStage(obstacles, world, pixel);
            CollectableItem.CreateCorrectableItem(world);
=======
            player = new Player(world, ConvertUnits.ToSimUnits(new Vector2(50, 50)), Content.Load<Texture2D>("Lab_Hamster 1")
                , Content.Load<Texture2D>("Lab_Hamster 2"), Content.Load<Texture2D>("Ball"), Content.Load<Texture2D>("portal_start"), Content.Load<Texture2D>("portal_goal"), jumpForce);
            background = Content.Load<Texture2D>("labBackground");
            createStage = new demoLevelOne(this.Content, player);
            InitialMap();
        }

        private void InitialMap()
        {
            createStage.DemoStage1(world);
            mapobjects.Add(collectableitem);
            mapobjects.Add(block);
            mapobjects.Add(noDraw);
>>>>>>> master
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
            foreach (LiteGrinder.Object.MapObject mapobject in mapobjects)
            {
                mapobject.Update(world);
            }

            //update UserInterface values
            ui.Update();

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
<<<<<<< HEAD
            keystate = Keyboard.GetState();
=======
            keyState = Keyboard.GetState();
>>>>>>> master
            mouseState = Mouse.GetState();

            if (mousePos == Vector2.Zero)
            {
                mousePos = new Vector2(mouseState.X, mouseState.Y);
                oldMousePos = mousePos;
            }

<<<<<<< HEAD
            if (keystate.IsKeyDown(Keys.Escape))
                Exit();

            // Reset scene
            if (keystate.IsKeyDown(Keys.R))
=======
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            // Reset scene
            if (keyState.IsKeyDown(Keys.R))
>>>>>>> master
            {
                cam.Reset();
                gameisproceeding = false;
                player.ResetPosition();
                totalLength = 0;
                Line.Reset(world);

                JetArea deleteJet = new JetArea();
                deleteJet.Delete(world);
                numberofJet = 3;
            }

<<<<<<< HEAD
            if (keystate.IsKeyDown(Keys.W) && oldKeyState.IsKeyUp(Keys.W))
=======
            if (keyState.IsKeyDown(Keys.W) && oldKeyState.IsKeyUp(Keys.W))
>>>>>>> master
            {
                gameisproceeding = (gameisproceeding ? false : true);
            }

<<<<<<< HEAD
            jumpCD += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keystate.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space) && isGrounded && (jumpCD >= 500))
=======
            // Jump when you press space
            player.jumpCD += gameTime.ElapsedGameTime.TotalMilliseconds;
<<<<<<< HEAD
            if (state.IsKeyDown(Keys.Space) && _oldKeyState.IsKeyUp(Keys.Space))
>>>>>>> master
=======
            if (keyState.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space))
>>>>>>> master
            {
                player.Jump();
            }

<<<<<<< HEAD
            if (keystate.IsKeyDown(Keys.O))
                circle.ApplyTorque(-10);

            if (keystate.IsKeyDown(Keys.P))
                circle.ApplyTorque(10);

            // Reset just player circle
            if (keystate.IsKeyDown(Keys.S))
=======
            // Reset just player circle
            if (keyState.IsKeyDown(Keys.S))
>>>>>>> master
            {
                player.ResetPosition();
            }

            if (keyState.IsKeyDown(Keys.D1) && oldKeyState.IsKeyUp(Keys.D1))
            {
                foreach(MapObject o in mapobjects)
                {
                    o.Delete(world);
                }
                createStage.DemoStage1(world);
            }
            if (keyState.IsKeyDown(Keys.D2) && oldKeyState.IsKeyUp(Keys.D2))
            {
                foreach (MapObject o in mapobjects)
                {
                    o.Delete(world);
                }
                createStage.DemoStage2(world);
            }
            if (keyState.IsKeyDown(Keys.D3) && oldKeyState.IsKeyUp(Keys.D3))
            {
                foreach (MapObject o in mapobjects)
                {
                    o.Delete(world);
                }
                createStage.DemoStage3(world);
            }

            // Drawing
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
<<<<<<< HEAD
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
=======
                MouseDrawing(mouseState,oldMouseState);
            }

            // Make a JetArea
            if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                jetDirection = new Vector2(mouseState.X, mouseState.Y);
            }

            if (mouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed)
            {
                if (numberofJet > 0)
                {
                    JetArea jetarea = new JetArea(world, 60, 2f, ConvertUnits.ToSimUnits(jetDirection), BodyType.Static);
                    mapobjects.Add(jetarea);
                    jetDirection = new Vector2(mouseState.X, mouseState.Y) - jetDirection;
                    jetarea.ChangeImpluse(jetDirection);
                    numberofJet--;
                }
                else
                {
                    return;
                }
     
            }

            if (mouseState.LeftButton == ButtonState.Released || mouseState.RightButton == ButtonState.Released)
            {
               mousePos = new Vector2(0, 0);
            }

            if (oldCamPos != cam.Pos)
            {
                oldCamPos = (cam.Pos - new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f));
            }

            // Move camera - Only moving the sprites ATM
            if (keyState.IsKeyDown(Keys.Left))
>>>>>>> master
                cam.Pos += new Vector2(-4f,0);
            if (keyState.IsKeyDown(Keys.Right))
                cam.Pos += new Vector2(4f, 0);
            if (keyState.IsKeyDown(Keys.Up))
                cam.Pos += new Vector2(0f, -4f);
            if (keyState.IsKeyDown(Keys.Down))
                cam.Pos += new Vector2(0f, 4f);
<<<<<<< HEAD
>>>>>>> master
        }

        private void MouseDrawing(MouseState mouseState ,MouseState oldMousState)
=======
            
            if (keyState.IsKeyDown(Keys.Z))
            {
                cam.Zoom += .01f;
            }
            if (keyState.IsKeyDown(Keys.X))
            {
                cam.Zoom -= .01f;
            }

            // memorize the state of mouse and keyboard 1 update before
            oldKeyState = keyState;
            oldMouseState = mouseState;
        }

        private void MouseDrawing(MouseState mouseState, MouseState oldMouseState)
>>>>>>> master
        {
            oldMousePos = new Vector2(oldMouseState.X, oldMouseState.Y);
            mousePos = new Vector2(mouseState.X, mouseState.Y);
            var camOffset = (cam.Pos - new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f));
            var deltaCam = camOffset - oldCamPos;
            oldMousePos += deltaCam;

            float angle = (float)Math.Atan2(mousePos.Y - oldMousePos.Y, mousePos.X - oldMousePos.X);
            float length = Vector2.Distance(oldMousePos, mousePos);
            float width = 6;

            if (totalLength > maxLength)
            {
                return;
            }

<<<<<<< HEAD
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
=======
            if (length <= 0)
            {
                length = 1;
            }
>>>>>>> master

            Line newLine = new Line(world, pixel, oldLineSprite, oldMousePos, mousePos, camOffset, width, length, angle);

            totalLength += length;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.get_transformation(GraphicsDevice));
            
            // Background drawing disabled until we get a tasty snack collectable because it hides the debug info
            //spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);

            player.Draw(spriteBatch, gameTime);

            Line.Draw(spriteBatch);

<<<<<<< HEAD
<<<<<<< HEAD
            debuginfo.RenderDebugData(ref projection);
=======
            var projection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
             ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);
=======
>>>>>>> master
            debuginfo.RenderDebugData(projection, cam.get_transformation2(GraphicsDevice));
>>>>>>> master
            spriteBatch.End();

            
            // Linear wrap drawing for the obstacles
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, cam.get_transformation(GraphicsDevice));
            foreach (LiteGrinder.Object.MapObject o in mapobjects)
            {
                o.Draw(spriteBatch);
            }
            spriteBatch.End();

            ui.Draw(spriteBatch);


            base.Draw(gameTime);
        }
    }
}
