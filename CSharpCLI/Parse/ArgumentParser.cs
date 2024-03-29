﻿////////////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2010 Bernard Badjari
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Globalization;
using CSharpCLI.Argument;
using CSharpCLI.Properties;

namespace CSharpCLI.Parse
{
	/// <summary>
	/// Parses command-line arguments.
	/// </summary>
	public class ArgumentParser
	{
		/// <summary>
		/// First argument number.
		/// </summary>
		private const int FirstArgument = 1;

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="arguments">
		/// Array of strings representing command-line arguments.
		/// </param>
		/// <param name="switches">
		/// SwitchCollection representing collection of switches expected to be parsed from given command-line arguments.
		/// </param>
		public ArgumentParser(string[] arguments, SwitchCollection switches)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (switches == null)
				throw new ArgumentNullException(nameof(switches));

			Arguments = arguments;
			ParsedSwitches = new SwitchCollection();
			Switches = switches;
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Determine if all switches with given names parsed.
		/// </summary>
		/// <param name="names">
		/// Array of strings representing switch names.
		/// </param>
		/// <returns>
		/// True if all switches with given names parsed, false otherwise.
		/// </returns>
		public bool AllParsed(params string[] names)
		{
			if (names == null || names.Length == 0)
				return false;

			foreach (string name in names)
			{
				if (!IsParsed(name))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Determine if any switches with given names parsed.
		/// </summary>
		/// <param name="names">
		/// Array of strings representing switch names.
		/// </param>
		/// <returns>
		/// True if any switches with given names parsed, false otherwise.
		/// </returns>
		public bool AnyParsed(params string[] names)
		{
			if (names == null || names.Length == 0)
				return false;

			return !NoneParsed(names);
		}

		/// <summary>
		///		<para>
		///		Get first argument value from switch with given name.
		///		</para>
		///		<para>
		///		If switch not parsed, return null.
		///		</para>
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		///		<para>
		///		String representing first argument value from switch with given name.
		///		</para>
		///		<para>
		///		Null if switch with given name not parsed.
		///		</para>
		/// </returns>
		public string GetValue(string name)
		{
			return GetValue(name, FirstArgument);
		}

		/// <summary>
		/// Get argument value from switch with given name and argument number.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="argumentNumber">
		/// Integer representing argument number.
		/// </param>
		/// <returns>
		///		<para>
		///		String representing argument value from switch with given name and argument number.
		///		</para>
		///		<para>
		///		Null if switch with given name not parsed or given argument number invalid.
		///		</para>
		/// </returns>
		public string GetValue(string name, int argumentNumber)
		{
			string[] values = GetValues(name);

			string value = null;

			if (values != null)
			{
				if (argumentNumber >= FirstArgument && argumentNumber <= values.Length)
					value = values[argumentNumber - FirstArgument];
			}

			return value;
		}

		/// <summary>
		/// Get argument values from switch with given name.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		///		<para>
		///		Array of strings representing argument values from switch with given name.
		///		</para>
		///		<para>
		///		Null if switch with given name not parsed.
		///		</para>
		/// </returns>
		public string[] GetValues(string name)
		{
			string[] values = null;

			if (IsParsed(name))
			{
				Switch parsedSwitch = ParsedSwitches[name];

				values = parsedSwitch.GetArgumentValues();
			}

			return values;
		}

		/// <summary>
		/// Determine if switch with given name parsed.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		/// True if switch with given name parsed, false otherwise.
		/// </returns>
		public bool IsParsed(string name)
		{
			return ParsedSwitches.HasSwitch(name);
		}

		/// <summary>
		/// Determine if no switches with given names parsed.
		/// </summary>
		/// <param name="names">
		/// Array of strings representing switch names.
		/// </param>
		/// <returns>
		/// True if no switches with given names parsed, false otherwise.
		/// </returns>
		public bool NoneParsed(params string[] names)
		{
			if (names == null || names.Length == 0)
				return false;

			foreach (string name in names)
			{
				if (IsParsed(name))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Parse command-line arguments.
		/// </summary>
		public void Parse()
		{
			ParsedSwitches.Clear();

			for (int index = 0; index < Arguments.Length; index++)
			{
				string argument = Arguments[index];

				if (Switch.IsValid(argument))
				{
					string switchName = Switch.GetName(argument);

					if (!Switches.HasSwitch(switchName))
						ThrowParsingException(ExceptionMessages.UndefinedSwitch, switchName);

					if (IsParsed(switchName))
						ThrowParsingException(ExceptionMessages.SwitchAlreadyParsed, switchName);

					Switch parsedSwitch = Switches[switchName];

					ParsedSwitches.Add(parsedSwitch);

					if (parsedSwitch.HasArguments)
					{
						for (index++; index < Arguments.Length; index++)
						{
							string argumentValue = Arguments[index];

							if (Switch.IsValid(argumentValue))
							{
								// Parse this switch again.
								index--;

								break;
							}

							parsedSwitch.AddArgumentValue(argumentValue);
						}

						if (!parsedSwitch.HasEnoughValues)
							ThrowParsingException(ExceptionMessages.SwitchMissingArgument, switchName);
					}
				}
			}

			foreach (Switch currentSwitch in Switches.Switches)
			{
				if (currentSwitch.IsRequired && !IsParsed(currentSwitch.Name))
					ThrowParsingException(ExceptionMessages.RequiredSwitchMissing, currentSwitch.Name);
			}
		}

		/// <summary>
		/// Throw ParsingException with given message and switch name.
		/// </summary>
		/// <param name="message">
		/// String representing error message.
		/// </param>
		/// <param name="name">
		/// String representing switch name to use in given error message.
		/// </param>
		private static void ThrowParsingException(string message, string name)
		{
			string formattedMessage = string.Format(CultureInfo.CurrentCulture, message, name);

			throw new ParsingException(formattedMessage);
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Get number of switches parsed.
		/// </summary>
		/// <value>
		/// Integer representing number of switches parsed.
		/// </value>
		public int NumberSwitchesParsed
		{
			get { return ParsedSwitches.Count; }
		}

		/// <summary>
		/// Get/set command-line arguments to parse.
		/// </summary>
		/// <value>
		/// Array of strings representing command-line arguments to parse.
		/// </value>
		private string[] Arguments { get; set; }

		/// <summary>
		/// Get/set switches parsed from command-line arguments, accessed by their name.
		/// </summary>
		/// <value>
		/// SwitchCollection representing switches parsed from command-line arguments.
		/// </value>
		private SwitchCollection ParsedSwitches { get; set; }

		/// <summary>
		/// Get/set expected switches to parse from command-line arguments.
		/// </summary>
		/// <value>
		/// SwitchCollection representing expected switches to parse from command-line arguments.
		/// </value>
		private SwitchCollection Switches { get; set; }
	}
}
