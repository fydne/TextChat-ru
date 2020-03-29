using EXILED.Extensions;
using System.Collections.Generic;
using TextChat.Enums;
using TextChat.Extensions;
using TextChat.Interfaces;
using static TextChat.Database;

namespace TextChat.Commands.Console
{
	public class PrivateChat : Chat, ICommand
	{
		public PrivateChat() : base(ChatRoomType.Private, Configs.privateMessageColor)
		{ }

		public string Description => "Отправляет личное сообщение в чат игроку.";

		public string Usage => ".chat_private [Ник/SteamID64/PlayerID] [Сообщение]";

		public (string response, string color) OnCall(ReferenceHub sender, string[] args)
		{
			(string message, bool isValid) = CheckMessageValidity(args.GetMessage(1), ChatPlayers[sender], sender);

			if (!isValid) return (message, "red");

			message = $"[{sender.GetNickname()}][PRIVATE]: {message}";

			ReferenceHub target = Player.GetPlayer(args[0]);

			if (target == null) return ($"Player {target.GetNickname()} не найден!", "red");
			else if (sender == target) return ("Вы не можете отправить сообщение себе!", "red");
			else if (!Configs.canSpectatorSendMessagesToAlive && sender.GetTeam() == Team.RIP && target.GetTeam() != Team.RIP)
			{
				return ("Вы не можете отправлять сообщения живым игрокам!", "red");
			}

			if (Configs.saveChatToDatabase) SaveMessage(message, ChatPlayers[sender], new List<Collections.Chat.Player>() { ChatPlayers[sender] }, type);

			target.SendConsoleMessage(message, color);

			if (Configs.showPrivateMessageNotificationBroadcast)
			{
				target.ClearBroadcasts();
				target.Broadcast(Configs.privateMessageNotificationBroadcastDuration, Configs.privateMessageNotificationBroadcast, true);
			}

			return (message, color);
		}
	}
}
