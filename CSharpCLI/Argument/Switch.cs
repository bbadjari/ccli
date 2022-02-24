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
using System.Collections.Generic;

namespace CSharpCLI.Argument
{
	/// <summary>
	/// Pre-defined command-line argument.
	/// </summary>
	public class Switch
	{
		/// <summary>
		/// Long prefix that identifies command-line argument as switch.
		/// </summary>
		public const string LongPrefix = "--";

		/// <summary>
		/// Prefix that identifies command-line argument as switch.
		/// </summary>
		public const string Prefix = "-";

		/// <summary>
		/// Error messages.
		/// </summary>
		private static class Messages
		{
			public const string InvalidName = "Invalid switch name.";
		}

		/// <summary>
		/// No arguments expected to follow switch.
		/// </summary>
		private const int NoArguments = 0;

		/// <summary>
		/// Switch not required (optional).
		/// </summary>
		private const bool NotRequired = false;

		/// <summary>
		/// One argument expected to follow switch.
		/// </summary>
		private const int OneArgument = 1;

		/// <summary>
		/// Prefixes that can identify command-line argument as switch.
		/// </summary>
		private static readonly string[] Prefixes = new string[]
		{
			LongPrefix, Prefix
		};

		/// <summary>
		/// Unknown number of arguments expected to follow switch.
		/// </summary>
		private const int UnknownNumberArguments = int.MaxValue;

		////////////////////////////////////////////////////////////////////////

		private List<string> argumentNames;

		private List<string> argumentValues;

		////////////////////////////////////////////////////////////////////////
		// Constructors - Printable

		/// <summary>
		/// Constructor for specifying switch description.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		public Switch(string name, string description)
			: this(name, null, description)
		{
		}

		/// <summary>
		/// Constructor for specifying switch long name.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		public Switch(string name, string longName, string description)
			: this(name, longName, description, NotRequired)
		{
		}

		/// <summary>
		/// Constructor for specifying whether switch required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public Switch(string name, string longName, string description,
			bool isRequired)
			: this(name, longName, description, NoArguments, isRequired)
		{
		}

