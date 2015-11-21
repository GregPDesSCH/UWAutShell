/*
 * 	University of Waterloo
 * 	SE Hack Day - UW AutShell
 * 
 *  Coded in November 2015
 * 
 * 	File Name: Main.cs
 * 
 * 	© 2015 University of Waterloo. Source Code © 2015 Gregory Desrosiers.
 * 	ALL RIGHTS RESERVED.
 */

using System;

namespace UWAutShell
{
	class MainClass
	{
		// Program Control
		enum ProgramState {
			HUMAN_INTERACTION_MODE,
			PROGRAM_CONTROL_MODE
		}

		static ProgramState currentProgramState;

		// Command States
		enum Command {
			QUIT_PROGRAM,
			START_OF_PROGRAM,
		};

		static Command currentInputtedCommandState;

		/* Program Variables */

		// Constants
		const int minimumValueForSelectingRandomInteger = 0;

		static readonly String[] helloMessages = {"Waaah!!!",
			"Hi... :(",
			"What do you want?!",
			"I... waah! Want solitude!",
			"GO AWAY!!",
			"(punches you on your stomach)",
			"I want to be alone! ALONE!!",
			"(throws a tantrum at you)",
			"Back off!",
			"No... NO! (thows something at you)",
			"You... (slaps you on the face) AUGH!",
		};

		static readonly String[] goodbyeMessages = {"What?! ARE YOU SURE YOU WANT TO QUIT?!!",
			"No! Please don't GO!!",
			"I refuse you to leave.",
			"OK...",
			"Really?!",
			"You must stay with me! Please, don't go!",
			"No! You must not leave. Be with me FOREVER.",
			"I'll eradicate you if you depart!",
			"I request you to retain your spot with me. C'mon!",
			"Oh, come on, stay with me!",
			"No. WAAAHHH! (stars shedding tears)",
			"You mustn\'t go!",
			"Be with me or I\'ll hurt you.",
			"Listen to me; I\'m the boss, so you must do what I tell you. And by that, I mean stay with me RIGHT NOW.",
		};

		static readonly String[] goodbyeYesMessages = {
			"Aww!! I am distraught!",
			"Goodbye (whining)...",
			"Please don\'t go!!! (crying)",
			"Bye!",
			"I won\'t forget you. I miss you already!!!",
			"GO AWAY. I hate you forever."
		};

		static readonly String[] goodbyeNoMessages = {
			"You\'re stuck with me??! Yes!",
			"Thank goodness!",
			"Yay!!",
			"I\'m happy you are with me!",
			"Yes; whoo-hoo!",
			"Alright..."
		};

		static readonly String[] goodbyeCommandUnrecognizableOptionMessages = {
			"Huh?!",
			"What did you say?",
			"?",
			"...",
			"Repeat your communication.",
			"I do not understand your comment. Repeat."
		};




		// Varied Variables
		static int randomMaxValue = 25;





		/* Program Components */
		static Random choiceIndexGenerator;


		public static void Main (string[] args)
		{
			initializeProgram();
			printFirstFewLines();
			while (currentInputtedCommandState != Command.QUIT_PROGRAM)
				acceptCommand ();
		}

		// Initialize program
		public static void initializeProgram()
		{
			choiceIndexGenerator = new Random();


			currentProgramState = ProgramState.HUMAN_INTERACTION_MODE;
			currentInputtedCommandState = Command.START_OF_PROGRAM;
		}

		// Intro to program
		public static void printFirstFewLines()
		{
			Console.WriteLine ("UW AutShell\n" +
			                   "By Gregory Desrosiers, Software Engineering 2A, University of Waterloo\n" +
			                   "November 2015\n\n" +
			                   "Inspired by Dave Pagurek Van Mossel\'s YesNoMaybe project\n" +
			                   "(https://github.com/davepagurek/YNM)\n\n" +
			                   "© 2015 University of Waterloo.\nSource Code © 2015 Gregory Desrosiers. All rights reserved.\n\n");
		}

