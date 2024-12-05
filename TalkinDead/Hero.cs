using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkinDead.Animation;
using TalkinDead.Input;
using TalkinDead.Interfaces;

namespace TalkinDead
{
    internal class Hero:IGameObject
    {

        Texture2D heroTexture;
        Animatie animatie;
        private Vector2 positie;
        private Vector2 snelheid;
        IInputReader inputReader;





        public Hero(Texture2D texture, IInputReader reader) 
        {

            heroTexture = texture;
            animatie = new Animatie();
            //animatie.GetFramesFromRow(256, 192,6 ,1, 8); Doesn't work with sprite sheets with variable amount of frames per row?

            
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(32, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(64, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(96, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(128, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(160, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(192, 32, 32, 32)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(224, 32, 32, 32)));

            positie = new Vector2(100, 50);
            snelheid = new Vector2(1, 1);


            // Read input for hero class
            this.inputReader = reader;

        }

        public void update(GameTime gameTime) 
        {

            Move();
            animatie.Update(gameTime);
            
        }

        public void draw(SpriteBatch spriteBatch)
        {

            
            spriteBatch.Draw(heroTexture, positie, animatie.CurrentFrame.SourceRectangle , Color.White);
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            direction *= snelheid;
            positie += direction;


        }


    }
}
