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
   
    public partial class Game1 : Game
    {
        public static SpriteBatch spriteBatch;

        Animation KnightWalking;
        Animation KnightAttack;
        Animation KnightIdle;

        GraphicsDeviceManager graphics;
    
        Texture2D background;
        Texture2D[] floors = new Texture2D[4];

        Vector2 backgroundPos = new Vector2(0, 0);
        
        int[] floorint = new int[10];
        public static int CameraShift = 0;  

        Random randomgen = new Random();

        Knight mainKnight = new Knight();
        Building TestBuild;

        static int screengroundLevel = 411;

        public static int cameraShiftRate = 2;
        /// <summary>
        /// Right Boundry where screen will shift
        /// </summary>
        static int leftBound = 100;
        /// <summary>
        /// Right Boundry where screen will shift
        /// </summary>
        static int rightBound = 250;
        

        enum GameState{
            Menu,
            InGame
        }
        GameState CurrentGameState = new GameState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;


        }
        void randomizeFloor()
        {
            for(int a = 0; a<10; a++)
            {
                floorint[a] = randomgen.Next(4);
            }
        }
        protected override void Initialize()
        {
            MouseCursor.FromTexture2D(Content.Load<Texture2D>("Cursor1"), 0, 0);
            IsMouseVisible = true;
            // TODO: Add your initialization logic here
            randomizeFloor();
        //    DestRekt = new Rectangle(100, 100, 60, 60);
            base.Initialize();
            //    frame = 0;
            mainKnight.setKnightJumpHeight(0);
            //   SourceRekt = new Rectangle(0, 0, 60, 60);
            // Viking AI difficlty is 10 for testing
            CurrentGameState = GameState.InGame;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("SkyOversize");
            floors[0] = Content.Load<Texture2D>("grass");
            floors[1] = Content.Load<Texture2D>("grasswbush");
            floors[2] = Content.Load<Texture2D>("grasswrocks");
            floors[3] = Content.Load<Texture2D>("grasswstick");
            Knight.Walk = Content.Load<Texture2D>("SoldierBlu_Running-sheet");
            Knight.KnightStand = Content.Load<Texture2D>("SoldierStand");
            Knight.KnightFlying = Content.Load<Texture2D>("SoldierBlu_Flying");
            Knight.KnightAttack = Content.Load<Texture2D>("SoldierBlu_attack");
            Knight.KnightIdle = Content.Load<Texture2D>("SoldierBlu_idle");
            // TODO: use this.Content to load your game content here
            IntializationAfterContent();
            TestBuild = new Building(Content.Load<Texture2D>("Building100x100"), new Vector2(1000, 371));
            }
        /// <summary>
        /// Generally just loads in the animation objects because they need the textures after load content
        /// </summary>
        public void IntializationAfterContent()
        {
            KnightAnimationLoad();

        }
        void KnightAnimationLoad()
        {
            KnightWalking = new Animation(4, 10, Knight.Walk, new Vector2(60, 60), CalculatePosition(mainKnight.MapPos), mainKnight.SoldierTurn);
            KnightAttack = new Animation(14, 4, Knight.KnightAttack, new Vector2(60, 60), CalculatePosition(mainKnight.MapPos), mainKnight.SoldierTurn);
            KnightIdle = new Animation(5, 15, Knight.KnightIdle, new Vector2(60, 60), CalculatePosition(mainKnight.MapPos), mainKnight.SoldierTurn);
        }
        protected override void UnloadContent()
        {
            //this is for unloading content
        }
        /// <summary>
        /// Interprates the controls, and it bascally sets the state of the character and Jumping Handling
        /// </summary>
        void interperateControls()
        {
            //If Knight is attacking, you cannot move
            if (mainKnight.CurrentState == Knight.State.attacking)
            {
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (mainKnight.MapPos.Y == screengroundLevel)
                        mainKnight.setVerticalSpeed(-10);
                    return;
                }
                mainKnight.CurrentState = Knight.State.running;

                mainKnight.setSoldierTurn(true);
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (mainKnight.MapPos.Y == screengroundLevel)
                        mainKnight.setVerticalSpeed(-10);
               
                    return;
                }
                mainKnight.CurrentState = Knight.State.running;
                mainKnight.setSoldierTurn(false);

                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (mainKnight.MapPos.Y == screengroundLevel)
                mainKnight.setVerticalSpeed(-10);

                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                mainKnight.CurrentState = Knight.State.attacking;
   
                return;
            }
            
            mainKnight.IdleTime++;
            if(mainKnight.CurrentState != Knight.State.idle)
            mainKnight.CurrentState = Knight.State.standing;

        }
        /// <summary>
        /// Gets the states of the player and updates the location of the player
        /// </summary>
        void UpdateGameLogic()
        {
            interperateControls();

            //Camera shift is based
           

            if (mainKnight.CurrentState == Knight.State.running)
            {
                    if (mainKnight.getSoldierTurn())
                    {
                        mainKnight.MapPos.X += cameraShiftRate;
                        if(CalculatePosition(mainKnight.MapPos).X > rightBound)
                        {
                           if(CameraShift<800)
                           CameraShift += cameraShiftRate;
                        }
                    }
                    else
                    {
                        mainKnight.MapPos.X -= 2;
                    if (CalculatePosition(mainKnight.MapPos).X < leftBound)
                    {
                        if(CameraShift!=0)
                        CameraShift -= 2;
                    }
                }
         
            }
           //Updates the position vertically of the player
            mainKnight.MapPos.Y += mainKnight.VerticalSpeed;
            if (mainKnight.MapPos.Y < screengroundLevel)
            {
                if (mainKnight.VerticalSpeed <= 5)
                {
                    mainKnight.VerticalSpeed += 1;
                }
            
            }
            else
            {
                mainKnight.VerticalSpeed = 0;
                mainKnight.MapPos.Y = screengroundLevel;
            }
            //1000 is the amount of frames while there is no action to take to cause the knight to start idling
            //Idle handling
            mainKnight.IdleTime++;
            if(mainKnight.IdleTime == 1000)
            {
                mainKnight.IdleTime = 0;
                mainKnight.CurrentState = Knight.State.idle;
            }
            //Camera shift is based
            //if(CalculatePosition(mainKnight.MapPos).X > CameraShift + 599)
            //{
            //    CameraShift+=2;
             //}
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { 
                Exit(); }
            UpdateGameLogic();
            base.Update(gameTime);
        }
        void DrawBuildings()
        {
            //Call Draw() function on each building here.They are drawn behind the grass, but infront of the background.
            TestBuild.Draw();
        }
        void DrawGame()
        {
            if (CurrentGameState == GameState.InGame)
            {
                DrawBackground();
                DrawBuildings();
                DrawFloor();
                DrawKnight();
            }
            else
            {
                DrawMenu();
            }
        }
        void DrawMenu()
        {

        }
        void DrawBackground()
        {
            spriteBatch.Draw(background, backgroundPos, Color.White);
        }
        void DrawFloor()
        {
            Vector2 wheretodraw = Vector2.Zero;
            wheretodraw.Y = 450;
            for(int a = 0; a < 5; a++)
            {
                wheretodraw.X = a * floors[0].Width;
                spriteBatch.Draw(floors[floorint[a]], CalculatePosition(wheretodraw), Color.White);
            }
            
            //MovingRight();
        }
        void DrawKnight()
        {
            
            if (mainKnight.MapPos.Y< screengroundLevel)
            {
                if (mainKnight.SoldierTurn)
                {
                    spriteBatch.Draw(Knight.KnightFlying, CalculatePosition(mainKnight.MapPos), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Knight.KnightFlying, CalculatePosition(mainKnight.MapPos), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                }
                return;
            }
            switch (mainKnight.CurrentState)
            {
                case Knight.State.standing:
                    if (mainKnight.SoldierTurn)
                    {
                        spriteBatch.Draw(Knight.KnightStand, CalculatePosition(mainKnight.MapPos), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(Knight.KnightStand, CalculatePosition(mainKnight.MapPos), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    }
                    break;
                case Knight.State.attacking:
                    KnightIdle.reset();
                    mainKnight.IdleTime = 0;
                    KnightWalking.reset();
                    KnightAttack.Update(mainKnight.SoldierTurn, CalculatePosition(mainKnight.MapPos));
                    if (KnightAttack.FrameInt == 13)
                        mainKnight.CurrentState = 0;
                    break;
                case Knight.State.running:
                    KnightIdle.reset();
                    mainKnight.IdleTime = 0;
                    KnightAttack.reset();
                    KnightWalking.Update(mainKnight.SoldierTurn, CalculatePosition(mainKnight.MapPos));
                    break;
                case Knight.State.idle:
                    KnightAttack.reset();
                    KnightWalking.reset();
                    KnightIdle.Update(mainKnight.SoldierTurn, CalculatePosition(mainKnight.MapPos));
                    break;

            }
        }
       protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawGame();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
