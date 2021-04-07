using System;
using DSharpPlus.VoiceNext;
using System.Threading;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;

namespace DiscordMidiBot
{
	public class Program
	{
		public static VoiceNextClient voice;

		static void Main(string[] args)
		{
			Console.WriteLine("Program Begun: DisocrdMidiBot");

			Thread midiThread = new Thread(MidiInitialize.StartMidi);
			midiThread.Start();

			Thread botThread = new Thread(BotInitialize.StartBot);
			botThread.Start();


		}
	}

    public class BotInitialize
    {
		public static void StartBot()
        {
            Console.WriteLine("Starting Bot");
			Bot bot = new Bot();
			bot.RunAsync().GetAwaiter().GetResult();
			Console.ReadKey();
        }
    }

	public class MidiInitialize
    {
		public static void StartMidi()
        {
            Console.WriteLine("Starting Midi");
            Midi midi = new Midi();
			try
			{
				midi.RunAsync().GetAwaiter().GetResult();
			}
			catch (Exception e)
            {
				Console.WriteLine("There is no midi device connected! Restart the program with the midi connected");
            }
        }
    }
}
