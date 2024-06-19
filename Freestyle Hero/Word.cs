using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle_Hero
{
    public class Word
    {
        string value = string.Empty;
        Random rand = new Random();


        public Word() { }

        public string getWord() { return value; }
        public void setWord(string value) {  this.value = value; }

        public Random GetRandom() { return rand; }
    }
}
