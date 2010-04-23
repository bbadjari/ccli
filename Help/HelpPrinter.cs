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
using System.Text;
using System.Text.RegularExpressions;
using CSharpCLI.Argument;

namespace CSharpCLI.Help
{
	/// <summary>
	/// Prints switch usage information and descriptions for a collection
	/// of Switch objects.
	/// </summary>
	public class HelpPrinter
	{
		/// <summary>
		/// Encloses argument names in output.
		/// </summary>
		static class ArgumentNameTag
		{
			public const string Start = "<";
			public const string End = ">";
		}

		/// <summary>
		/// Characters used in output.
		/// </summary>
		static class Character
		{
			/// <summary>
			/// Length of one character.
			/// </summary>
			public const int Length = 1;

			/// <summary>
			/// Space character.
			/// </summary>
			public const char Space = ' ';

			/// <summary>
			/// Delimiter character.
			/// </summary>
			public const char Delimiter = ',';
		}

		/// <summary>
		/// Encloses optional switches in output.
		/// </summary>
		static class OptionalSwitchTag
		{
			public const string Start = "[";
			public const string End = "]";
		}

		/// <summary>
		/// Number of space characters to use when printing descriptions.
		/// </summary>
		const int DescriptionSpacing = 3;

		/// <summary>
		/// No characters to indent each new line by.
		/// </summary>
		const int NoIndentCharacters = 0;

		/// <summary>
		/// Number of characters per line of output.
		/// </summary>
		const int OutputWidth = 80;

		/// <summary>
		/// Start of usage output.
		/// </summary>
		const string UsagePrefix = "Usage: ";

		////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Executable name to be printed.
		/// </summary>
		string m_executableName;

		/// <summary>
		/// Footer to be printed.
		/// </summary>
		string m_footer;

		/// <summary>
		/// Header to be printed.
		/// </summary>
		string m_header;

		/// <summary>
		/// Number of characters to indent each new line by.
		/// </summary>
		int m_numberIndentCharacters;

		/// <summary>
		/// Help output to be printed.
		/// </summary>
		StringBuilder m_output;

		/// <summary>
		/// Switches to be printed.
		/// </summary>
		List<Switch> m_switches;

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Constructor for specifying executable name and switches to be
		/// printed.
		/// </summary>
		public HelpPrinter(string executableName, SwitchCollection switches)
		{
			m_executableName = executableName;
			m_switches = new List<Switch>();

			if (switches != null)
				m_switches.AddRange(switches.SortedSwitches);
		}

		/// <summary>
		/// Constructor for specifying header to be printed.
		/// </summary>
		public HelpPrinter(string executableName, SwitchCollection switches,
			string header)
			: this(executableName, switches)
		{
			m_header = header;
		}

		/// <summary>
		/// Constructor for specifying footer to be printed.
		/// </summary>
		public HelpPrinter(string executableName, SwitchCollection switches,
			string header, string footer)
			: this(executableName, switches, header)
		{
			m_footer = footer;
		}

		////////////////////////////////////////////////////////////////////////
		// Public Methods

