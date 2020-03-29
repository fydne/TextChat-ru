using System.Text;
using TextChat.Interfaces;

namespace TextChat.Commands.Console
{
	public class Help : ICommand
	{
		private readonly TextChat pluginInstance;

		public Help(TextChat pluginInstance) => this.pluginInstance = pluginInstance;

		public string Description => "Отправляет список команд или описание отдельной команды.";

		public string Usage => ".help/.help [Команда]";

		public (string response, string color) OnCall(ReferenceHub sender, string[] args)
		{
			if (pluginInstance.ConsoleCommands.Count == 0) return ("Нет команд для отправки!", "red");

			if (args.Length == 0)
			{
				StringBuilder commands = new StringBuilder($"\n\n[СПИСОК КОМАНД: ({pluginInstance.ConsoleCommands.Count})]\n by fydne");

				foreach (ICommand command in pluginInstance.ConsoleCommands.Values)
				{
					commands.Append($"\n\n{command.Usage}\n\n{command.Description}");
				}

				return (commands.ToString(), "green");
			}
			else if (args.Length == 1)
			{
				if (!pluginInstance.ConsoleCommands.TryGetValue(args[0].Replace(".", ""), out ICommand command)) return ($"Команда {args[0]} не найдена!", "red");

				return ($"\n\n{command.Usage}\n\n{command.Description}", "green");
			}

			return ("Слишком много аргументов!", "red");
		}
	}
}
