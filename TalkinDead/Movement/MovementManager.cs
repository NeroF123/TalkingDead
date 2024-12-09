using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkinDead.Input;

namespace TalkinDead.Movement
{
    internal class MovementManager
    {

        private IInputReader inputReader;
        private Vector2 position;
        private Vector2 speed;
        private bool isFacingRight;

        public MovementManager(IInputReader inputReader, Vector2 initialPosition)
        {
            this.inputReader = inputReader;
            this.position = initialPosition;
            this.speed = Vector2.Zero;
            this.isFacingRight = true;
        }

        public void UpdateMovement(GameTime gameTime)
        {
            var direction = inputReader.ReadInput();

            // Determine facing direction
            if (direction.X > 0)
                isFacingRight = true;
            else if (direction.X < 0)
                isFacingRight = false;

           
            if (direction.X == 0 && direction.Y == 0)
            {
                speed = Vector2.Zero; // Idle
            }
            else
            {
                speed = direction; // Update speed based on input
            }

            position += speed;
        }

        public Vector2 GetPosition() => position;

        public bool IsFacingRight() => isFacingRight;

        public Vector2 GetSpeed() => speed;



    }
}
