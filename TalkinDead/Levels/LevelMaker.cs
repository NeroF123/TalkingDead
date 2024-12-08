using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkinDead.Levels
{
    internal class LevelMaker
    {

        private Texture2D textureAtlas;
        private Dictionary<Vector2, int> fgMap;
        private Dictionary<Vector2, int> mgMap;
        private Dictionary<Vector2, int> bgMap;
        // Tile properties
        private int dTilesize;
        private int tilesRow;
        private int tilesPixelSize;

        // Get's all info stores it and send csv file to loadmap to be read/stored
        public LevelMaker(string filepathBg, string filepathMg, string filepathFg, Texture2D textureAtlas, int dTilesize, int tilesRow, int tilesPixelSize) 
        {
            this.textureAtlas = textureAtlas;
            this.dTilesize = dTilesize;
            this.tilesRow = tilesRow;
            this.tilesPixelSize = tilesPixelSize;

            bgMap = LoadMap(filepathBg);
            mgMap = LoadMap(filepathMg);
            fgMap = LoadMap(filepathFg);
        }

        // Reads , parses and then stores the map as a dictionary (csv file)
        private Dictionary<Vector2, int> LoadMap(string filepath) 
        {

            // Initialize empty dictionary to store tile data
            Dictionary<Vector2, int> result = new();
            StreamReader reader = new(filepath);

            int y = 0; // Row index
            string line;

            // Read each line from the file
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into an array of tile IDs (separated by commas)
                string[] items = line.Split(",");

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            // Add the tile ID to the dictionary with its position (x, y)
                            result[new Vector2(x, y)] = value;
                        }
                    }

                }

                y++;

            }

            return result;

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            //Send bg then mg then fg to be drawn in order
            DrawLayer(spriteBatch, bgMap);
            DrawLayer(spriteBatch, mgMap);
            DrawLayer(spriteBatch, fgMap);
        }

        private void DrawLayer(SpriteBatch spriteBatch, Dictionary<Vector2, int> map) //Draws level using drect(= where tile goes) and src(= what tile to use)
        {
            // Loop through each tile in the map
            foreach (var item in map)
            {
                // Calculate the destination rectangle where the tile will be drawn on the screen
                Rectangle drect = new((int)item.Key.X * dTilesize, (int)item.Key.Y * dTilesize, dTilesize, dTilesize);

                // Calculate the position of the tile in the texture atlas (sprite sheet)
                int x = item.Value % tilesRow; //Column 
                int y = item.Value / tilesRow; // Row 

                Rectangle src = new(x * tilesPixelSize, y * tilesPixelSize, tilesPixelSize, tilesPixelSize);
                spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }
        }

    }
}