		public static void acceptCommand()
		{
			// Output to tell user about accepting a command.
			Console.Write (">= ");
			
			String currentStringLine = Console.ReadLine (), currentStringLineInLowercase = currentStringLine.ToLower();

			if (currentStringLine.Length > 0 && currentProgramState == ProgramState.HUMAN_INTERACTION_MODE)
			{
				if (currentStringLineInLowercase.StartsWith("switchcontrol"))
					switchControl();
				else if (currentStringLineInLowercase.StartsWith("hello"))
					executeHelloCommand();
				else if (currentStringLineInLowercase.StartsWith("question"))
				{
					Console.WriteLine ("question");
				}
				else if (currentStringLineInLowercase.StartsWith("bye") ||
				         currentStringLineInLowercase.StartsWith("goodbye") )
					executeGoodbyeCommand();
				else if (currentStringLineInLowercase.StartsWith("go"))
				{
					Console.WriteLine ("go");
				}
				else if (currentStringLineInLowercase.StartsWith("don\'t") ||
				         currentStringLineInLowercase.StartsWith("do not"))
				{
					Console.WriteLine ("don\'t / do not");
				}
				else if (currentStringLineInLowercase.StartsWith("do"))
				{
					Console.WriteLine ("do");
				}
				else if (currentStringLineInLowercase.StartsWith("help"))
				{
					Console.WriteLine ("help");
				}
				else if (currentStringLineInLowercase.StartsWith("timeout"))
				{
					Console.WriteLine ("timeout");
				}
				else if (currentStringLineInLowercase.StartsWith("sleep"))
				{
					Console.WriteLine ("sleep");
				}
				else if (currentStringLineInLowercase.StartsWith("read"))
				{
					Console.WriteLine ("read");
				}
				else
				{
					Console.WriteLine ("Unrecognizable token");
				}
				
			}
			else if (currentStringLine.Length > 0 && currentProgramState == ProgramState.PROGRAM_CONTROL_MODE)
			{
				if (currentStringLineInLowercase.StartsWith("switchcontrol"))
					switchControl();
				else if (currentStringLineInLowercase.StartsWith("set"))
				{
					Console.WriteLine ("set");
				}
				else if (currentStringLineInLowercase.StartsWith("foreground"))
				{
					String[] arguments = currentStringLineInLowercase.Split (' ');

					if (arguments.Length != 2)
						Console.WriteLine ("Usage: foreground <color>\n<color> is a string representing the foreground color you want.");
					else
						setForegroundColor(arguments[1]);
				}
				else if (currentStringLineInLowercase.StartsWith("background"))
				{
					String[] arguments = currentStringLineInLowercase.Split (' ');

					if (arguments.Length != 2)
						Console.WriteLine ("Usage: foreground <color>\n<color> is a string representing the foreground color you want.");
					else
						setBackgroundColor(arguments[1]);
				}
				else if (currentStringLineInLowercase.StartsWith("help"))
				{
					Console.WriteLine ("help");
				}
				else
					Console.WriteLine ("Command \"" + currentStringLine + "\" is unrecognized. " +
					"Please type the \'help\' command for details on what commands you can use.");
			}
		}


		public static void switchControl()
		{
			if (currentProgramState == ProgramState.HUMAN_INTERACTION_MODE)
			{
				currentProgramState = ProgramState.PROGRAM_CONTROL_MODE;
				Console.WriteLine ("Program control mode enabled.");
			}
			else if (currentProgramState == ProgramState.PROGRAM_CONTROL_MODE)
			{
				Console.WriteLine ("Program control mode disabled.");
				currentProgramState = ProgramState.HUMAN_INTERACTION_MODE;
			}
		}


		public static void executeHelloCommand()
		{
			int randomValue = choiceIndexGenerator.Next(minimumValueForSelectingRandomInteger, randomMaxValue);
			float decimalToMax = (float)randomValue / (float)randomMaxValue;

			if (decimalToMax >= 0.80f)
			{
				randomValue = choiceIndexGenerator.Next (minimumValueForSelectingRandomInteger, helloMessages.Length);
				Console.WriteLine (helloMessages[randomValue]);
			}
		}

		// Sets the highlight of the text to be displayed on the console.
		public static void setBackgroundColor(String backgroundColorString)
		{
			string choiceString =  backgroundColorString.ToLower();
			
			if (choiceString.Equals ("black"))
				Console.BackgroundColor = ConsoleColor.Black;
			else if (choiceString.Equals ("blue"))
				Console.BackgroundColor = ConsoleColor.Blue;
			else if (choiceString.Equals ("cyan"))
				Console.BackgroundColor = ConsoleColor.Cyan;
			else if (choiceString.Equals ("darkblue"))
				Console.BackgroundColor = ConsoleColor.DarkBlue;
			else if (choiceString.Equals ("darkcyan"))
				Console.BackgroundColor = ConsoleColor.DarkCyan;
			else if (choiceString.Equals ("darkgray"))
				Console.BackgroundColor = ConsoleColor.DarkGray;
			else if (choiceString.Equals ("darkgreen"))
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			else if (choiceString.Equals ("darkmagneta"))
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
			else if (choiceString.Equals ("darkred"))
				Console.BackgroundColor = ConsoleColor.DarkRed;
			else if (choiceString.Equals ("darkyellow"))
				Console.BackgroundColor = ConsoleColor.DarkYellow;
			else if (choiceString.Equals ("gray"))
				Console.BackgroundColor = ConsoleColor.Gray;
			else if (choiceString.Equals ("green"))
				Console.BackgroundColor = ConsoleColor.Green;
			else if (choiceString.Equals ("magneta"))
				Console.BackgroundColor = ConsoleColor.Magenta;
			else if (choiceString.Equals ("red"))
				Console.BackgroundColor = ConsoleColor.Red;
			else if (choiceString.Equals ("white"))
				Console.BackgroundColor = ConsoleColor.White;
			else if (choiceString.Equals ("yellow"))
				Console.BackgroundColor = ConsoleColor.Yellow;
			else
				Console.WriteLine ("The background color argument \"" + backgroundColorString + "\" is unrecognized.\n\n"+
				                   "You may enter one of the following colors:\n" +
				                   "Command - Background Color\n\n" +
				                   "black - Black\n" +
				                   "blue - Blue\n" +
				                   "cyan - Cyan\n" +
				                   "darkblue - Dark Blue\n" +
				                   "darkcyan - Dark Cyan\n" +
				                   "darkgray - Dark Gray\n" +
				                   "darkgreen - Dark Green\n" +
				                   "darkmagneta - Dark Magneta\n" +
				                   "darkred - Dark Red\n" +
				                   "darkyellow - Dark Yellow\n" +
				                   "gray - Gray\n" +
				                   "green - Green\n" +
				                   "magneta - Magneta\n" +
				                   "red - Red\n" +
				                   "white - White\n" +
				                   "yellow - Yellow\n");
		}
		
