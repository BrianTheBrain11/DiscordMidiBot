using System;
using System.Collections.Generic;
using System.Text;
using Melanchall.DryWetMidi;

namespace DiscordMidiBot
{
	public static class NoteTransfer
	{
		public static Dictionary<int, string> noteDict = new Dictionary<int, string>()
		{
			{ 36, "C2"  },
			{ 37, "C#2" },
			{ 38, "D2"  },
			{ 39, "D#2" },
			{ 40, "E2"  },
			{ 41, "F2"  },
			{ 42, "F#2" },
			{ 43, "G2"  },
			{ 44, "G#2" },
			{ 45, "A2"  },
			{ 46, "A#2" },
			{ 47, "B2"  },
			{ 48, "C2"  },
			{ 49, "C#2" },
			{ 50, "D2"  },
			{ 51, "D#2" },
			{ 52, "E2"  },
			{ 53, "F2"  },
			{ 54, "F#2" },
			{ 55, "G2"  },
			{ 56, "G#2" },
			{ 57, "A2"  },
			{ 58, "A#2" },
			{ 59, "B2"  },
			{ 60, "C2"  },
			{ 61, "C#2" },
			{ 62, "D2"  },
			{ 63, "D#2" },
			{ 64, "E2"  },
			{ 65, "F2"  },
			{ 66, "F#2" },
			{ 67, "G2"  },
			{ 68, "G#2" },
			{ 69, "A2"  },
			{ 70, "A#2" },
			{ 71, "B2"  },
		};

		public static List<Note> noteBuffer = new List<Note>();

	}

	public class Note
    {
		public Note(string _name, int _intensity)
        {
			name = _name;
			intensity = _intensity;
        }

		public string name { get;  }
		public int intensity { get; }

        public override string ToString()
        {
			return name;
        }
    }
}
