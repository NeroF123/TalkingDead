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

        /*
        public void GetFramesFromRow(int textureWidth, int textureHeight,int numberOfSpritesInHeight , int rowIndex, int numberOfSpritesInRow)
        {
            frames.Clear(); // Clear existing frames if necessary.

            // Calculate the frame dimensions
            int frameWidth = textureWidth / numberOfSpritesInRow;
            int frameHeight = textureHeight / numberOfSpritesInHeight;

            // Calculate the Y-coordinate of the row
            int y = rowIndex * frameHeight;

            // Loop through the texture width to create frames for the row
            for (int x = 0; x < textureWidth; x += frameWidth)
            {
                // Add each frame from the row
                frames.Add(new AnimationFrame(new Rectangle(x, y, frameWidth, frameHeight)));
            }
        }
        */


        public Animatie()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame animationFrame)
        {
            frames.Add(animationFrame);
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