		public static void setForegroundColor(String foregroundColorString)
		{
			string choiceString =  foregroundColorString.ToLower();
			
			if (choiceString.Equals ("black"))
				Console.ForegroundColor = ConsoleColor.Black;
			else if (choiceString.Equals ("blue"))
				Console.ForegroundColor = ConsoleColor.Blue;
			else if (choiceString.Equals ("cyan"))
				Console.ForegroundColor = ConsoleColor.Cyan;
			else if (choiceString.Equals ("darkblue"))
				Console.ForegroundColor = ConsoleColor.DarkBlue;
			else if (choiceString.Equals ("darkcyan"))
				Console.ForegroundColor = ConsoleColor.DarkCyan;
			else if (choiceString.Equals ("darkgray"))
				Console.ForegroundColor = ConsoleColor.DarkGray;
			else if (choiceString.Equals ("darkgreen"))
				Console.ForegroundColor = ConsoleColor.DarkGreen;
			else if (choiceString.Equals ("darkmagneta"))
				Console.ForegroundColor = ConsoleColor.DarkMagenta;
			else if (choiceString.Equals ("darkred"))
				Console.ForegroundColor = ConsoleColor.DarkRed;
			else if (choiceString.Equals ("darkyellow"))
				Console.ForegroundColor = ConsoleColor.DarkYellow;
			else if (choiceString.Equals ("gray"))
				Console.ForegroundColor = ConsoleColor.Gray;
			else if (choiceString.Equals ("green"))
				Console.ForegroundColor = ConsoleColor.Green;
			else if (choiceString.Equals ("magneta"))
				Console.ForegroundColor = ConsoleColor.Magenta;
			else if (choiceString.Equals ("red"))
				Console.ForegroundColor = ConsoleColor.Red;
			else if (choiceString.Equals ("white"))
				Console.ForegroundColor = ConsoleColor.White;
			else if (choiceString.Equals ("yellow"))
				Console.ForegroundColor = ConsoleColor.Yellow;
			else
				Console.WriteLine ("The foreground color argument \"" + foregroundColorString + "\" is unrecognized.\n\n"+
				                   "You may enter one of the following colors:\n" +
				                   "Command - Background Color\n\n" +
				                   "black - Black\n" +
				                   "blue - Blue\n" +
				                   "cyan - Cyan\n" +
				                   "darkblue - Dark Blue\n" +
				                   "darkcyan - Dark Cyan\n" +
				                   "darkgray - Dark Gray\n" +
				                   "darkgreen - Dark Green\n" +
				                   "darkmagneta - Dark Magneta\n" +
				                   "darkred - Dark Red\n" +
				                   "darkyellow - Dark Yellow\n" +
				                   "gray - Gray\n" +
				                   "green - Green\n" +
				                   "magneta - Magneta\n" +
				                   "red - Red\n" +
				                   "white - White\n" +
				                   "yellow - Yellow\n");
		}

		public static void executeGoodbyeCommand()
		{
			int randomValue = choiceIndexGenerator.Next (minimumValueForSelectingRandomInteger, goodbyeMessages.Length);
			Console.WriteLine (goodbyeMessages[randomValue]);
			string choice;

			do
			{
				Console.Write("Are you sure you want to quit the program?\n> ");
				choice = Console.ReadLine ();
				choice = choice.ToLower();

				if (choice.Equals("yes"))
				{
					randomValue = choiceIndexGenerator.Next (minimumValueForSelectingRandomInteger, goodbyeYesMessages.Length);
					Console.WriteLine (goodbyeYesMessages[randomValue]);
					currentInputtedCommandState = Command.QUIT_PROGRAM;
				}
				else if (choice.Equals("no"))
				{
					randomValue = choiceIndexGenerator.Next (minimumValueForSelectingRandomInteger, goodbyeNoMessages.Length);
					Console.WriteLine (goodbyeNoMessages[randomValue]);
				}
				else
				{
					randomValue = choiceIndexGenerator.Next (minimumValueForSelectingRandomInteger, goodbyeCommandUnrecognizableOptionMessages.Length);
					Console.WriteLine (goodbyeCommandUnrecognizableOptionMessages[randomValue]);
				}

			} while(!choice.Equals("yes") && !choice.Equals("no"));

		}
	}
}
