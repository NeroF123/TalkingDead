using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkinDead.Animation
{
    internal class Animatie
    {
        public AnimationFrame CurrentFrame { get; set; }

        private List<AnimationFrame> frames;

        private int counter;
        private double secondCounter = 0;

        public Animatie()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFramesFromRow(int rowIndex, int numberOfFramesInRow, int frameWidth, int frameHeight)
        {
       
            // Calculate the Y-coordinate of the row
            int y = rowIndex * frameHeight;

            // Loop through the number of frames in the row
            for (int i = 0; i < numberOfFramesInRow; i++)
            {
                // Create a rectangle for each frame based on its position in the sprite sheet
                Rectangle frameRect = new Rectangle(i * frameWidth, y, frameWidth, frameHeight);
                frames.Add(new AnimationFrame(frameRect));
            }

            // Set the current frame to the first frame
            if (frames.Count > 0)
                CurrentFrame = frames[0];
        }


        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 15;
            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }


        }
    }
}
