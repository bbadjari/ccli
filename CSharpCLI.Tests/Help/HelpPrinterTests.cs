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
	/// Unit tests for HelpPrinter class.
	/// </summary>
	[TestFixture]
	public class HelpPrinterTests
	{
		private const string EmptyExecutableName = "";

		private const string EmptyFooter = "";

		private const string EmptyHeader = "";

		private const string ExecutableName = "testExecutable";

		private const bool HasArguments = true;

		private const bool IsRequired = true;

		private const string NoDescription = null;

		private const string NoExecutableName = null;

		private const string NoFooter = null;

		private const string NoHeader = null;

		private const string NoLongName = null;

		private const SwitchCollection NoSwitches = null;

		////////////////////////////////////////////////////////////////////////

		private TextWriter output;

		////////////////////////////////////////////////////////////////////////
		// Helper Methods

		/// <summary>
		/// Called before each test executed.
		/// </summary>
		[SetUp]
		public void BeforeTest()
		{
			EmptySwitches = new SwitchCollection();

			output = new StringWriter();

			Console.SetOut(output);
		}

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Test constructor with empty executable name.
		/// </summary>
		[Test]
		public void WithEmptyExecutableName()
		{
			Assert.Throws<ArgumentException>(() => new HelpPrinter(EmptyExecutableName, EmptySwitches));
		}

		/// <summary>
		/// Test constructor with empty footer value.
		/// </summary>
		[Test]
		public void WithEmptyFooter()
		{
			Assert.DoesNotThrow(() => new HelpPrinter(ExecutableName, EmptySwitches, GetHeader(), EmptyFooter));
		}

		/// <summary>
		/// Test constructor with empty header value.
		/// </summary>
		[Test]
		public void WithEmptyHeader()
		{
			Assert.DoesNotThrow(() => new HelpPrinter(ExecutableName, EmptySwitches, EmptyHeader, GetFooter()));
		}

		/// <summary>
		/// Test constructor with empty switches.
		/// </summary>
		[Test]
		public void WithEmptySwitches()
		{
			Assert.DoesNotThrow(() => new HelpPrinter(ExecutableName, EmptySwitches));
		}

		/// <summary>
		/// Test constructor with no executable name.
		/// </summary>
		[Test]
		public void WithNoExecutableName()
		{
			Assert.Throws<ArgumentException>(() => new HelpPrinter(NoExecutableName, EmptySwitches));
		}

		/// <summary>
		/// Test constructor with no footer value.
		/// </summary>
		[Test]
		public void WithNoFooter()
		{
			Assert.DoesNotThrow(() => new HelpPrinter(ExecutableName, EmptySwitches, GetHeader(), NoFooter));
		}

		/// <summary>
		/// Test constructor with no header value.
		/// </summary>
		[Test]
		public void WithNoHeader()
		{
			Assert.DoesNotThrow(() => new HelpPrinter(ExecutableName, EmptySwitches, NoHeader));
		}

		/// <summary>
		/// Test constructor with no switches.
		/// </summary>
		[Test]
		public void WithNoSwitches()
		{
			Assert.Throws<ArgumentNullException>(() => new HelpPrinter(ExecutableName, NoSwitches));
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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, NoHeader, EmptyFooter);

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, EmptyHeader);

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, NoHeader, GetFooter());

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, GetHeader());

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, NoHeader, GetLongFooter());

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, GetLongHeader());

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, GetLongHeader(), GetLongFooter());

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches, GetHeader(), GetFooter());

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
			HelpPrinter helpPrinter = new HelpPrinter(ExecutableName, EmptySwitches);

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
		private string GetFooter()
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
		private string GetHeader()
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
		private string GetLongFooter()
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
		private string GetLongHeader()
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
		/// Get/set empty collection of switches.
		/// </summary>
		/// <value>
		/// SwitchCollection representing empty collection of switches.
		/// </value>
		private SwitchCollection EmptySwitches { get; set; }

		/// <summary>
		/// Get current HelpPrinter output.
		/// </summary>
		/// <value>
		/// String representing output of current HelpPrinter instance.
		/// </value>
		private string Output
		{
			get { return output.ToString(); }
		}
	}
}
