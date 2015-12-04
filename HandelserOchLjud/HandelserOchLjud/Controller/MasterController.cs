using HandelserOchLjud.Model;
using HandelserOchLjud.View;
using HandelserOchLjud.View.Explosion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HandelserOchLjud.Controller
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MasterController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameView gameView;
        ExplosionSystem explosionSystem;
        BallSimulation ballSimulation;
        Camera camera = new Camera();
        MouseState lastMouseState;

        List<ExplosionView> explosions = new List<ExplosionView>();
        List<SmokeSystem> smokes = new List<SmokeSystem>();
        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 900;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
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
            ballSimulation = new BallSimulation();
            gameView = new GameView(graphics, ballSimulation, Content, camera);
            explosionSystem = new ExplosionSystem(camera, Content);
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
                Vector2 logicalLocation = camera.convertMousePosToLogicalCoords(new Vector2(mouseState.X, mouseState.Y));
                if (logicalLocation.X <= 1f && logicalLocation.X >= 0f && logicalLocation.Y <= 1f && logicalLocation.Y >= 0f)
                {
                    explosions.Add(explosionSystem.newExplosion(spriteBatch, logicalLocation, 0.5f));
                    ballSimulation.setDeadBalls(logicalLocation.X, logicalLocation.Y, gameView.CrosshairSize / 2);
                    foreach (Ball ball in ballSimulation.RecentlyKilledBalls)
                    {
                        smokes.Add(explosionSystem.newDeadBallSmoke(ball));
                    }
                }
            }
            lastMouseState = mouseState;
            // TODO: Add your update logic here
            foreach(ExplosionView explosion in explosions)
            {
                explosion.UpdateExplosion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
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
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            foreach(SmokeSystem smokeSystem in smokes)
            {
                smokeSystem.Draw(spriteBatch, camera);
            }
            foreach(ExplosionView explosion in explosions)
            {
                explosion.DrawExplosion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            gameView.Draw(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds, smokes);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
