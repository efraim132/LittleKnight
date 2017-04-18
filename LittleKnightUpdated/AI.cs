using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleKnight
{
    class AI
    {
        int difficulty;
        //Difficulty ranges from 1-10
        Enemy ControlledEnemy;
        public AI(int Difficulty, Enemy Control)
        {
            difficulty = Difficulty;
            ControlledEnemy = Control;
        }
        void Update()
        {
            //updates the AI's information
        }
    }
}
