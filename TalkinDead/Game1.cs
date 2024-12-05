using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TalkinDead
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Rectangle _deelRectangle;
        private int schuifOp_X = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
 

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _deelRectangle = new Rectangle(schuifOp_X, 32, 32, 32);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _texture = Content.Load<Texture2D>("Ratfolk Mage Sprite Sheet");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            // Teken hier alles uit de spritebatch
            _spriteBatch.Draw(_texture, new Vector2(0, 0), _deelRectangle, Color.White);
            _spriteBatch.End();

            schuifOp_X += 32;
            if (schuifOp_X > 256)
            {
                schuifOp_X = 0;
            }
            _deelRectangle.X = schuifOp_X;


            base.Draw(gameTime);
        }
    }
}
