using EXILED;
using EXILED.Extensions;
using System;
using System.Linq;
using TextChat.Extensions;
using TextChat.Interfaces;
using static TextChat.Database;

namespace TextChat.Events
{
	public class PlayerHandler
	{
		private readonly TextChat pluginInstance;

		public PlayerHandler(TextChat pluginInstance) => this.pluginInstance = pluginInstance;

		public void OnConsoleCommand(ConsoleCommandEvent ev)
		{
			if (ev.Player == null) return;

			if (ev.Player.gameObject == PlayerManager.localPlayer)
			{
				ev.ReturnMessage = "Вы не можете использовать консоль сервера!";
				ev.Color = "red";

				return;
			}

			(string commandName, string[] commandArguments) = ExtractCommand(ev.Command);

			if (!pluginInstance.ConsoleCommands.TryGetValue(commandName, out ICommand command)) return;

			try
			{
				(string response, string color) = command.OnCall(ev.Player, commandArguments);

				ev.ReturnMessage = response;
				ev.Color = color;
			}
			catch (Exception exception)
			{
				Log.Error($"{commandName} command error: {exception}");
				ev.ReturnMessage = "Произошла ошибка при выполнении команды!";
				ev.Color = "red";
			}
		}

		public void OnRemoteAdminCommand(ref RACommandEvent ev)
		{
			(string commandName, string[] commandArguments) = ExtractCommand(ev.Command);

			if (!pluginInstance.RemoteAdminCommands.TryGetValue(commandName, out ICommand command)) return;

			try
			{
				(string response, string color) = command.OnCall(Player.GetPlayer(ev.Sender.SenderId), commandArguments);
				ev.Sender.RAMessage($"<color={color}>{response}</color>", color == "green");
				ev.Allow = false;
			}
			catch (Exception exception)
			{
				Log.Error($"{commandName} command error: {exception}");
				ev.Sender.RAMessage("Произошла ошибка при выполнении команды!", true);
			}
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			ChatPlayers.Add(ev.Player, new Collections.Chat.Player()
			{
				Id = ev.Player.GetRawUserId(),
				Authentication = ev.Player.GetAuthentication(),
				Name = ev.Player.GetNickname()
			});

			ev.Player.SendMessage("Добро пожаловать в чат!", "green");
		}

		public void OnPlayerLeave(PlayerLeaveEvent ev) => ChatPlayers.Remove(ev.Player);

		private (string commandName, string[] arguments) ExtractCommand(string commandLine)
		{
			var extractedCommandArguments = commandLine.Split(' ');

			return (extractedCommandArguments[0].ToLower(), extractedCommandArguments.Skip(1).ToArray());
		}
	}
}
