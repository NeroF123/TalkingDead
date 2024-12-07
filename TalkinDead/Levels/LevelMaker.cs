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
        
        private int dTilesize;
        private int tilesRow;
        private int tilesPixelSize;

        public  LevelMaker(string filepathBg, string filepathMg, string filepathFg, Texture2D textureAtlas, int dTilesize, int tilesRow, int tilesPixelSize) // Get's all info stores it and send csv file to loadmap to be read/stored
        {
            this.textureAtlas = textureAtlas;
            this.dTilesize = dTilesize;
            this.tilesRow = tilesRow;
            this.tilesPixelSize = tilesPixelSize;

            bgMap = LoadMap(filepathBg);
            mgMap = LoadMap(filepathMg);
            fgMap = LoadMap(filepathFg);
        }

        private Dictionary<Vector2, int> LoadMap(string filepath) // Reads , parses and then stores the map as a dictionary (csv file)
        {

            Dictionary<Vector2, int> result = new();
            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(",");

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }

                }

                y++;

            }

            return result;

        }

        public void Draw(SpriteBatch spriteBatch) //Send bg then mg then fg to be drawn in order
        {
            DrawLayer(spriteBatch, bgMap);
            DrawLayer(spriteBatch, mgMap);
            DrawLayer(spriteBatch, fgMap);
        }

        private void DrawLayer(SpriteBatch spriteBatch, Dictionary<Vector2, int> map) //Draws level using drect(= where tile goes) and src(= what tile to use)
        {
            foreach (var item in map)
            {
                Rectangle drect = new((int)item.Key.X * dTilesize, (int)item.Key.Y * dTilesize, dTilesize, dTilesize);

                int x = item.Value % tilesRow;
                int y = item.Value / tilesRow;

                Rectangle src = new(x * tilesPixelSize, y * tilesPixelSize, tilesPixelSize, tilesPixelSize);
                spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }
        }

    }
}
