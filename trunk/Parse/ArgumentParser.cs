////////////////////////////////////////////////////////////////////////////////
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

namespace CSharpCLI.Parse
{
	/// <summary>
	/// Parses command-line arguments.
	/// </summary>
	public class ArgumentParser
	{
		/// <summary>
		/// Error messages.
		/// </summary>
		static class Messages
		{
			public const string RequiredSwitchMissing = "Required switch '{0}' not found.";
			public const string SwitchAlreadyParsed = "Switch '{0}' already parsed.";
			public const string SwitchMissingArgument = "Missing argument for switch '{0}'.";
			public const string UndefinedSwitch = "Switch '{0}' undefined.";
		}

		/// <summary>
		/// First argument number.
		/// </summary>
		const int FirstArgument = 1;

		////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Command-line arguments to parse.
		/// </summary>
		string[] m_arguments;

		/// <summary>
		/// Switches parsed from command-line arguments, accessed by their name.
		/// </summary>
		SwitchCollection m_parsedSwitches;

		/// <summary>
		/// Expected switches to parse from command-line arguments.
		/// </summary>
		SwitchCollection m_switches;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ArgumentParser(string[] arguments, SwitchCollection switches)
		{
			m_arguments = arguments;
			m_parsedSwitches = new SwitchCollection();
			m_switches = switches;
		}

		////////////////////////////////////////////////////////////////////////
		// Public Methods

		/// <summary>
		/// Determine if all switches with given names parsed.
		/// </summary>
		public bool AllParsed(params string[] names)
		{
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
		public bool AnyParsed(params string[] names)
		{
			return !NoneParsed(names);
		}

		/// <summary>
		/// Get first argument value from switch with given name.
		/// 
		/// If switch not parsed, return null.
		/// </summary>
		public string GetValue(string name)
		{
			return GetValue(name, FirstArgument);
		}

		/// <summary>
		/// Get argument value from switch with given name and argument number.
		/// 
		/// If switch not parsed or invalid argument number, return null.
		/// </summary>
		public string GetValue(string name, int argumentNumber)
		{
			string[] values = GetValues(name);

			string value = null;

			if (values != null)
			{
				if (argumentNumber > 0 && argumentNumber <= values.Length)
					value = values[argumentNumber - 1];
			}

			return value;
		}

		/// <summary>
		/// Get argument values from switch with given name.
		/// 
		/// If switch not parsed, return null.
		/// </summary>
		public string[] GetValues(string name)
		{
			string[] values = null;

			if (IsParsed(name))
			{
				Switch parsedSwitch = m_parsedSwitches[name];

				values = parsedSwitch.GetArgumentValues();
			}

			return values;
		}

		/// <summary>
		/// Determine if switch with given name parsed.
		/// </summary>
		public bool IsParsed(string name)
		{
			return m_parsedSwitches.HasSwitch(name);
		}

		/// <summary>
		/// Determine if no switches with given names parsed.
		/// </summary>
		public bool NoneParsed(params string[] names)
		{
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
			m_parsedSwitches.Clear();

			for (int index = 0; index < m_arguments.Length; index++)
			{
				string argument = m_arguments[index];

				if (Switch.IsValid(argument))
				{
					string switchName = Switch.GetName(argument);

					if (!m_switches.HasSwitch(switchName))
						ThrowParsingException(Messages.UndefinedSwitch, switchName);

					if (IsParsed(switchName))
						ThrowParsingException(Messages.SwitchAlreadyParsed, switchName);

					Switch parsedSwitch = m_switches[switchName];

					m_parsedSwitches.Add(parsedSwitch);

					if (parsedSwitch.HasArguments)
					{
						for (index++; index < m_arguments.Length; index++)
						{
							string argumentValue = m_arguments[index];

							if (Switch.IsValid(argumentValue))
							{
								// Parse this switch again.
								index--;

								break;
							}

							parsedSwitch.AddArgumentValue(argumentValue);
						}

						if (!parsedSwitch.HasEnoughValues)
							ThrowParsingException(Messages.SwitchMissingArgument, switchName);
					}
				}
			}

			foreach (Switch currentSwitch in m_switches.Switches)
			{
				if (currentSwitch.IsRequired && !IsParsed(currentSwitch.Name))
					ThrowParsingException(Messages.RequiredSwitchMissing, currentSwitch.Name);
			}
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Throw ParsingException with given message and switch name.
		/// </summary>
		static void ThrowParsingException(string message, string name)
		{
			string formattedMessage = string.Format(CultureInfo.CurrentCulture,
				message, name);

			throw new ParsingException(formattedMessage);
		}
	}
}
