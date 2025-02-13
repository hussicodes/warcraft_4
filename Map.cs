using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace warcraft_4
{
    struct Tile
    {
        public Rectangle DrawRectangle; // This is the selected tile image
    }

    public class Map
    {
        public int Size;
        public int TileSize;
        public Tileset Tileset;

        private Rectangle[][] tileGrid;
        private Random r = new Random();

        // Constructor
        public Map(int size, Tileset tileset)
        {
            Size = size;
            TileSize = 32; // Size of tile! If you want to edit, also edit in game1
            Tileset = tileset;

            // Figures out how many tiles that fit horizontally and vertically
            int screenWidth = 1300;  // sets scren width, remember to adjust in game1 too, or you will get a Nintendo DS experience
            int screenHeight = 600; // sets screen height, remember to adjust in game1 too, or you will get a Nintendo DS experience

            // The map will fill the screen, so calculate the number of tiles
            int tilesX = screenWidth / TileSize;
            int tilesY = screenHeight / TileSize;

            Size = Math.Max(tilesX, tilesY); // Adjust size

            // Initialize the grid with random tiles, but always the same tiles
            tileGrid = new Rectangle[Size][];
            for (int i = 0; i < Size; i++)
            {
                tileGrid[i] = new Rectangle[Size];
                for (int j = 0; j < Size; j++)
                {
                    // Pick a random tile from the Tileset
                    var tile = Tileset.TileRegions[r.Next(Tileset.TileRegions.Count)];
                    tileGrid[i][j] = tile;
                }
            }
        }

        // Draw method to draw the map to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            // Loop through the grid and draw each tile
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // Used to find where the next Rectangle will be drawn
                    Rectangle destinationRectangle = new Rectangle(i * TileSize, j * TileSize, TileSize, TileSize);

                    // Draw the tile using our tileset
                    spriteBatch.Draw(Tileset.TilesetTexture, destinationRectangle, tileGrid[i][j], Color.White);
                }
            }
        }
    }
}
