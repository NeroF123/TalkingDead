using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkinDead.Animation;
using TalkinDead.Interfaces;

namespace TalkinDead
{
    internal class Hero:IGameObject
    {

        Texture2D heroTexture;
        Animatie animatie;
        private double secondCounter = 0;
        private Vector2 positie = new Vector2(100, 50);
        private Vector2 snelheid = new Vector2(1, 1);


        


        public Hero(Texture2D texture) 
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
        }

        public void update(GameTime gameTime) 
        {
            animatie.Update(gameTime);
            Move();
        }

        public void draw(SpriteBatch spriteBatch)
        {

            
            spriteBatch.Draw(heroTexture, positie, animatie.CurrentFrame.SourceRectangle , Color.White);
        }

        private void Move()
        {
            positie += snelheid;
            if (positie.X > 800 - 32
                || positie.X < 0)
            {
                snelheid.X *= -1;
            }
            if (positie.Y > 480 - 32
                || positie.Y < 0)
            {
                snelheid.Y *= -1;
            }

        }


    }
}
