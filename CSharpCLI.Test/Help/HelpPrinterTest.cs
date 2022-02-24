////////////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2011 Bernard Badjari
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
using System.IO;
using System.Text;
using CSharpCLI.Argument;
using CSharpCLI.Help;
using NUnit.Framework;

namespace CSharpCLI.Tests.Help
{
	/// <summary>
	/// NUnit unit tests for HelpPrinter class.
	/// </summary>
	[TestFixture]
	public class HelpPrinterTest
	{
		/// <summary>
		/// Empty footer value.
		/// </summary>
		const string EmptyFooter = "";

		/// <summary>
		/// Empty header value.
		/// </summary>
		const string EmptyHeader = "";

		/// <summary>
		/// Executable test name.
		/// </summary>
		const string ExecutableName = "testExecutable";

		/// <summary>
		/// Switch has arguments.
		/// </summary>
		const bool HasArguments = true;

		/// <summary>
		/// Switch required.
		/// </summary>
		const bool IsRequired = true;

		/// <summary>
		/// No switch description.
		/// </summary>
		const string NoDescription = null;

		/// <summary>
		/// No footer value.
		/// </summary>
		const string NoFooter = null;

		/// <summary>
		/// No header value.
		/// </summary>
		const string NoHeader = null;

		/// <summary>
		/// No switch long name.
		/// </summary>
		const string NoLongName = null;

		////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// SwitchCollection with no switches to use for tests.
		/// </summary>
		SwitchCollection m_noSwitches;

		/// <summary>
		/// Output to use instead of standard output for each test.
		/// </summary>
		TextWriter m_output;

		/// <summary>
		/// Initialization.
		/// </summary>
		[TestFixtureSetUp]
		public void Initialize()
		{
			m_noSwitches = new SwitchCollection();
		}

		/// <summary>
		/// Initialization for each test.
		/// </summary>
		[SetUp]
		public void InitializeTest()
		{
			m_output = new StringWriter();

			Console.SetOut(m_output);
		}

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Test constructor with empty footer value.
		/// </summary>
		[Test]
		public void WithEmptyFooter()
		{
			Assert.DoesNotThrow(delegate { new HelpPrinter(ExecutableName, NoSwitches, GetHeader(), EmptyFooter); });
		}

		/// <summary>
		/// Test constructor with empty header value.
		/// </summary>
		[Test]
		public void WithEmptyHeader()
		{
			Assert.DoesNotThrow(delegate { new HelpPrinter(ExecutableName, NoSwitches, EmptyHeader, GetFooter()); });
		}

		/// <summary>
		/// Test constructor with empty executable name.
		/// </summary>
		[Test]
		public void WithEmptyName()
		{
			Assert.Throws<ArgumentNullException>(delegate { new HelpPrinter(string.Empty, NoSwitches); });
		}

		/// <summary>
		/// Test constructor with no footer value.
		/// </summary>
		[Test]
		public void WithNoFooter()
		{
			Assert.DoesNotThrow(delegate { new HelpPrinter(ExecutableName, NoSwitches, GetHeader(), NoFooter); });
		}

		/// <summary>
		/// Test constructor with no header value.
		/// </summary>
		[Test]
		public void WithNoHeader()
		{
			Assert.DoesNotThrow(delegate { new HelpPrinter(ExecutableName, NoSwitches, NoHeader); });
		}

		/// <summary>
		/// Test constructor with no value for switches.
		/// </summary>
		[Test]
		public void WithNoSwitches()
		{
			Assert.DoesNotThrow(delegate { new HelpPrinter(ExecutableName, NoSwitches); });
		}

