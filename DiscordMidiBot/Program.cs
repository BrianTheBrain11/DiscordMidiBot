using System;
using DSharpPlus;
using System.Threading;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;

namespace DiscordMidiBot
{
	class Program
	{
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
            midi.RunAsync().GetAwaiter().GetResult();
        }
    }
}
