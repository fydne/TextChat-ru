using EXILED.Extensions;
using System.Collections.Generic;
using System.Linq;
using TextChat.Enums;
using TextChat.Extensions;
using TextChat.Interfaces;
using static TextChat.Database;

namespace TextChat.Commands.Console
{
	public class PublicChat : Chat, ICommand
	{
		public PublicChat() : base(ChatRoomType.Public, Configs.generalChatColor)
		{ }

		public string Description => "Отправляет сообщение всем на сервере.";

		public string Usage => ".chat [Сообщение]";

		public (string response, string color) OnCall(ReferenceHub sender, string[] args)
		{
			(string message, bool isValid) = CheckMessageValidity(args.GetMessage(), ChatPlayers[sender], sender);

			if (!isValid) return (message, "red");

			message = $"[{sender.GetNickname()}][PUBLIC]: {message}";

			IEnumerable<ReferenceHub> targets = Player.GetHubs().Where(target =>
			{
				return sender != target && (Configs.canSpectatorSendMessagesToAlive || sender.GetTeam() != Team.RIP || target.GetTeam() == Team.RIP);
			});

			List<Collections.Chat.Player> chatPlayers = targets.GetChatPlayers(ChatPlayers);

			if (chatPlayers.Count == 0) return ("Нет доступных игроков для общения!", "red");

			if (Configs.saveChatToDatabase) SaveMessage(message, ChatPlayers[sender], chatPlayers, type);

			targets.SendConsoleMessage(message, color);			

			return (message, color);
		}
	}
}
