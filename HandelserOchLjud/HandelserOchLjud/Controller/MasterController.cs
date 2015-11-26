using HandelserOchLjud.Model;
using HandelserOchLjud.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HandelserOchLjud.Controller
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MasterController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BallView ballView;
        BallSimulation ballSimulation;
        Camera camera = new Camera();
        MouseState lastMouseState;
        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 900;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            camera.setSizeOfField(graphics.GraphicsDevice.Viewport);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D ballTexture = Content.Load<Texture2D>("aqua-ball.png");
            Texture2D splitterTexture = Content.Load<Texture2D>("Spark3");
            Texture2D splitterSecondTexture = Content.Load<Texture2D>("Spark2");
            Texture2D smokeTexture = Content.Load<Texture2D>("Smoketest");
            Texture2D explosionTexture = Content.Load<Texture2D>("Fixforshittyschoolcomputer");
            Texture2D shockwaveTexture = Content.Load<Texture2D>("Shockwave2");
            SoundEffect fireSound = Content.Load<SoundEffect>("fire");
            
            ballSimulation = new BallSimulation();
            ballView = new BallView(graphics, ballSimulation, ballTexture, camera, splitterTexture, splitterSecondTexture, smokeTexture, explosionTexture, shockwaveTexture, fireSound);

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Mouse click help from http://stackoverflow.com/a/9719528
            var mouseState = Mouse.GetState();
            if (lastMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                ballView.NewExplosion(mouseState.X, mouseState.Y, spriteBatch);
            }
            lastMouseState = mouseState;
            // TODO: Add your update logic here
            ballSimulation.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            ballView.Draw(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Draw(gameTime);
        }
    }
}
