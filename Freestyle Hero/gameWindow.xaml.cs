using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Freestyle_Hero
{
    /// <summary>
    /// Interaction logic for gameWindow.xaml
    /// </summary>
    public partial class gameWindow : Window
    {
        List<string> atStart = new List<string> {
            "cat",
            "at",
            "bat",
            "brat",
            "chat",
            "dat",
            "fat",
            "flat",
            "gat",
            "hat",
            "mat",
            "matt",
            "matte",
            "nat",
            "pat",
            "rat",
            "sat",
            "scat",
            "slat",
            "spat",
            "splat",
            "stat",
            "tat",
            "that",
            "vat"
        };

        List<string> adStart = new List<string> {
            "ad",
            "add",
            "brad",
            "cad",
            "chad",
            "clad",
            "dad",
            "fad",
            "glad",
            "grad",
            "had",
            "lad",
            "mad",
            "pad",
            "plaid",
            "rad",
            "sad",
            "tad",
        };

        List<string> otStart = new List<string>{
            "blot",
            "bought",
            "brought",
            "caught",
            "clot",
            "dot",
            "fought",
            "fraught",
            "haught",
            "jot",
            "knot",
            "lot",
            "naught",
            "not",
            "ought",
            "plot",
            "pot",
            "rot",
            "scott",
            "shot",
            "slot",
            "snot",
            "sought",
            "spot",
            "squat",
            "swat",
            "taught",
            "thought",
            "trot",
            "watt",
            "yacht",
        };

        List<string> ingEnd = new List<string> {
            "bing",
            "bring",
            "ching",
            "cling",
            "ding",
            "fling",
            "g-string",
            "king",
            "ling",
            "ping",
            "ring",
            "sing",
            "sling",
            "sting",
            "string",
            "swing",
            "thing",
            "wing",
            "wring",
            "wing",
            "xing",
            "zing",
        };

        List<string> essEnd = new List<string> {
            "yes",
            "bless",
            "chess",
            "cress",
            "dress",
            "guess",
            "jess",
            "less",
            "mess",
            "press",
            "stress",
        };

        List<string> estEnd = new List<string> {
            "blessed",
            "blest",
            "breast",
            "chest",
            "crest",
            "dressed",
            "fest",
            "guessed",
            "guest",
            "jest",
            "messed",
            "nest",
            "pest",
            "pressed",
            "quest",
            "rest",
            "stressed",
            "test",
            "vest",
            "west",
            "zest"
        };

        List<string> edEnd = new List<string> {
            "bed",
            "bled",
            "bred",
            "dead",
            "dread",
            "ed",
            "fed",
            "fled",
            "fred",
            "head",
            "lead",
            "led",
            "med",
            "ned",
            "pled",
            "read",
            "red",
            "said",
            "shed",
            "shred",
            "sled",
            "sped",
            "spread",
            "stead",
            "ted",
            "thread",
            "tread",
            "zed",
        };

        List<string> andEnd = new List<string> {
            "land",
            "band",
            "banned",
            "bland",
            "brand",
            "canned",
            "fanned",
            "gland",
            "grand",
            "hand",
            "manned",
            "panned",
            "planned",
            "sand",
            "scanned",
            "spanned",
            "stand",
            "strand",
            "tanned"
        };

        List<string> inkEnd = new List<string> {
            "think",
            "blink",
            "brink",
            "clink",
            "drink",
            "inc",
            "ink",
            "kink",
            "link",
            "mink",
            "pink",
            "rink",
            "sink",
            "stink",
            "sync",
            "wink",
            "zinc",
            "zink",
        };


        List<string> barsStart = new List<string>{ //these bars can match with any of the ends
            "Having fun is fun, __,",
            "Count it up, need a __",
            "I like it all unlike the __"
        };
        List<string> barsFinish = new List<string>{
            "back to that I must __.",
            "got no time to second __.",
            "you can put me to the __."
        };

        List<string> barsStartTwo = new List<string>{ //these bars can match with any of the ends
  
        };
        List<string> barsFinishTwo = new List<string>{

        };

        Player player = new Player();
        DispatcherTimer timer = new DispatcherTimer();
        int time = 0;
        double combo = 0; //every multiple of #score increases combo?
        double score = 0;
        double comboScore = 0;
        int missed = 0; //once 3 missed, skip the bar & reset combo.
        int bars = 0; //have mode for # of bars, 6 for default?
        int BPM = 840;
        string playerLyrics = "";
        string barOne = string.Empty;
        string barTwo = string.Empty;
        string selOne = string.Empty;
        string selTwo = string.Empty;
        bool gameLive = false;
        bool rethink = false;
        Word redWord = new Word();
        Word blueWord = new Word();
        Word greenWord = new Word();


        public gameWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            generateLyrics();
            if (redWord.getWord() == string.Empty) { redWord = generateWord(); } //Make red create words that are random
            if (blueWord.getWord() == string.Empty) { blueWord = generateWord(); } //Make blue create words that are 1 sylb
            if (greenWord.getWord() == string.Empty) { greenWord = generateWord(); } //Make green create words that are 2 sylb
            updateGame();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            time++;
        }

        private async void updateGame()
        {
            await Task.Delay(BPM).ContinueWith(_ =>
            {
                updateRedColumn(); updateBlueColumn(); updateGreenColumn();
                if(missed == 3)
                {
                    missedWord();
                }
                updateGame();
            });
        }

        private void updateRedColumn()
        {
            bool isEmpty = true;
            bool exit = false;
            ThreadPool.QueueUserWorkItem(o =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    foreach (Label curLabel in redColumn.Children)
                    {
                        if (curLabel.Content != null) { isEmpty = false; }
                    }
                });
                
                if (isEmpty)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Label temp = (Label)redColumn.Children[0];
                        temp.Content = redWord.getWord();
                        redColumn.UpdateLayout();
                    });
                }
                else
                {
                    Thread.Sleep(60);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        for (int i = 0; i < redColumn.Children.Count; i++)
                        {
                            if (exit)
                            { break; }
                            Label temp = (Label)redColumn.Children[i];
                            if (temp.Content != null)
                            {
                                if (i + 1 < redColumn.Children.Count)
                                {
                                    temp.Content = null;
                                    temp = (Label)redColumn.Children[i + 1];
                                    temp.Content = redWord.getWord();
                                    redColumn.UpdateLayout();
                                    //break;
                                    exit = true;
                                }
                                else
                                {
                                    temp.Content = null;
                                    //redQueue.Remove(redWord);
                                   // if (redQueue.Count == 0) { newQueue(redQueue); }
                                    redWord = generateWord();
                                    redColumn.UpdateLayout();
                                    //break;
                                    exit = true;
                                }
                            }
                        }
                    }));
                    Thread.Sleep(60);
                    //redColumn.UpdateLayout();
                }
            });
        }

        private void updateBlueColumn()
        {
            bool isEmpty = true;
            bool exit = false;
            ThreadPool.QueueUserWorkItem(o =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    foreach (Label curLabel in blueColumn.Children)
                    {
                        if (curLabel.Content != null) { isEmpty = false; }
                    }
                });

                if (isEmpty)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Label temp = (Label)blueColumn.Children[0];
                        temp.Content = blueWord.getWord();
                        blueColumn.UpdateLayout();
                    });
                }
                else
                {
                    Thread.Sleep(60);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        for (int i = 0; i < blueColumn.Children.Count; i++)
                        {
                            if (exit)
                            { break; }
                            Label temp = (Label)blueColumn.Children[i];
                            if (temp.Content != null)
                            {
                                if (i + 1 < blueColumn.Children.Count)
                                {
                                    temp.Content = null;
                                    temp = (Label)blueColumn.Children[i + 1];
                                    temp.Content = blueWord.getWord();
                                    blueColumn.UpdateLayout();
                                    exit = true;
                                }
                                else
                                {
                                    temp.Content = null;
                                    blueWord = generateWord();
                                    blueColumn.UpdateLayout();
                                    exit = true;
                                }
                            }
                        }
                    }));
                    Thread.Sleep(60);
                }
            });
        }

        private void updateGreenColumn()
        {
            bool isEmpty = true;
            bool exit = false;
            ThreadPool.QueueUserWorkItem(o =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    foreach (Label curLabel in greenColumn.Children)
                    {
                        if (curLabel.Content != null) { isEmpty = false; }
                    }
                });

                if (isEmpty)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Label temp = (Label)greenColumn.Children[0];
                        temp.Content = greenWord.getWord();
                        greenColumn.UpdateLayout();
                    });
                }
                else
                {
                    Thread.Sleep(60);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        for (int i = 0; i < greenColumn.Children.Count; i++)
                        {
                            if (exit)
                            { break; }
                            Label temp = (Label)greenColumn.Children[i];
                            if (temp.Content != null)
                            {
                                if (i + 1 < greenColumn.Children.Count)
                                {
                                    temp.Content = null;
                                    temp = (Label)greenColumn.Children[i + 1];
                                    temp.Content = greenWord.getWord();
                                    greenColumn.UpdateLayout();
                                    exit = true;
                                }
                                else
                                {
                                    temp.Content = null;
                                    greenWord = generateWord();
                                    greenColumn.UpdateLayout();
                                    exit = true;
                                }
                            }
                        }
                    }));
                    Thread.Sleep(60);
                }
            });
        }

        private Word generateWord()
        {
            Word newWord = new Word();
            List<string> temp = new List<string> { redWord.getWord(), blueWord.getWord(), greenWord.getWord()};
            do
            {
                Task.Delay(100).ContinueWith(_ =>
                {
                    List<List<string>> lists = new List<List<string>>
                    { atStart,adStart,otStart,ingEnd, essEnd, edEnd, andEnd, inkEnd, estEnd };
                    List<string> selected = lists[newWord.GetRandom().Next(0, lists.Count)];
                    int index = newWord.GetRandom().Next(0, selected.Count);
                    newWord.setWord(selected[index]);
                });
            } while (temp.Contains(newWord.getWord()));
            return newWord;
        }

        private void generateLyrics()
        {
            barOne = string.Empty;
            barTwo = string.Empty;
            Random random = new Random();
            barOne = barsStart[random.Next(0, barsStart.Count)];
            barTwo = barsFinish[random.Next(0, barsFinish.Count)];
            Label lyricOne = (Label)lyricsPanel.Children[0];
            Label lyricTwo = (Label)lyricsPanel.Children[1];
            lyricOne.Content = barOne.ToString();
            lyricTwo.Content = barTwo.ToString();
        }

        private void checkWord(string word)
        {
            bool foundOne = false;
            bool foundTwo = false;
            foreach (char c in barOne)
            {if(c == '_') {  foundOne = true; break; }}
            foreach (char c in barTwo)
            {if (c == '_') { foundTwo = true; break; }}

            if(foundOne)
            {
                for (int i = 0; i < barOne.Length; i++)
                {
                    if (barOne[i] == '_')
                    {
                        var split = barOne.Split('_');
                        barOne = barOne.Remove(i).Insert(i, word);
                        barOne = barOne + split[2];
                        break;
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    Label lyricOne = (Label)lyricsPanel.Children[0];
                    lyricOne.Content = barOne.ToString();
                    selOne = word;
                });
            }
            else if (foundTwo)
            {
                for (int i = 0; i < barTwo.Length; i++)
                {
                    if (barTwo[i] == '_')
                    {
                        var split = barTwo.Split('_');
                        barTwo = barTwo.Remove(i).Insert(i, word);
                        barTwo = barTwo + split[2];
                        break;
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    Label lyricTwo = (Label)lyricsPanel.Children[1];
                    lyricTwo.Content = barTwo.ToString();
                    selTwo = word;
                    checkBar(barOne, barTwo);
                    //checkBar(selOne, selTwo);
                    playerLyrics = playerLyrics + " " + barOne + " " + barTwo;
                    bars++;
                    generateLyrics();
                });
            }
        }

        private void missedWord()
        {
            bool foundOne = false;
            bool foundTwo = false;
            foreach (char c in barOne)
            {if (c == '_') { foundOne = true; break; }}
            foreach (char c in barTwo)
            {if (c == '_') { foundTwo = true; break; }}

            if (foundOne)
            {
                for (int i = 0; i < barOne.Length; i++)
                {
                    if (barOne[i] == '_')
                    {
                        var split = barOne.Split('_');
                        barOne = barOne.Remove(i).Insert(i, "--");
                        barOne = barOne + split[2];
                        break;
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    Label lyricOne = (Label)lyricsPanel.Children[0];
                    lyricOne.Content = barOne.ToString();
                });
            }
            else if (foundTwo)
            {
                for (int i = 0; i < barTwo.Length; i++)
                {
                    if (barTwo[i] == '_')
                    {
                        var split = barTwo.Split('_');
                        barTwo = barTwo.Remove(i).Insert(i, "--");
                        barTwo = barTwo + split[2];
                        break;
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    Label lyricTwo = (Label)lyricsPanel.Children[1];
                    lyricTwo.Content = barTwo.ToString();
                    playerLyrics = playerLyrics + " " + barOne + " " + barTwo;
                    checkBar(barOne, barTwo);
                    bars++;
                    generateLyrics();
                });
            }
        }

        private void checkBar(string bar1,string bar2)
        {
            //check words or bars for matching words in list
            //if(time < 15)
            //{

            //}
            bool check = false;
            List<List<string>> allWords = new List<List<string>> 
            { atStart, adStart, otStart, ingEnd, essEnd, edEnd, andEnd, inkEnd, estEnd };

            foreach(List<string> word in allWords)
            {
                if (word.Contains(selOne)) { if (word.Contains(selTwo)) { check = true; break; } }
            }

            if (check)
            {
                score = score + 150;
            }
        }

        private void updateCombo()
        {
            switch (comboScore)
            {
                case var expression when comboScore < 1500:
                    combo = 1;
                break;

                case var expression when comboScore >= 1500:
                    combo = 2;
                break;

                case var expression when comboScore >= 5000:
                    combo = 2.5;
                break;

                case var expression when comboScore >= 8500:
                    combo = 3;
                break;

                case var expression when comboScore >= 12000:
                    combo = 3.5;
                break;

                case var expression when comboScore >= 17500:
                    combo = 4;
                break;
            }
        }

        private void aButton_Click(object sender, RoutedEventArgs e)
        {
            Label temp = redColumn.Children[5] as Label;
            if(temp.Content != null)
            {
                score = score + 50;
                comboScore = comboScore + 50;
                updateCombo();
                missed = 0;
                rethink = false;
                checkWord(temp.Content.ToString());
                scoreLabel.Content = score + " " + "x" + combo;
            }
            else
            {
                missed++;
                comboScore = 0;
                scoreLabel.Content = score + " " + "x" + combo;
            }
        }

        private void sButton_Click(object sender, RoutedEventArgs e)
        {
            Label temp = blueColumn.Children[5] as Label;
            if (temp.Content != null)
            {
                score = score + 50;
                comboScore = comboScore + 50;
                missed = 0;
                rethink = false;
                checkWord(temp.Content.ToString());
                scoreLabel.Content = score + " " + "x"  + combo;
            }
            else
            {
                missed++;
                comboScore = 0;
                scoreLabel.Content = score + " " + "x" + combo;
            }
        }

        private void dButton_Click(object sender, RoutedEventArgs e)
        {
            Label temp = greenColumn.Children[5] as Label;
            if (temp.Content != null)
            {
                score = score + 50;
                comboScore = comboScore + 50;
                missed = 0;
                rethink = false;
                checkWord(temp.Content.ToString());
                scoreLabel.Content = score + " " + "x" + combo;
            }
            else
            {
                missed++;
                comboScore = 0;
                scoreLabel.Content = score + " " + "x" + combo;
            }
        }

        private void rethinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!rethink)
            {
                redWord = generateWord();
                blueWord = generateWord();
                greenWord = generateWord();
                rethink = true;
            }
            else
            {
                //make some text show up saying no no no lol
            }
        }
    }
}
