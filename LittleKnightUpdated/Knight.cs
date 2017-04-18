using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
public class Knight
{
    public int JumpHeight;
    public int VerticalSpeed = 0;
    public int IdleTime = 0;
    public int Health = 10;

    public Boolean SoldierTurn = true;

    public static Texture2D Walk;
    public static Texture2D KnightStand;
    public static Texture2D KnightFlying;
    public static Texture2D KnightAttack;
    public static Texture2D KnightIdle;
   
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
    public  State CurrentState = new State();
    public Knight()
	{
        CurrentState = State.standing;
	}
    public void setKnightJumpHeight(int newVal)
    {
        JumpHeight = newVal;
    }
    public void setVerticalSpeed(int newVal)
    {
        VerticalSpeed = newVal;
    }
  
    public void setSoldierTurn(Boolean newVal)
    {
        SoldierTurn = newVal;
    }
    public Boolean getSoldierTurn()
    {
        return SoldierTurn;
    }
}