		/// <summary>
		/// Test constructor with no values for executable name and switches.
		/// </summary>
		[Test]
		public void WithNoValues()
		{
			Assert.Throws<ArgumentNullException>(delegate { new HelpPrinter(null, null); });
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Test Print() method.
		/// </summary>
		[Test]
		public void Print()
		{
			const string ArgumentName = "arg";
			const string Description1 = "First switch.";
			const string Description2 = "Second switch. This switch is required.";
			const string Description3 = "Third switch. This switch is required and has an argument.";
			const string LongName1 = "switch1";
			const string LongName2 = "switch2";
			const string LongName3 = "switch3";
			const string Name1 = "s1";
			const string Name2 = "s2";
			const string Name3 = "s3";

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, LongName1, Description1);
			switches.Add(Name2, LongName2, Description2, IsRequired);
			switches.Add(Name3, LongName3, Description3, HasArguments, IsRequired, ArgumentName);

			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, switches, GetHeader(), GetFooter());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(GetHeader());
			expectedOutput.AppendLine();
			expectedOutput.AppendLine("Usage: " + ExecutableName + " [-s1] -s2 -s3 <arg>");
			expectedOutput.AppendLine();
			expectedOutput.AppendLine("-s1, --switch1         " + Description1);
			expectedOutput.AppendLine("-s2, --switch2         " + Description2);
			expectedOutput.AppendLine("-s3, --switch3 <arg>   Third switch. This switch is required and has an ");
			expectedOutput.AppendLine("                       argument.");
			expectedOutput.AppendLine();
			expectedOutput.AppendLine(GetFooter());

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only empty footer to print.
		/// </summary>
		[Test]
		public void PrintEmptyFooter()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, NoHeader, EmptyFooter);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(EmptyFooter);

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only empty header to print.
		/// </summary>
		[Test]
		public void PrintEmptyHeader()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, EmptyHeader);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(EmptyHeader);

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only footer to print.
		/// </summary>
		[Test]
		public void PrintFooter()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, NoHeader, GetFooter());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(GetFooter());

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only header to print.
		/// </summary>
		[Test]
		public void PrintHeader()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, GetHeader());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(GetHeader());

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only long footer to print.
		/// </summary>
		[Test]
		public void PrintLongFooter()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, NoHeader, GetLongFooter());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("------------------------------------------------------------------------------");
			expectedOutput.AppendLine("This is the long footer.");
			expectedOutput.AppendLine("It is \"long\" to demonstrate how text is wrapped when it extends beyond the ");
			expectedOutput.AppendLine("output width of the screen.");
			expectedOutput.AppendLine("It is printed at the bottom of the help screen.");
			expectedOutput.AppendLine("------------------------------------------------------------------------------");

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only long header to print.
		/// </summary>
		[Test]
		public void PrintLongHeader()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, GetLongHeader());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("------------------------------------------------------------------------------");
			expectedOutput.AppendLine("This is the long header.");
			expectedOutput.AppendLine("It is \"long\" to demonstrate how text is wrapped when it extends beyond the ");
			expectedOutput.AppendLine("output width of the screen.");
			expectedOutput.AppendLine("It is printed at the top of the help screen.");
			expectedOutput.AppendLine("------------------------------------------------------------------------------");

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with only long header and footer to print.
		/// </summary>
		[Test]
		public void PrintLongHeaderFooter()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, GetLongHeader(), GetLongFooter());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("------------------------------------------------------------------------------");
			expectedOutput.AppendLine("This is the long header.");
			expectedOutput.AppendLine("It is \"long\" to demonstrate how text is wrapped when it extends beyond the ");
			expectedOutput.AppendLine("output width of the screen.");
			expectedOutput.AppendLine("It is printed at the top of the help screen.");
			expectedOutput.AppendLine("------------------------------------------------------------------------------");
			expectedOutput.AppendLine();
			expectedOutput.AppendLine("------------------------------------------------------------------------------");
			expectedOutput.AppendLine("This is the long footer.");
			expectedOutput.AppendLine("It is \"long\" to demonstrate how text is wrapped when it extends beyond the ");
			expectedOutput.AppendLine("output width of the screen.");
			expectedOutput.AppendLine("It is printed at the bottom of the help screen.");
			expectedOutput.AppendLine("------------------------------------------------------------------------------");

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with switches having no descriptions.
		/// </summary>
		[Test]
		public void PrintNoDescriptions()
		{
			const string LongName1 = "switch1";
			const string LongName2 = "switch2";
			const string Name1 = "s1";
			const string Name2 = "s2";

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, LongName1, NoDescription);
			switches.Add(Name2, LongName2, NoDescription, IsRequired);

			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, switches);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("Usage: " + ExecutableName + " [-s1] -s2");
			expectedOutput.AppendLine();
			expectedOutput.AppendLine("-s1, --switch1");
			expectedOutput.AppendLine("-s2, --switch2");

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with no header and footer to print.
		/// </summary>
		[Test]
		public void PrintNoHeaderFooter()
		{
			const string Description1 = "First switch.";
			const string Description2 = "Second switch. This switch is required.";
			const string LongName1 = "switch1";
			const string LongName2 = "switch2";
			const string Name1 = "s1";
			const string Name2 = "s2";

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, LongName1, Description1);
			switches.Add(Name2, LongName2, Description2, IsRequired);

			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, switches);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("Usage: " + ExecutableName + " [-s1] -s2");
			expectedOutput.AppendLine();
			expectedOutput.AppendLine("-s1, --switch1   " + Description1);
			expectedOutput.AppendLine("-s2, --switch2   " + Description2);

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with switches having no long names.
		/// </summary>
		[Test]
		public void PrintNoLongNames()
		{
			const string Description1 = "First switch.";
			const string Description2 = "Second switch. This switch is required.";
			const string Name1 = "s1";
			const string Name2 = "s2";

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, NoLongName, Description1);
			switches.Add(Name2, NoLongName, Description2, IsRequired);

			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, switches);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("Usage: " + ExecutableName + " [-s1] -s2");
			expectedOutput.AppendLine();
			expectedOutput.AppendLine("-s1   " + Description1);
			expectedOutput.AppendLine("-s2   " + Description2);

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with no switches to print (only header and footer).
		/// </summary>
		[Test]
		public void PrintNoSwitches()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches, GetHeader(), GetFooter());

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(GetHeader());
			expectedOutput.AppendLine();
			expectedOutput.AppendLine(GetFooter());

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with non-printable switches (i.e. switches with no long names or descriptions).
		/// </summary>
		[Test]
		public void PrintNonPrintable()
		{
			const string Name1 = "s1";
			const string Name2 = "s2";

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2, IsRequired);

			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, switches);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine("Usage: " + ExecutableName + " [-s1] -s2");

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		/// <summary>
		/// Test Print() method with nothing to print.
		/// </summary>
		[Test]
		public void PrintNothing()
		{
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, NoSwitches);

			helpPrinter.Print();

			StringBuilder expectedOutput = new StringBuilder();

			expectedOutput.AppendLine(string.Empty);

			Assert.AreEqual(expectedOutput.ToString(), Output);
		}

		////////////////////////////////////////////////////////////////////////
		// Helper Methods

		/// <summary>
		/// Get footer test value.
		/// </summary>
		/// <returns>
		/// String representing footer test value.
		/// </returns>
		string GetFooter()
		{
			StringBuilder footer = new StringBuilder();

			footer.AppendLine("----------");
			footer.AppendLine("This is the footer.");
			footer.AppendLine("It is printed at the bottom of the help screen.");
			footer.Append("----------");

			return footer.ToString();
		}

		/// <summary>
		/// Get header test value.
		/// </summary>
		/// <returns>
		/// String representing header test value.
		/// </returns>
		string GetHeader()
		{
			StringBuilder header = new StringBuilder();

			header.AppendLine("----------");
			header.AppendLine("This is the header.");
			header.AppendLine("It is printed at the top of the help screen.");
			header.Append("----------");

			return header.ToString();
		}

		/// <summary>
		/// Get long footer test value.
		/// </summary>
		/// <returns>
		/// String representing long footer test value.
		/// </returns>
		string GetLongFooter()
		{
			StringBuilder footer = new StringBuilder();

			footer.AppendLine("------------------------------------------------------------------------------");
			footer.AppendLine("This is the long footer.");
			footer.AppendLine("It is \"long\" to demonstrate how text is wrapped when it extends beyond the output width of the screen.");
			footer.AppendLine("It is printed at the bottom of the help screen.");
			footer.Append("------------------------------------------------------------------------------");

			return footer.ToString();
		}

		/// <summary>
		/// Get long header test value.
		/// </summary>
		/// <returns>
		/// String representing long header test value.
		/// </returns>
		string GetLongHeader()
		{
			StringBuilder header = new StringBuilder();

			header.AppendLine("------------------------------------------------------------------------------");
			header.AppendLine("This is the long header.");
			header.AppendLine("It is \"long\" to demonstrate how text is wrapped when it extends beyond the output width of the screen.");
			header.AppendLine("It is printed at the top of the help screen.");
			header.Append("------------------------------------------------------------------------------");

			return header.ToString();
		}

		////////////////////////////////////////////////////////////////////////
		// Helper Properties

		/// <summary>
		/// Get SwitchCollection with no switches.
		/// </summary>
		/// <value>
		/// SwitchCollection representing empty collection of switches.
		/// </value>
		SwitchCollection NoSwitches
		{
			get { return m_noSwitches; }
		}

		/// <summary>
		/// Get current HelpPrinter output.
		/// </summary>
		/// <value>
		/// String representing output of current HelpPrinter instance.
		/// </value>
		string Output
		{
			get { return m_output.ToString(); }
		}
	}
}
