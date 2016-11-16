using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.tinylabproductions.TLPLib.Functional;

namespace Assets.Scripts
{
    //T - type of difficulty indication
    //for example int
    //class with difficulty name and int
    //float, etc.

     //S - type of a thing to increase/decrease based on difficulty
    abstract class ADifficulty<T,S>
    {
        public Option<T> difficulty;

        //Makes ga
        protected abstract S increaseDifficulty(T currDifficulty);
        protected abstract S decreaseDifficulty(T currDifficulty);

        protected abstract bool shouldIncreaseDifficulty();
    }
}
