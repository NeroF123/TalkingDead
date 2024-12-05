using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkinDead.Input
{
    internal class KeyBoardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            var direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            // Horizontal movement (Left/Right)
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }
            else if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                direction.X = -1;
            }

            // Vertical movement (Up/Down)
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                direction.Y = -1;
            }
            else if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                direction.Y = 1;
            }

            // Normalize the direction to prevent faster diagonal movement 
            if (direction != Vector2.Zero)
            {
                direction.Normalize(); // Ensures diagonal speed is the same as horizontal/vertical
            }

            return direction;
        }
    }
}
