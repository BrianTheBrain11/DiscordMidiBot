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
			string noteName;
			string noteIntensity;
			if (CheckString(e.Event.ToString()))
			{			
				string tempString;

				tempString = e.Event.ToString();
				tempString = tempString.Replace("Note On [0] (", "");
				tempString = tempString.Replace(", ", "");
				tempString = tempString.Replace("(", "");
				tempString = tempString.Replace(")", "");

				if (tempString.Length == 4)
				{
					string tempClone = tempString;
					noteName = tempString.Substring(0, tempString.Length - 2);
                    noteIntensity = tempClone.Replace(noteName, "");
					tempString = tempString.Substring(0, tempString.Length - 2);
				}
				else
				{
					string tempclone = tempString;
					noteName = tempString.Substring(0, tempString.Length - 1);
					noteIntensity = tempclone.Replace(noteName, "");
					tempString = tempString.Substring(0, tempString.Length - 1);
				}
				Console.WriteLine($"Note name: {noteName}");
				Console.WriteLine($"Note intensity: {noteIntensity}");

				int nameInt = Int32.Parse(noteName);
				int noteInt = Int32.Parse(noteIntensity);

				NoteTransfer.noteBuffer.Add(new Note(NoteTransfer.noteDict[nameInt], noteInt));
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