		/// <summary>
		/// Constructor for specifying arguments expected to follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="hasArguments">
		/// True if arguments expected to follow switch, false otherwise.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public Switch(string name, string longName, string description,
			bool hasArguments, bool isRequired)
			: this(name, longName, description, UnknownNumberArguments,
				isRequired)
		{
			HasArguments = hasArguments;
		}

		/// <summary>
		/// Constructor for defining number of arguments expected to
		/// follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		public Switch(string name, string longName, string description,
			int numberArguments)
			: this(name, longName, description, numberArguments, NotRequired)
		{
		}

		/// <summary>
		/// Constructor for defining number of arguments expected to
		/// follow switch and whether switch required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public Switch(string name, string longName, string description,
			int numberArguments, bool isRequired)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException(Messages.InvalidName);

			argumentNames = new List<string>();
			argumentValues = new List<string>();
			Description = description;
			HasArguments = numberArguments > NoArguments;
			IsRequired = isRequired;
			LongName = longName == name ? null : longName;
			Name = name;
			NumberArguments = HasArguments ? numberArguments : NoArguments;
		}

		/// <summary>
		/// Constructor for specifying arguments with given name expected to
		/// follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="hasArguments">
		/// True if arguments expected to follow switch, false otherwise.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		/// <param name="argumentName">
		/// String representing name of arguments expected to follow switch.
		/// </param>
		public Switch(string name, string longName, string description,
			bool hasArguments, bool isRequired, string argumentName)
			: this(name, longName, description, hasArguments, isRequired)
		{
			if (argumentName != null && HasArguments)
				argumentNames.Add(argumentName);
		}

		/// <summary>
		/// Constructor for defining argument name expected to follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		/// <param name="argumentName">
		/// String representing name of single argument expected to follow
		/// switch.
		/// </param>
		public Switch(string name, string longName, string description,
			bool isRequired, string argumentName)
			: this(name, longName, description, isRequired)
		{
			if (argumentName != null)
			{
				argumentNames.Add(argumentName);
				HasArguments = true;
				NumberArguments = OneArgument;
			}
		}

		/// <summary>
		/// Constructor for defining argument names expected to follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		/// <param name="argumentNames">
		/// Array of strings representing names of arguments expected to follow
		/// switch.
		/// </param>
		public Switch(string name, string longName, string description,
			bool isRequired, string[] argumentNames)
			: this(name, longName, description, isRequired)
		{
			if (argumentNames != null)
			{
				foreach (string argumentName in argumentNames)
				{
					if (argumentName != null)
						this.argumentNames.Add(argumentName);
				}

				NumberArguments = NumberArgumentNames;
				HasArguments = NumberArguments > NoArguments;
			}
		}

		////////////////////////////////////////////////////////////////////////
		// Constructors - Non-Printable

		/// <summary>
		/// Constructor for specifying switch name.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		public Switch(string name)
			: this(name, null)
		{
		}

		/// <summary>
		/// Constructor for specifying whether switch required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public Switch(string name, bool isRequired)
			: this(name, null, null, NoArguments, isRequired)
		{
		}

		/// <summary>
		/// Constructor for specifying arguments expected to follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="hasArguments">
		/// True if arguments expected to follow switch, false otherwise.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public Switch(string name, bool hasArguments, bool isRequired)
			: this(name, null, null, (hasArguments ? UnknownNumberArguments : NoArguments), isRequired)
		{
		}

		/// <summary>
		/// Constructor for defining number of arguments expected to
		/// follow switch.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		public Switch(string name, int numberArguments)
			: this(name, null, null, numberArguments, NotRequired)
		{
		}

		/// <summary>
		/// Constructor for defining number of arguments expected to
		/// follow switch and whether switch required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public Switch(string name, int numberArguments, bool isRequired)
			: this(name, null, null, numberArguments, isRequired)
		{
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Add name of argument expected to follow this switch.
		/// </summary>
		/// <param name="name">
		/// String representing argument name.
		/// </param>
		public void AddArgumentName(string name)
		{
			if (HasAllNames)
				return;

			if (!string.IsNullOrWhiteSpace(name))
				argumentNames.Add(name);
		}

		/// <summary>
		/// Add value of argument expected to follow this switch.
		/// </summary>
		/// <param name="value">
		/// String representing argument value.
		/// </param>
		public void AddArgumentValue(string value)
		{
			if (HasAllValues)
				return;

			if (!string.IsNullOrWhiteSpace(value))
				argumentValues.Add(value);
		}

		/// <summary>
		/// Get names of arguments following switch.
		/// </summary>
		/// <returns>
		/// Array of strings representing argument names.
		/// </returns>
		public string[] GetArgumentNames()
		{
			return argumentNames.ToArray();
		}

		/// <summary>
		/// Get values of arguments following switch.
		/// </summary>
		/// <returns>
		/// Array of strings representing argument values.
		/// </returns>
		public string[] GetArgumentValues()
		{
			return argumentValues.ToArray();
		}

		/// <summary>
		/// Get given switch name with long prefix.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		/// String representing switch name with long prefix.
		/// </returns>
		public static string GetLongPrefixedName(string name)
		{
			return GetPrefixedName(name, LongPrefix);
		}

		/// <summary>
		/// Get switch name given command-line argument, which may include prefix.
		/// </summary>
		/// <param name="argument">
		/// String representing command-line argument.
		/// </param>
		/// <returns>
		/// String representing switch name.
		/// </returns>
		public static string GetName(string argument)
		{
			if (!string.IsNullOrWhiteSpace(argument))
			{
				foreach (string prefix in Prefixes)
				{
					if (argument.StartsWith(prefix, StringComparison.Ordinal))
						return argument.Substring(prefix.Length);
				}
			}

			return argument;
		}

		/// <summary>
		/// Get given switch name with prefix.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		/// String representing switch name with prefix.
		/// </returns>
		public static string GetPrefixedName(string name)
		{
			return GetPrefixedName(name, Prefix);
		}

		/// <summary>
		/// Determine if given command-line argument is valid switch.
		/// </summary>
		/// <param name="argument">
		/// String representing command-line argument.
		/// </param>
		/// <returns>
		/// True if valid switch, false otherwise.
		/// </returns>
		public static bool IsValid(string argument)
		{
			string switchName = GetName(argument);

			if (switchName == argument)
			{
				// Command-line argument did not contain prefix.
				return false;
			}

			// Valid if switch contains no other prefixes.
			return !HasPrefix(switchName);
		}

		/// <summary>
		/// Get given switch name with given prefix.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="prefix">
		/// String representing switch prefix.
		/// </param>
		/// <returns>
		/// String representing switch name with given prefix.
		/// </returns>
		private static string GetPrefixedName(string name, string prefix)
		{
			if (string.IsNullOrWhiteSpace(name) || HasPrefix(name))
				return name;

			return prefix + name;
		}

		/// <summary>
		/// Determine if given number of arguments represents all those for
		/// this switch.
		/// </summary>
		/// <param name="numberArguments">
		/// Integer representing number of arguments.
		/// </param>
		/// <returns>
		/// True if given number of arguments represents all those for switch,
		/// false otherwise.
		/// </returns>
		private bool HasAllArguments(int numberArguments)
		{
			return HasArguments && (NumberArguments == numberArguments);
		}

		/// <summary>
		/// Determine if given number of arguments enough for this switch.
		/// </summary>
		/// <param name="numberArguments">
		/// Integer representing number of arguments.
		/// </param>
		/// <returns>
		/// True if given number of arguments enough for switch, false otherwise.
		/// </returns>
		private bool HasEnoughArguments(int numberArguments)
		{
			if (HasArguments)
			{
				if (NumberArguments <= numberArguments)
					return true;

				// Determine if at least one argument if number of arguments unknown.
				if (NumberArguments == UnknownNumberArguments && numberArguments > 0)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Determine if given argument has prefix.
		/// </summary>
		/// <param name="argument">
		/// String representing command-line argument.
		/// </param>
		/// <returns>
		/// True if given argument has prefix, false otherwise.
		/// </returns>
		private static bool HasPrefix(string argument)
		{
			return argument != GetName(argument);
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Get switch description.
		/// </summary>
		/// <value>
		/// String representing switch description.
		/// </value>
		public string Description { get; private set; }

		/// <summary>
		/// Determine if all argument names added to this switch.
		/// </summary>
		/// <value>
		/// True if all argument names added to this switch, false otherwise.
		/// </value>
		public bool HasAllNames
		{
			get { return HasAllArguments(NumberArgumentNames); }
		}

		/// <summary>
		/// Determine if all argument values added to this switch.
		/// </summary>
		/// <value>
		/// True if all argument values added to this switch, false otherwise.
		/// </value>
		public bool HasAllValues
		{
			get { return HasAllArguments(NumberArgumentValues); }
		}

		/// <summary>
		/// Determine if arguments expected to follow this switch.
		/// </summary>
		/// <value>
		/// True if arguments expected to follow this switch, false otherwise.
		/// </value>
		public bool HasArguments { get; private set; }

		/// <summary>
		/// Determine if switch has description.
		/// </summary>
		/// <value>
		/// True if switch has description, false otherwise.
		/// </value>
		public bool HasDescription
		{
			get { return !string.IsNullOrWhiteSpace(Description); }
		}

		/// <summary>
		/// Determine if enough argument names added to this switch.
		/// </summary>
		/// <value>
		/// True if enough argument names added to this switch, false otherwise.
		/// </value>
		public bool HasEnoughNames
		{
			get { return HasEnoughArguments(NumberArgumentNames); }
		}

		/// <summary>
		/// Determine if enough argument values added to this switch.
		/// </summary>
		/// <value>
		/// True if enough argument values added to this switch, false otherwise.
		/// </value>
		public bool HasEnoughValues
		{
			get { return HasEnoughArguments(NumberArgumentValues); }
		}

		/// <summary>
		/// Determine if switch has long name.
		/// </summary>
		/// <value>
		/// True if switch has long name, false otherwise.
		/// </value>
		public bool HasLongName
		{
			get { return !string.IsNullOrWhiteSpace(LongName); }
		}

		/// <summary>
		/// Determine if non-printable switch (no long name or description).
		/// </summary>
		/// <value>
		/// True if non-printable switch, false otherwise.
		/// </value>
		public bool IsNonPrintable
		{
			get { return !HasLongName && !HasDescription; }
		}

		/// <summary>
		/// Determine if optional switch.
		/// </summary>
		/// <value>
		/// True if optional switch, false otherwise.
		/// </value>
		public bool IsOptional
		{
			get { return !IsRequired; }
		}

		/// <summary>
		/// Determine if required switch.
		/// </summary>
		/// <value>
		/// True if required switch, false otherwise.
		/// </value>
		public bool IsRequired { get; private set; }

		/// <summary>
		/// Get switch long name.
		/// </summary>
		/// <value>
		/// String representing switch long name.
		/// </value>
		public string LongName { get; private set; }

		/// <summary>
		/// Get switch name.
		/// </summary>
		/// <value>
		/// String representing switch name.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Get number of argument names added to this switch.
		/// </summary>
		/// <value>
		/// Integer representing number of argument names added to this switch.
		/// </value>
		public int NumberArgumentNames
		{
			get { return argumentNames.Count; }
		}

		/// <summary>
		/// Get number of argument values added to this switch.
		/// </summary>
		/// <value>
		/// Integer representing number of argument values added to this switch.
		/// </value>
		public int NumberArgumentValues
		{
			get { return argumentValues.Count; }
		}

		/// <summary>
		/// Get number of arguments expected to follow this switch.
		/// </summary>
		/// <value>
		/// Integer representing number of arguments expected to follow this
		/// switch.
		/// </value>
		public int NumberArguments { get; private set; }
	}
}
