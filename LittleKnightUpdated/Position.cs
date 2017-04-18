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
    public partial class Game1
    {
        /// <summary>
        /// Method Calculates where on screen the object should be from the map position
        /// </summary>
        /// <param name="MapPos">The position of the object in the Map(top Left corner)</param>
        /// <returns></returns>
        public static Vector2 CalculatePosition(Vector2 MapPos)
        {
            return new Vector2(MapPos.X - CameraShift, MapPos.Y);
        }
    }
}
