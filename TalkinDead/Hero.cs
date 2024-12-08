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
        Animatie moving;
        Animatie idle;
        private Vector2 positie;
        private Vector2 snelheid;
        IInputReader inputReader;

        private bool isFacingRight = true;



        public Hero(Texture2D texture, IInputReader reader) 
        {

            heroTexture = texture;
            idle = new Animatie();
            moving = new Animatie();



            // Add frames for the idle animation 
            idle.AddFramesFromRow(0, 8, 32, 32);

            // Add frames for the moving animation 
            moving.AddFramesFromRow(1, 8, 32, 32);

            positie = new Vector2(250, 250);
            snelheid = new Vector2(1, 1);


            // Read input for hero class
            this.inputReader = reader;

        }

        public void update(GameTime gameTime) 
        {

            Move();


            // Check if the hero is moving or idle
            if (snelheid.X != 0 || snelheid.Y != 0)
            {
                // Hero is moving
                moving.Update(gameTime);
            }
            else
            {
                // Hero is idle
                idle.Update(gameTime);
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            //System.Diagnostics.Debug.WriteLine($"Facing Right: {isFacingRight}");

            // Choose the current animation based on the hero's movement status
            Animatie currentAnimation = (snelheid.X != 0 || snelheid.Y != 0) ? moving : idle;

            // Determine the sprite effect for flipping horizontally
            SpriteEffects spriteEffect = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            // Draw the correct frame from the current animation
            spriteBatch.Draw(heroTexture, positie, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2.0f, spriteEffect, 0f);

        }

        private void Move()
        {
            var direction = inputReader.ReadInput();

            //System.Diagnostics.Debug.WriteLine($"Direction: {direction}"); 

            if (direction.X > 0)
                isFacingRight = true;
            else if (direction.X < 0)
                isFacingRight = false;

            if (direction.X == 0 && direction.Y == 0)
            {
                snelheid = Vector2.Zero; // Ensure snelheid is zero when idle
            }
            else
            {
                snelheid = direction; // Update speed based on input
            }

            positie += direction;


        }


    }
}
