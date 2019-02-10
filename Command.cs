using System;

namespace BasicCommander
{
	class Command
	{
		public ConsoleKeyInfo key;
		public Action command;
		public Context screen;

		public Command(ConsoleKeyInfo keysNeeded, Action commandToExecute, Context screenNeeded)
		{
			key = keysNeeded;
			command = commandToExecute;
			screen = screenNeeded;
		}
	}
}