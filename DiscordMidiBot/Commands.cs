using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus.EventArgs;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;

namespace DiscordMidiBot
{
	public class Commands
	{
		[Command("join")]
		public async Task Join(CommandContext ctx)
        {
			var vnext = ctx.Client.GetVoiceNextClient();

			var vnc = vnext.GetConnection(ctx.Guild);
			if (vnc != null)
				throw new InvalidOperationException("Already connected in this guild");

			var chn = ctx.Member?.VoiceState?.Channel;
			if (chn == null)
				throw new InvalidOperationException("you need to be in a vc");

			vnc = await vnext.ConnectAsync(chn);
			await ctx.RespondAsync("okay");
        }

		[Command("leave")]
		public async Task Leave(CommandContext ctx)
		{
			Console.WriteLine("trying to leave");
			await ctx.RespondAsync("trying to leave");

			var vnext = ctx.Client.GetVoiceNextClient();

			var vnc = vnext.GetConnection(ctx.Guild);
			if (vnc == null)
				throw new InvalidOperationException("Not connected in this guild.");

			vnc.Disconnect();
			await ctx.RespondAsync("👌");
		}
	}
}