		/// <summary>
		/// Print switch usage information and descriptions.
		/// </summary>
		public void Print()
		{
			Initialize();

			PrintHeader();

			if (HasSwitches)
			{
				PrintUsage();

				AppendLines();

				PrintDescriptions();

				if (HasFooter)
					AppendLines();
			}

			PrintFooter();

			// Print help output to standard output.
			Console.WriteLine(Output.ToString());
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Append given string value to output.
		/// </summary>
		void Append(string value)
		{
			string[] lines = Regex.Split(value, Environment.NewLine);

			for (int index = 0; index < lines.Length; index++)
			{
				AppendLine(lines[index]);

				if (index != lines.Length - 1)
					AppendLine();
			}
		}

		/// <summary>
		/// Append given argument names to given output.
		/// </summary>
		static void AppendArgumentNames(string[] argumentNames, StringBuilder output)
		{
			for (int index = 0; index < argumentNames.Length; index++)
			{
				output.Append(ArgumentNameTag.Start);
				output.Append(argumentNames[index]);
				output.Append(ArgumentNameTag.End);

				if (index != argumentNames.Length - 1)
					output.Append(Character.Space);
			}
		}

		/// <summary>
		/// Append line and indentation to output.
		/// </summary>
		void AppendLine()
		{
			Output.AppendLine();
			Output.Append(Indentation);
		}

		/// <summary>
		/// Append given string value representing single line to output.
		/// </summary>
		void AppendLine(string value)
		{
			// Ensure given string value represents single line.
			if (HasNewLine(value))
			{
				Append(value);

				return;
			}

			// Maximum number of characters to append to line of output.
			int maximumAppendLength = OutputWidth - Environment.NewLine.Length;

			while (value.Length > 0)
			{
				int lineLengthLeft = maximumAppendLength - CurrentLineLength;

				int appendLength = GetAppendLength(value, lineLengthLeft,
					maximumAppendLength);

				string outputLine = value.Substring(0, appendLength);

				Output.Append(outputLine);

				if (value.Length > lineLengthLeft)
					AppendLine();

				// Remove appended output, ensuring no leading whitespace.
				value = value.Remove(0, appendLength).TrimStart();
			}
		}

		/// <summary>
		/// Append lines to output to separate sections.
		/// </summary>
		void AppendLines()
		{
			Output.AppendLine();
			Output.AppendLine();
		}

		/// <summary>
		/// Append given switch to given output.
		/// </summary>
		static void AppendSwitch(Switch switchObject, StringBuilder output)
		{
			if (switchObject.IsOptional)
				output.Append(OptionalSwitchTag.Start);

			output.Append(Switch.GetPrefixedName(switchObject.Name));

			if (switchObject.HasArguments)
			{
				output.Append(Character.Space);

				AppendArgumentNames(switchObject.GetArgumentNames(), output);
			}

			if (switchObject.IsOptional)
				output.Append(OptionalSwitchTag.End);
		}

		/// <summary>
		/// Determine if next token in given string starting at given index can
		/// be appended on new line of output, given maximum amount.
		/// </summary>
		bool CanAppendNextToken(string value, int startIndex, int maximumAppendLength)
		{
			int firstSpaceIndex = value.IndexOf(Character.Space, startIndex);

			int nextTokenLength = value.Length - startIndex;

			if (firstSpaceIndex > 0)
				nextTokenLength = firstSpaceIndex - startIndex;

			return nextTokenLength + NumberIndentCharacters <= maximumAppendLength;
		}

		/// <summary>
		/// Get number of characters in given string to append, depending on
		/// whether it contains a new line character.
		/// </summary>
		static int GetAppendLength(string value)
		{
			int valueLength = value.Length;

			int firstNewLineIndex = value.IndexOf(Environment.NewLine,
				StringComparison.Ordinal);

			if (firstNewLineIndex > 0)
				valueLength = firstNewLineIndex;

			return valueLength;
		}

		/// <summary>
		/// Get number of characters in given string to append to current line
		/// of output, given length left on current line and maximum length.
		/// </summary>
		int GetAppendLength(string value, int lineLengthLeft, int maximumAppendLength)
		{
			int appendLength = Math.Min(GetAppendLength(value), lineLengthLeft);

			if (appendLength == lineLengthLeft)
			{
				string appendValue = value.Substring(0, appendLength);

				int lastSpaceIndex = appendValue.LastIndexOf(Character.Space);

				if (lastSpaceIndex > 0)
				{
					appendLength = lastSpaceIndex + Character.Length;

					if (!CanAppendNextToken(value, appendLength, maximumAppendLength))
						appendLength = lineLengthLeft;
				}
			}

			return appendLength;
		}

		/// <summary>
		/// Get padding of given length.
		/// </summary>
		static string GetPadding(int length)
		{
			return new String(Character.Space, length);
		}

		/// <summary>
		/// Determine if given string value contains new line characters.
		/// </summary>
		static bool HasNewLine(string value)
		{
			return value.Contains(Environment.NewLine);
		}

		/// <summary>
		/// Initialize printer.
		/// </summary>
		void Initialize()
		{
			m_output = new StringBuilder();

			NumberIndentCharacters = NoIndentCharacters;
		}

		/// <summary>
		/// Print switch descriptions.
		/// </summary>
		void PrintDescriptions()
		{
			List<string> namesArguments = new List<string>();

			int longestLength = 0;

			foreach (Switch switchObject in Switches)
			{
				StringBuilder nameArgument = new StringBuilder();

				nameArgument.Append(Switch.GetPrefixedName(switchObject.Name));

				if (switchObject.HasLongName)
				{
					nameArgument.Append(Character.Delimiter);
					nameArgument.Append(Character.Space);
					nameArgument.Append(Switch.GetLongPrefixedName(switchObject.LongName));
				}

				if (switchObject.HasArguments)
				{
					nameArgument.Append(Character.Space);

					AppendArgumentNames(switchObject.GetArgumentNames(), nameArgument);
				}

				longestLength = Math.Max(longestLength, nameArgument.Length);

				namesArguments.Add(nameArgument.ToString());
			}

			NumberIndentCharacters = longestLength + DescriptionSpacing;

			for (int index = 0; index < Switches.Count; index++)
			{
				StringBuilder output = new StringBuilder();

				Switch switchObject = Switches[index];

				string nameArgument = namesArguments[index];

				output.Append(nameArgument);

				if (switchObject.HasDescription)
				{
					int paddingLength = NumberIndentCharacters - nameArgument.Length;

					output.Append(GetPadding(paddingLength));
					output.Append(switchObject.Description);
				}

				AppendLine(output.ToString());

				if (index != Switches.Count - 1)
					Output.AppendLine();
			}
		}

		/// <summary>
		/// Print footer.
		/// </summary>
		void PrintFooter()
		{
			NumberIndentCharacters = NoIndentCharacters;

			if (HasFooter)
				Append(Footer);
		}

		/// <summary>
		/// Print header.
		/// </summary>
		void PrintHeader()
		{
			NumberIndentCharacters = NoIndentCharacters;

			if (HasHeader)
			{
				Append(Header);

				AppendLines();
			}
		}

		/// <summary>
		/// Print switch usage information.
		/// </summary>
		void PrintUsage()
		{
			NumberIndentCharacters = UsagePrefix.Length;

			StringBuilder output = new StringBuilder();

			output.Append(UsagePrefix);
			output.Append(ExecutableName);
			output.Append(Character.Space);

			for (int index = 0; index < Switches.Count; index++)
			{
				AppendSwitch(Switches[index], output);

				if (index != Switches.Count - 1)
					output.Append(Character.Space);
			}

			AppendLine(output.ToString());
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Get length of current line of output.
		/// </summary>
		int CurrentLineLength
		{
			get
			{
				string output = Output.ToString();

				int currentLineLength = output.Length;

				int lastNewLineIndex = output.LastIndexOf(Environment.NewLine,
					StringComparison.Ordinal);

				if (lastNewLineIndex >= 0)
					currentLineLength -= (lastNewLineIndex + Environment.NewLine.Length);

				return currentLineLength;
			}
		}

		/// <summary>
		/// Get name of executable to be printed.
		/// </summary>
		string ExecutableName
		{
			get { return m_executableName; }
		}

		/// <summary>
		/// Get footer to be printed.
		/// </summary>
		string Footer
		{
			get { return m_footer; }
		}

		/// <summary>
		/// Determine if footer to be printed given.
		/// </summary>
		bool HasFooter
		{
			get { return !string.IsNullOrEmpty(Footer); }
		}

		/// <summary>
		/// Determine if header to be printed given.
		/// </summary>
		bool HasHeader
		{
			get { return !string.IsNullOrEmpty(Header); }
		}

		/// <summary>
		/// Determine if switches to be printed given.
		/// </summary>
		bool HasSwitches
		{
			get { return Switches.Count > 0; }
		}

		/// <summary>
		/// Get header to be printed.
		/// </summary>
		string Header
		{
			get { return m_header; }
		}

		/// <summary>
		/// Get indentation string to append to each new line of output.
		/// </summary>
		string Indentation
		{
			get { return GetPadding(NumberIndentCharacters); }
		}

		/// <summary>
		/// Get/set number of characters to indent each new line by.
		/// </summary>
		int NumberIndentCharacters
		{
			get { return m_numberIndentCharacters; }
			set { m_numberIndentCharacters = value; }
		}

		/// <summary>
		/// Get help output to be printed.
		/// </summary>
		StringBuilder Output
		{
			get { return m_output; }
		}

		/// <summary>
		/// Get switches to be printed.
		/// </summary>
		IList<Switch> Switches
		{
			get { return m_switches; }
		}
	}
}
