using System;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using LiteGrinder;
using System.Diagnostics;
using LiteGrinder.Object;
using LiteGrinder.MapObject;

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
        
        private int currentLevel = 1;
        public int CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                currentLevel = value;
                loadLevel = true;
            }
        }
        private Vector2 jetDirection;

        private bool loadLevel = false;
        private bool gameisproceeding = false;
        private bool cameraFollow = false;

        private World world;
        private Player player;

        private Vector2 oldMousePos, mousePos;
        private KeyboardState keyState, oldKeyState;
        private MouseState mouseState, oldMouseState;

        private UserInterface ui;

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
            world.Tag = this;

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

            // Initialize UserInterface
            ui = new UserInterface(Content);

            pixel = Content.Load<Texture2D>("1pixel");
            oldLineSprite = Content.Load<Texture2D>("1pixtransparent");

            //Object Initializations
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

            base.Update(gameTime);
        }

        private void HandleControls(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if(loadLevel)
            {
                loadLevel = false;
                LoadLevel(CurrentLevel);
            }

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
                LoadLevel(CurrentLevel);
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
            if (keyState.IsKeyDown(Keys.S) && oldKeyState.IsKeyUp(Keys.S))
            {
                LoadLevel(CurrentLevel);
            }

            if (keyState.IsKeyDown(Keys.D1) && oldKeyState.IsKeyUp(Keys.D1))
            {
                LoadLevel(1);
            }
            if (keyState.IsKeyDown(Keys.D2) && oldKeyState.IsKeyUp(Keys.D2))
            {
                LoadLevel(2);
            }
            if (keyState.IsKeyDown(Keys.D3) && oldKeyState.IsKeyUp(Keys.D3))
            {
                LoadLevel(3);
            }
            if (keyState.IsKeyDown(Keys.D4) && oldKeyState.IsKeyUp(Keys.D4))
            {
                LoadLevel(4);
            }

            // Drawing
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
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
                cam.Pos += new Vector2(-4f,0);
            if (keyState.IsKeyDown(Keys.Right))
                cam.Pos += new Vector2(4f, 0);
            if (keyState.IsKeyDown(Keys.Up))
                cam.Pos += new Vector2(0f, -4f);
            if (keyState.IsKeyDown(Keys.Down))
                cam.Pos += new Vector2(0f, 4f);
            
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

        private void ResetScene()
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

        private void LoadLevel(int levelNum)
        {
            foreach (MapObject o in mapobjects)
            {
                o.Delete(world);
            }

            ResetScene();

            switch (levelNum)
            {
                case 1:
                    createStage.DemoStage1(world);
                    break;
                case 2:
                    createStage.DemoStage2(world);
                    break;
                case 3:
                    createStage.DemoStage3(world);
                    break;
                case 4:
                    createStage.DemoStage4(world);
                    break;
                default:
                    createStage.DemoStage1(world);
                    break;
            }
        }
        private void MouseDrawing(MouseState mouseState, MouseState oldMouseState)
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

            if (length <= 0)
            {
                length = 1;
            }

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
            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);

            player.Draw(spriteBatch, gameTime);

            Line.Draw(spriteBatch);

            debuginfo.RenderDebugData(projection, cam.get_transformation2(GraphicsDevice));
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
