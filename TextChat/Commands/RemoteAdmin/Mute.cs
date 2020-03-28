using EXILED.Extensions;
using System;
using System.Linq;
using TextChat.Extensions;
using TextChat.Interfaces;
using static TextChat.Database;

namespace TextChat.Commands.RemoteAdmin
{
	public class Mute : ICommand
	{
		public string Description => "Отключить звук игрока из чата.";

		public string Usage => "chat_mute [PlayerID/SteamID64/Ник] [минут] [причина]";

		public (string response, string color) OnCall(ReferenceHub sender, string[] args)
		{
			if (!sender.CheckPermission("tc.mute")) return ("У вас недостаточно прав для запуска этой команды!", "red");

			if (args.Length < 2) return ($"Вы должны предоставить два параметра! {Usage}", "red");

			ReferenceHub target = Player.GetPlayer(args[0]);

			if (target == null) return ($"{args[0]} не найден!", "red");

			if (!double.TryParse(args[1], out double duration) || duration < 1) return ($"{args[1]} недопустимая продолжительность!", "red");

			string reason = string.Join(" ", args.Skip(2).Take(args.Length - 2));

			if (string.IsNullOrEmpty(reason)) return ("Причина не может быть пустой!", "red");

			if (target.IsChatMuted()) return ($"{target.GetNickname()} уже отключен!", "red");

			LiteDatabase.GetCollection<Collections.Chat.Mute>().Insert(new Collections.Chat.Mute()
			{
				Target = ChatPlayers[target],
				Issuer = ChatPlayers[sender],
				Reason = reason,
				Timestamp = DateTime.Now,
				Expire = DateTime.Now.AddMinutes(duration)
			});

			if (Configs.showChatMutedBroadcast)
			{
				target.ClearBroadcasts();
				target.Broadcast(Configs.chatMutedBroadcastDuration, string.Format(Configs.chatMutedBroadcast, duration, reason), true);
			}

			target.SendMessage($"Вы были отключены от чата на {duration} минут{(duration != 1 ? "сек" : "")}! Причина: {reason}", "red");

			return ($"{target.GetNickname()} был отключен в чате на {duration} минут{(duration != 1 ? "сек" : "")}, причина: {reason}", "green");
		}
	}
}
