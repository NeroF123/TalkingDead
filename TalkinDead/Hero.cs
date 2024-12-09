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
using TalkinDead.Movement;

namespace TalkinDead
{
    internal class Hero:IGameObject
    {

        Texture2D heroTexture;
        Animatie moving;
        Animatie idle;
        IInputReader inputReader;
        private MovementManager movementManager;

        public Hero(Texture2D texture, IInputReader reader) 
        {

            heroTexture = texture;
            idle = new Animatie();
            moving = new Animatie();

            // Add frames for the idle animation 
            idle.AddFramesFromRow(0, 8, 32, 32);

            // Add frames for the moving animation 
            moving.AddFramesFromRow(1, 8, 32, 32);

            movementManager = new MovementManager(reader, new Vector2(250, 250));

        }

        public void update(GameTime gameTime) 
        {

            movementManager.UpdateMovement(gameTime);


            // Check if the hero is moving or idle
            if (movementManager.GetSpeed().X != 0 || movementManager.GetSpeed().Y != 0)
            {
                moving.Update(gameTime);
            }
            else
            {
                idle.Update(gameTime);
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            //System.Diagnostics.Debug.WriteLine($"Facing Right: {isFacingRight}");

            // Choose the current animation based on the hero's movement status
            Animatie currentAnimation = (movementManager.GetSpeed().X != 0 || movementManager.GetSpeed().Y != 0) ? moving : idle;

            // Determine the sprite effect for flipping horizontally
            SpriteEffects spriteEffect = movementManager.IsFacingRight() ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            // Draw the correct frame from the current animation
            spriteBatch.Draw(heroTexture, movementManager.GetPosition(), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, Vector2.Zero, 2.0f, spriteEffect, 0f);

        }
    

    }
}
