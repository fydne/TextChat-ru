using System;
using System.Collections.Generic;
using TextChat.Collections.Chat;
using TextChat.Enums;
using TextChat.Extensions;

namespace TextChat.Commands.Console
{
	public class Chat
	{
		protected readonly ChatRoomType type;
		protected string color;

		protected Chat(ChatRoomType type) => this.type = type;

		protected Chat(ChatRoomType type, string color) : this(type) => this.color = color;

		protected (string message, bool isValid) CheckMessageValidity(string message, Player messageSender, ReferenceHub sender)
		{
			if (string.IsNullOrEmpty(message.Trim())) return ("Сообщение не может быть пустым!", true);
			else if (sender.IsChatMuted()) return ("Вы замьючены!", true);
			else if (messageSender.IsFlooding(Configs.slowModeCooldown)) return ("Вы отправляете сообщения слишком быстро!", true);
			else if (message.Length > Configs.maxMessageLength) return ($"Сообщение слишком длинное! Максимальная длина: {Configs.maxMessageLength}", true);

			return (message, true);
		}

		protected void SendConsoleMessage(ref string message, Player sender, IEnumerable<ReferenceHub> targets)
		{
			targets.SendConsoleMessage(message = Configs.censorBadWords ? message.Sanitize(Configs.badWords, Configs.censorBadWordsChar) : message, color);

			sender.lastMessageSentTimestamp = DateTime.Now;
		}
	}
}
