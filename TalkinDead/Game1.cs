using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TalkinDead.Input;

namespace TalkinDead
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture;
        private Texture2D textureAtlas;

        private Dictionary<Vector2, int> fgMap;
        private Dictionary<Vector2, int> mgMap;
        private Dictionary<Vector2, int> bgMap;
        private List<Rectangle> TextureStore;

        Hero hero;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.ApplyChanges();



            fgMap = LoadMap("../../../Levels/level1_fg.csv"); // Foreground layer
            mgMap = LoadMap("../../../Levels/level1_mg.csv"); // Filepath to csv level1 Middle ground
            bgMap = LoadMap("../../../Levels/level1_bg.csv"); // Background layer
            TextureStore = new()
            {
            new Rectangle(0,0,32,32)
            };

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
            textureAtlas = Content.Load<Texture2D>("Dungeon_Tileset");
            InitializeGameObject();
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
            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            int dTilesize = 32; // Display tilesize
            int tilesRow = 16;
            int tilesPixelSize = 32;

            foreach (var item in bgMap)
            {
                Rectangle drect = new((int)item.Key.X * dTilesize, (int)item.Key.Y * dTilesize, dTilesize, dTilesize);

                int x = item.Value % tilesRow;
                int y = item.Value / tilesRow;

                Rectangle src = new(x * tilesPixelSize, y * tilesPixelSize, tilesPixelSize, tilesPixelSize);
                _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }

            foreach (var item in mgMap)
            {
                Rectangle drect = new((int)item.Key.X * dTilesize, (int)item.Key.Y * dTilesize, dTilesize, dTilesize);

                int x = item.Value % tilesRow;
                int y = item.Value / tilesRow;

                Rectangle src = new(x * tilesPixelSize, y * tilesPixelSize, tilesPixelSize, tilesPixelSize);
                _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }

            foreach (var item in fgMap)
            {
                Rectangle drect = new((int)item.Key.X * dTilesize, (int)item.Key.Y * dTilesize, dTilesize, dTilesize);

                int x = item.Value % tilesRow;
                int y = item.Value / tilesRow;   

                Rectangle src = new(x * tilesPixelSize, y * tilesPixelSize, tilesPixelSize, tilesPixelSize);
                _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }

            hero.draw(_spriteBatch);

            _spriteBatch.End();   
            base.Draw(gameTime);    
        }

        private Dictionary<Vector2, int> LoadMap(string filepath)
        {
            
            Dictionary<Vector2, int> result = new() ;
            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(",");

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value)) { 
                        if (value > -1) {
                            result[new Vector2(x, y)] = value;
                        }
                    }

                }

                y++;

            }

            return result ; 

        }



    }
}
