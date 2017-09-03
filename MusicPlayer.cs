using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFramework
{
    public class MusicPlayer
    {
        LinkedList<int> notes;
        Dictionary<char, int> noteKeys;
        int playbackSpeed { get; set; }
        int pauseDuration { get; set; }
        bool looping { get; set; }
        bool playing { get; set; }

        Thread mpThread;

        public MusicPlayer()
        {
            playing = false;

            noteKeys = new Dictionary<char, int>(14);
            noteKeys['C'] = 1;
            noteKeys['c'] = 2;
            noteKeys['D'] = 3;
            noteKeys['d'] = 4;
            noteKeys['E'] = 5;
            noteKeys['F'] = 6;
            noteKeys['f'] = 7;
            noteKeys['G'] = 8;
            noteKeys['g'] = 9;
            noteKeys['A'] = 10;
            noteKeys['a'] = 11;
            noteKeys['B'] = 12;
        }

        int noteToFreqency(char note, int octave)
        {
            int key = noteKeys[note] + 12 * octave - 9;
            return (int) (440.0 * Math.Pow(2.0, (key - 49.0) / 12.0));
        }

        public void play(String noteFilePath, int playbackSpeed = 1000, int pauseDuration = 0, bool looping = false)
        {
            parseNoteFile(noteFilePath);
            this.playbackSpeed = playbackSpeed;
            this.pauseDuration = pauseDuration;
            this.looping = looping;

            if (playing)
            {
                playing = false;
                mpThread.Abort();
            }

            playing = true;
            mpThread = new Thread(playback);
            mpThread.Start();
        }

        void playback()
        {
            do
            {
                foreach (int freq in notes)
                {
                    if (!playing)
                        break;

                    if (freq == 0)
                    {
                        Thread.Sleep(playbackSpeed);
                    }
                    else
                    {
                        Console.Beep(freq, playbackSpeed);
                    }
                    Thread.Sleep(pauseDuration);
                }

            } while (looping && playing);

            playing = false;
        }

        public void stop()
        {
            if (playing)
            {
                playing = false;
                mpThread.Abort();
            }
        }

        void parseNoteFile(String noteFilePath)
        {
            var reader = new System.IO.StreamReader(noteFilePath);
            string noteString = reader.ReadLine();

            notes = new LinkedList<int>();
            for (int i = 0; i < noteString.Length; i += 2)
            {
                int freq = 0;
                if (noteString[i] == ' ')
                {
                    notes.AddLast(0);
                    continue;
                }
                else if (noteString[i] == '-' && i > 0)
                {
                    notes.AddLast(notes.Last());
                    continue;
                }
                else if (noteKeys.ContainsKey(noteString[i]))
                {
                    freq = noteToFreqency(noteString[i], Int32.Parse(Char.ToString(noteString[i + 1])));
                    notes.AddLast(freq);
                }
                else
                {
                    throw new FormatException("Invalid character found in note file");
                }
            }
        }
    }
}
