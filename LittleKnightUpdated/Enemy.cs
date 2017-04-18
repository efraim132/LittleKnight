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
    class Enemy
    {
        public int JumpHeight;
        public int VerticalSpeed = 0;
        public int IdleTime = 0;
        public int Health = 10;

        public Boolean Turn = true;

        public static Texture2D Walk;
        public static Texture2D EnemyStand;
        public static Texture2D EnemyFlying;
        public static Texture2D EnemyAttack;
        public static Texture2D EnemyIdle;

        public Vector2 MapPos = new Vector2(50, 50);
        public enum State
        {
            standing,
            running,
            attacking,
            blocking,
            jumping,
            idle
        }
        public State CurrentState = new State();
        public Enemy(){}
        
    }
}
