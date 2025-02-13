using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace warcraft_4
{
    // ¨This class is to hold the info we need for our tiles to work splendidly
    public class Tileset
    {
        public Texture2D TilesetTexture; // Holds the texture
        public List<Rectangle> TileRegions; // List to hold tile rectangles

        public Tileset(Texture2D texture)
        {
            TilesetTexture = texture;
            TileRegions = new List<Rectangle>();
            TileRegions.Add(new Rectangle(0, 0, 16, 16));    // Tile 1
            TileRegions.Add(new Rectangle(16, 0, 16, 16));   // Tile 2
            TileRegions.Add(new Rectangle(32, 0, 16, 16));   // Tile 3
            TileRegions.Add(new Rectangle(48, 0, 16, 16));   // Tile 4
        }

        // Method to add tiles (specify each tile's position and size on the tileset)
        public void AddTile(int x, int y, int width, int height)
        {
            TileRegions.Add(new Rectangle(x, y, width, height));
        }

        //now we can use AddTile in our other classes!
    }
}
