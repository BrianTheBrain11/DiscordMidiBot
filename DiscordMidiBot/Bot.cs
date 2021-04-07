using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.VoiceNext;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMidiBot
{
    public class Bot
	{
		public DiscordClient client { get; private set; }
		public CommandsNextModule commands { get; private set; }

		public async Task RunAsync()
		{
			string json = string.Empty;

			using (FileStream fs = File.OpenRead("config.json"))
			{
				using (StreamReader sr = new StreamReader(fs, new UTF8Encoding(false)))
				{
					json = await sr.ReadToEndAsync().ConfigureAwait(false);
				}
			}

			var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

			DiscordConfiguration config = new DiscordConfiguration
			{
				Token = configJson.Token,
				TokenType = TokenType.Bot,
				AutoReconnect = true,
				LogLevel = LogLevel.Debug,
				UseInternalLogHandler = true
			};

			client = new DiscordClient(config);

			client.Ready += OnClientReady;

			CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
			{
				StringPrefix = configJson.Prefix,
				EnableDms = true,
				EnableMentionPrefix = true,
			};

			commands = client.UseCommandsNext(commandsConfig);

			commands.RegisterCommands<Commands>();

			Program.voice = client.UseVoiceNext();

			await client.ConnectAsync();

			await Task.Delay(-1);
		}

		private Task OnClientReady(ReadyEventArgs e)
        {
			return Task.CompletedTask;
        }
	}
}
