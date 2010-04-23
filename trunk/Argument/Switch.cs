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

		////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// No arguments expected to follow switch.
		/// </summary>
		const int NoArguments = 0;

		/// <summary>
		/// Switch not required (optional).
		/// </summary>
		const bool NotRequired = false;

		/// <summary>
		/// One argument expected to follow switch.
		/// </summary>
		const int OneArgument = 1;

		/// <summary>
		/// Prefixes that can identify command-line argument as switch.
		/// </summary>
		static readonly string[] Prefixes = new string[]
		{
			LongPrefix, Prefix
		};

		/// <summary>
		/// Unknown number of arguments expected to follow switch.
		/// </summary>
		const int UnknownNumberArguments = int.MaxValue;

		////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Names of arguments following switch.
		/// </summary>
		List<string> m_argumentNames;

		/// <summary>
		/// Values of arguments following switch.
		/// </summary>
		List<string> m_argumentValues;

		/// <summary>
		/// Description of switch.
		/// </summary>
		string m_description;

		/// <summary>
		/// True if arguments following switch expected, false otherwise.
		/// </summary>
		bool m_hasArguments;

		/// <summary>
		/// True if switch is required, false if optional.
		/// </summary>
		bool m_isRequired;

		/// <summary>
		/// Long name of switch.
		/// </summary>
		string m_longName;

		/// <summary>
		/// Name of switch.
		/// </summary>
		string m_name;

		/// <summary>
		/// Number of arguments expected to follow switch.
		/// </summary>
		int m_numberArguments;

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Constructor for specifying switch name.
		/// </summary>
		public Switch(string name)
			: this(name, null)
		{
		}

		/// <summary>
		/// Constructor for specifying switch description.
		/// </summary>
		public Switch(string name, string description)
			: this(name, null, description, NoArguments, NotRequired)
		{
		}

		/// <summary>
		/// Constructor for specifying switch long name.
		/// </summary>
		public Switch(string name, string longName, string description)
			: this(name, longName, description, NoArguments, NotRequired)
		{
		}

		/// <summary>
		/// Constructor for specifying whether switch required.
		/// </summary>
		public Switch(string name, string longName, string description,
			bool isRequired)
			: this(name, longName, description, NoArguments, isRequired)
		{
		}

		/// <summary>
		/// Constructor for specifying arguments expected to follow switch.
		/// </summary>
		public Switch(string name, string longName, string description,
			bool hasArguments, bool isRequired)
			: this(name, longName, description, UnknownNumberArguments,
				isRequired)
		{
			m_hasArguments = hasArguments;
		}

		/// <summary>
		/// Constructor for defining number of arguments expected to
		/// follow switch.
		/// </summary>
		public Switch(string name, string longName, string description,
			int numberArguments, bool isRequired)
		{
			m_argumentNames = new List<string>();
			m_argumentValues = new List<string>();
			m_description = description;
			m_hasArguments = numberArguments > NoArguments;
			m_isRequired = isRequired;
			m_longName = longName == name ? null : longName;
			m_name = name;
			m_numberArguments = HasArguments ? numberArguments : NoArguments;
		}

		/// <summary>
		/// Constructor for specifying arguments with given name expected to
		/// follow switch.
		/// </summary>
		public Switch(string name, string longName, string description,
			bool hasArguments, bool isRequired, string argumentName)
			: this(name, longName, description, hasArguments, isRequired)
		{
			if (argumentName != null && HasArguments)
				m_argumentNames.Add(argumentName);
		}

		/// <summary>
		/// Constructor for defining argument name expected to follow switch.
		/// </summary>
		public Switch(string name, string longName, string description,
			bool isRequired, string argumentName)
			: this(name, longName, description, isRequired)
		{
			if (argumentName != null)
			{
				m_argumentNames.Add(argumentName);
				m_hasArguments = true;
				m_numberArguments = OneArgument;
			}
		}

		/// <summary>
		/// Constructor for defining argument names expected to follow switch.
		/// </summary>
		public Switch(string name, string longName, string description,
			bool isRequired, string[] argumentNames)
			: this(name, longName, description, isRequired)
		{
			if (argumentNames != null)
			{
				foreach (string argumentName in argumentNames)
				{
					if (argumentName != null)
						m_argumentNames.Add(argumentName);
				}

				m_numberArguments = m_argumentNames.Count;
				m_hasArguments = NumberArguments > NoArguments;
			}
		}

		////////////////////////////////////////////////////////////////////////
		// Public Methods

		/// <summary>
		/// Add name of argument expected to follow this switch.
		/// </summary>
		public void AddArgumentName(string name)
		{
			if (HasEnoughNames)
				return;

			if (name != null && name.Length > 0)
				m_argumentNames.Add(name);
		}

		/// <summary>
		/// Add value of argument expected to follow this switch.
		/// </summary>
		public void AddArgumentValue(string value)
		{
			if (HasEnoughValues)
				return;

			if (value != null && value.Length > 0)
				m_argumentValues.Add(value);
		}

		/// <summary>
		/// Get names of arguments following switch.
		/// </summary>
		public string[] GetArgumentNames()
		{
			return m_argumentNames.ToArray();
		}

		/// <summary>
		/// Get values of arguments following switch.
		/// </summary>
		public string[] GetArgumentValues()
		{
			return m_argumentValues.ToArray();
		}

		/// <summary>
		/// Get given switch name with long prefix.
		/// </summary>
		public static string GetLongPrefixedName(string name)
		{
			return GetPrefixedName(name, LongPrefix);
		}

		/// <summary>
		/// Get switch name given command-line argument, which may include prefix.
		/// </summary>
		public static string GetName(string argument)
		{
			foreach (string prefix in Prefixes)
			{
				if (argument.StartsWith(prefix, StringComparison.Ordinal))
					return argument.Substring(prefix.Length);
			}

			return argument;
		}

		/// <summary>
		/// Get given switch name with prefix.
		/// </summary>
		public static string GetPrefixedName(string name)
		{
			return GetPrefixedName(name, Prefix);
		}

		/// <summary>
		/// Determine if given command-line argument is valid switch.
		/// </summary>
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

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Get given switch name with prefix.
		/// </summary>
		static string GetPrefixedName(string name, string prefix)
		{
			if (HasPrefix(name))
				return name;

			return prefix + name;
		}

		/// <summary>
		/// Determine if given number of arguments enough for this switch.
		/// </summary>
		bool HasEnoughArguments(int numberArguments)
		{
			if (HasArguments)
			{
				if (NumberArguments == numberArguments)
				{
					return true;
				}
				else if (NumberArguments == UnknownNumberArguments &&
					numberArguments > 0)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Determine if given argument has prefix.
		/// </summary>
		static bool HasPrefix(string argument)
		{
			return argument != GetName(argument);
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Get switch description.
		/// </summary>
		public string Description
		{
			get { return m_description; }
		}

		/// <summary>
		/// Determine if arguments expected to follow this switch.
		/// </summary>
		public bool HasArguments
		{
			get { return m_hasArguments; }
		}

		/// <summary>
		/// Determine if switch has description.
		/// </summary>
		public bool HasDescription
		{
			get { return !string.IsNullOrEmpty(Description); }
		}

		/// <summary>
		/// Determine if enough argument names added to this switch.
		/// </summary>
		public bool HasEnoughNames
		{
			get { return HasEnoughArguments(m_argumentNames.Count); }
		}

		/// <summary>
		/// Determine if enough argument values added to this switch.
		/// </summary>
		public bool HasEnoughValues
		{
			get { return HasEnoughArguments(m_argumentValues.Count); }
		}

		/// <summary>
		/// Determine if switch has long name.
		/// </summary>
		public bool HasLongName
		{
			get { return !string.IsNullOrEmpty(LongName); }
		}

		/// <summary>
		/// Determine if optional switch.
		/// </summary>
		public bool IsOptional
		{
			get { return !IsRequired; }
		}

		/// <summary>
		/// Determine if required switch.
		/// </summary>
		public bool IsRequired
		{
			get { return m_isRequired; }
		}

		/// <summary>
		/// Get switch long name.
		/// </summary>
		public string LongName
		{
			get { return m_longName; }
		}

		/// <summary>
		/// Get switch name.
		/// </summary>
		public string Name
		{
			get { return m_name; }
		}

		/// <summary>
		/// Get number of arguments expected to follow this switch.
		/// </summary>
		public int NumberArguments
		{
			get { return m_numberArguments; }
		}
	}
}
