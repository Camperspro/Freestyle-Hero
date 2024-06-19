using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle_Hero
{
    public class Player
    {
        string name = "newArtist";
        int score = 0;
        string lyrics = string.Empty;

        public Player() { }

        public string getName() { return name; }
        public int getScore() { return score; }
        public string getLyrics() {  return lyrics; }
        public void setLyrics(string lyrics) {this.lyrics = lyrics;}
        public void setScore(int score) { this.score = score;}
        public void setName(string name) { this.name = name;}
    }
}
