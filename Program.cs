﻿using System;
using System.Threading;

namespace BasicCommander
{
	class Program
	{
		static void Main(string[] args)
		{
			Keyboard.Initialize();
			Output.Initialize();

			CreateMainThread();
		}

		static void CreateMainThread()
		{
			Thread th = new Thread(Update);
			th.Name = "MainThread";
			//th.Start();
		}

		static void Update()
		{

		}
	}
}
