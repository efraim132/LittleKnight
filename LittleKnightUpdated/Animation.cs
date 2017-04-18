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
    class Animation
    {
        //SpriteBatch spriteBatch;
        //GraphicsDeviceManager graphics;

        public int FrameInt = 0;
        int AnimationCount = 0;
        Vector2 Resolution;
        int TotalFramesInAnim;
        int FrameDelay;
        Texture2D SpriteSheet;
        Vector2 Coordinates;
        bool Flipped;
        /// <summary>
        /// Animation Object
        /// <param name="Frames"> The amount of frames in the spritesheet</param>
        /// <param name="InbFrames"> Amount of frames in between each spriteseet frame</param>
        /// <param name="Texture">The texture of the spritesheet</param>
        /// <param name="Texture"> The resolution of one frame</param>
        /// <param name="Coords"> Coordinates on screen where to display</param>
        /// <param name="Flipped">Flipped Boolean Value</param>
        /// </summary>

        public Animation(int Frames, int InbFrames, Texture2D Texture, Vector2 ResolutionOfFrame, Vector2 Coords, bool Flipped)
        {
            TotalFramesInAnim = Frames;
            FrameDelay = InbFrames;
            SpriteSheet = Texture;
            Coordinates = Coords;
            this.Flipped = Flipped;
            
            Resolution = ResolutionOfFrame;
        }
        
       public void Update(bool flip, Vector2 Coord)
        {
            Flipped = flip;
            AnimationCount++;
           if(AnimationCount == FrameDelay)
            {
                AnimationCount = 0;
                FrameInt++;
                if (FrameInt >= TotalFramesInAnim)
                    FrameInt = 0;
            }
            Coordinates = Coord;
            Draw();
        }
        void Draw()
        {
            if (Flipped)
            {
                Game1.spriteBatch.Draw(SpriteSheet, Coordinates, new Rectangle(FrameInt * (int)Resolution.X, 0, (int)Resolution.Y, (int)Resolution.Y), Color.White);
            }
            else
            {
                Game1.spriteBatch.Draw(SpriteSheet, Coordinates, new Rectangle(FrameInt * (int)Resolution.X, 0, (int)Resolution.Y, (int)Resolution.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
        }
       public void reset()
        {
            FrameInt = 0;
            AnimationCount = 0;
        }
    }
}
