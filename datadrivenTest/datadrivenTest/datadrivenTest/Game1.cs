﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using datadrivenTest.GameOctopus;
using datadrivenTest.GameOctopus.DrawClasss;
using datadrivenTest.GameOctopus.ObjectClasss;

namespace datadrivenTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BasicEffect effect;

        public static int WINDOW_SIZE_X = 306;
        public static int WINDOW_SIZE_Y = 199;

        public static float gameTime;

        SoundManager soundManager;
        Stage stage;
        DrawStage drawGame;
                
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var stageSizeData = My.CsvControler.ReadCSV("config/stage.csv");
            WINDOW_SIZE_X = 0;
            foreach (var item in stageSizeData)
            {
                if (WINDOW_SIZE_X < int.Parse(item.Value["size"]))
                    WINDOW_SIZE_X = int.Parse(item.Value["size"]);
            }
            WINDOW_SIZE_X *= 306;
            _graphics.PreferredBackBufferWidth = WINDOW_SIZE_X;
            _graphics.PreferredBackBufferHeight = WINDOW_SIZE_Y;

            My.FileDataUpdater.Instance.OnUpdate(@"config");
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            MyXNA.InputManager.Initialize();
            My.FileDataUpdater.Instance.UpdateAction.Clear();

            if (stage != null)
                stage = stage.StageSelect();
            else
                stage = new Stage(Initialize);
            drawGame = new DrawStage(Content, stage);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;
            soundManager = new SoundManager(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            Game1.gameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here7
            MyXNA.InputManager.Update();

            stage.Update(gameTime);
            if (stage.Gameover)
                Exit();

            drawGame.Update(new My.BoneMatrix(), true);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            Game1.gameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            _spriteBatch.Begin();

            drawGame.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
