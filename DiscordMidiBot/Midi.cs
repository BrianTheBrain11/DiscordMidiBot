using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;

namespace DiscordMidiBot
{
	public class Midi
	{
		public async Task RunAsync()
		{
			while (true)
			{
				using (var outputDevice = OutputDevice.GetByName("Yamaha Portatone-1"))
				{
					outputDevice.EventSent += OnEventSent;

					using (var inputDevice = InputDevice.GetByName("Yamaha Portatone-1"))
					{
						inputDevice.EventReceived += OnEventReceived;
						inputDevice.StartEventsListening();

						outputDevice.SendEvent(new NoteOnEvent());
						outputDevice.SendEvent(new NoteOffEvent());
					}
				}

				await Task.Delay(0);
			}
		}

		private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
		{
			var midiDevice = (MidiDevice)sender;
			if (CheckString(e.Event.ToString()))
			{

				Console.WriteLine($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
			}
		}

		private static void OnEventSent(object sender, MidiEventSentEventArgs e)
		{
			var midiDevice = (MidiDevice)sender;

			if (CheckString(e.Event.ToString()))
			{
				Console.WriteLine($"Event sent to '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
			}
		}

		private static bool CheckString(string _event)
		{
			List<string> exclude = new List<string>
			{
				{ "Note On [0] (0, 0)" },
				{ "Note Off [0] (0, 0)" },
				{ "Timing Clock" }
			};
			foreach (string message in exclude)
            {
				if (_event == message)
                {
					return false;
                }
            }
			return true;
		}

	}
}
