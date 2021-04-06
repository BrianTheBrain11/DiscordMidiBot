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

			Thread botThread = new Thread(BotInitialize.StartBot);
			botThread.Start();

			Thread midiThread = new Thread(MidiInitialize.StartMidi);
			midiThread.Start();
		}
	}

    public class BotInitialize
    {
		public static void StartBot()
        {
			Bot bot = new Bot();
			bot.RunAsync().GetAwaiter().GetResult();
			Console.ReadKey();
        }
    }

	public class MidiInitialize
    {
		public static void StartMidi()
        {

        }

		public static void MidiLoop()
        {
            using (var outputDevice = OutputDevice.GetByName("MIDI Device"))
            {
                outputDevice.EventSent += OnEventSent;

                using (var inputDevice = InputDevice.GetByName("MIDI Device"))
                {
                    inputDevice.EventReceived += OnEventReceived;
                    inputDevice.StartEventsListening();

                    outputDevice.SendEvent(new NoteOnEvent());
                    outputDevice.SendEvent(new NoteOffEvent());
                }
            }
        }

        private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }

        private static void OnEventSent(object sender, MidiEventSentEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event sent to '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }
    }
}
