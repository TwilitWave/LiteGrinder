﻿using System;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using LiteGrinder;
using System.Diagnostics;
using LiteGrinder.Object;

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
        private bool cameraFollow = true;

        private World world;
        private List<Body> boxes = new List<Body>();
        private List<Line> lines =  new List<Line>();
        private Player player;

        private Vector2 oldMousePos, mousePos;
        private KeyboardState keyState, oldKeyState;
        private MouseState mouseState, oldMouseState;

        private float totallength = 0;

        private DebugView debuginfo;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Matrix projection;

        private Texture2D pixel;
        private SpriteFont font;
        private Camera2d cam;

        //Map objects
        private List<LiteGrinder.Object.Object> objects = new List<LiteGrinder.Object.Object>();
        private CollectableItem collectableitem = new CollectableItem();
        private Block block = new Block();

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

            //Something about Projection
            projection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
            ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);

            // Initialize camera controls
            cam = new Camera2d(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f));

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("1pixel");

            //Object Initializations
            player = new Player(world, ConvertUnits.ToSimUnits(new Vector2(50, 50)), Content.Load<Texture2D>("Lab_Dude_2048"), jumpForce);

            InitialMap();
        }

        private void InitialMap()
        {
            demoLevelOne.CreateTestStage(world, pixel);
            objects.Add(collectableitem);
            objects.Add(block);
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
            foreach (LiteGrinder.Object.Object o in objects)
            {
                o.Update(world);
            }

            if (cameraFollow)
                cam.Pos = ConvertUnits.ToDisplayUnits(player.GetBody().Position);

            if (gameisproceeding)
                world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, 1 / 30f));
            else
                world.Step(0);

            base.Update(gameTime);
        }

        private void HandleControls(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (mousePos == Vector2.Zero)
            {
                mousePos = new Vector2(mouseState.X, mouseState.Y);
                oldMousePos = mousePos;
            }

            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            // Reset scene
            if (keyState.IsKeyDown(Keys.R))
            {
                cam.Reset();
                gameisproceeding = false;
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

            if (keyState.IsKeyDown(Keys.W) && oldKeyState.IsKeyUp(Keys.W))
            {
                gameisproceeding = (gameisproceeding ? false : true);
            }

            // Jump when you press space
            player.jumpCD += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keyState.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space))
            {
                player.Jump();
            }

            // Reset just player circle
            if (keyState.IsKeyDown(Keys.S))
            {
                player.ResetPosition();
            }

            // Drawing
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                MouseDrawing(mouseState,oldMouseState);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
            }

            if (mouseState.LeftButton == ButtonState.Released || mouseState.RightButton == ButtonState.Released)
            {
               mousePos = new Vector2(0, 0);
            }

            // Move camera - Only moving the sprites ATM
            if (keyState.IsKeyDown(Keys.Left))
                cam.Pos += new Vector2(-4f,0);
            if (keyState.IsKeyDown(Keys.Right))
                cam.Pos += new Vector2(4f, 0);
            if (keyState.IsKeyDown(Keys.Up))
                cam.Pos += new Vector2(0f, -4f);
            if (keyState.IsKeyDown(Keys.Down))
                cam.Pos += new Vector2(0f, 4f);

            // memorize the state of mouse and keyboard 1 update before
            oldKeyState = keyState;
            oldMouseState = mouseState;
        }

        private void MouseDrawing(MouseState mouseState, MouseState oldMouseState)
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

                box.BodyType = BodyType.Static;
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

            foreach (Line line in lines)
            {
                line.Draw(spriteBatch);
            }

            foreach (LiteGrinder.Object.Object o in objects){
                o.Draw(spriteBatch, pixel);
            }

            debuginfo.RenderDebugData(projection, cam.get_transformation2(GraphicsDevice));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
