using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LittleKnight
{
    class Building
    {
        public Texture2D TextureOfBuilding;
        public Vector2 PositionOfBuildingInMap;
        public Building(Texture2D Texture, Vector2 Pos)
        {
            TextureOfBuilding = Texture;
            PositionOfBuildingInMap = Pos;
        }
        public void Draw()
        {
            Game1.spriteBatch.Draw(TextureOfBuilding, Game1.CalculatePosition(PositionOfBuildingInMap), Color.White);
        }
    }
}
