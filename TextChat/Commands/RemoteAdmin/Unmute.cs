using EXILED.Extensions;
using System;
using TextChat.Extensions;
using TextChat.Interfaces;
using static TextChat.Database;

namespace TextChat.Commands.RemoteAdmin
{
	public class Unmute : ICommand
	{
		public string Description => "Включить игроку чат";

		public string Usage => ".chat_unmute [SteamID64/UserID/Ник]";

		public (string response, string color) OnCall(ReferenceHub sender, string[] args)
		{
			if (!sender.CheckPermission("tc.unmute")) return ("У вас недостаточно прав для запуска этой команды!", "red");

			if (args.Length != 1) return ($"Вы должны предоставить один параметр! {Usage}", "red");

			ReferenceHub target = Player.GetPlayer(args[0]);

			if (target == null) return ($"{args[0]} не найден!", "red");

			var mutedPlayer = LiteDatabase.GetCollection<Collections.Chat.Mute>().FindOne(mute => mute.Target.Id == target.GetRawUserId() && mute.Expire > DateTime.Now);

			if (mutedPlayer == null) return ($"{target.GetNickname()} не замьючен!", "red");

			mutedPlayer.Expire = DateTime.Now;

			LiteDatabase.GetCollection<Collections.Chat.Mute>().Update(mutedPlayer);

			target.SendConsoleMessage("Вы были размьючены в чате!", "green");

			return ($"{target.GetNickname()} был размьючен в чате", "green");
		}
	}
}
