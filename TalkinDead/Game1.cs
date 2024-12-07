using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Xml.Linq;
using TalkinDead.Input;
using TalkinDead.Levels;

namespace TalkinDead
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D texture;

        LevelMaker level1;
        Hero hero;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>("Ratfolk Mage Sprite Sheet");
            InitializeGameObject();
            
            level1 = new LevelMaker(
            "../../../Levels/level1_bg.csv",
            "../../../Levels/level1_mg.csv",
            "../../../Levels/level1_fg.csv",
            Content.Load<Texture2D>("Dungeon_Tileset"),
            32, 16, 32);

        }

        private void InitializeGameObject()
        {
            hero = new Hero(texture, new KeyBoardReader());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            hero.update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            level1.Draw(_spriteBatch);
            hero.draw(_spriteBatch);

            _spriteBatch.End();   
            base.Draw(gameTime);    
        }

    }
}
