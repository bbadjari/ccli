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
using CSharpCLI.Argument;
using CSharpCLI.Parse;
using NUnit.Framework;

namespace CSharpCLI.Tests.Parse
{
	/// <summary>
	/// Unit tests for ArgumentParser class.
	/// </summary>
	[TestFixture]
	public class ArgumentParserTests
	{
		/// <summary>
		/// Empty switch name.
		/// </summary>
		private const string EmptyName = "";

		/// <summary>
		/// Empty switch names.
		/// </summary>
		private static readonly string[] EmptyNames = new string[NoElements];

		/// <summary>
		/// Switch has arguments.
		/// </summary>
		private const bool HasArguments = true;

		/// <summary>
		/// Switch required.
		/// </summary>
		private const bool IsRequired = true;

		/// <summary>
		/// No switch description.
		/// </summary>
		private const string NoDescription = null;

		/// <summary>
		/// No array elements.
		/// </summary>
		private const int NoElements = 0;

		/// <summary>
		/// No switch long name.
		/// </summary>
		private const string NoLongName = null;

		/// <summary>
		/// No switch name.
		/// </summary>
		private const string NoName = null;

		/// <summary>
		/// No switch names.
		/// </summary>
		private static readonly string[] NoNames = null;

		/// <summary>
		/// No switches.
		/// </summary>
		private const int NoSwitches = 0;

		////////////////////////////////////////////////////////////////////////
		// Helper Methods

		/// <summary>
		/// Called before each test executed.
		/// </summary>
		[SetUp]
		public void BeforeTest()
		{
			EmptyParser = new ArgumentParser(new string[NoElements], new SwitchCollection());

			EmptyParser.Parse();
		}

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Test constructor with no values for arguments and switches.
		/// </summary>
		[Test]
		public void WithNoValues()
		{
			Assert.Throws<ArgumentNullException>(delegate { new ArgumentParser(null, null); });
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Test AllParsed() method.
		/// </summary>
		[Test]
		public void AllParsed()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";
			const string Name3 = "arg3";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			string[] switchNames1 = new string[] { Name1 };
			string[] switchNames2 = new string[] { Name1, Name2 };
			string[] switchNames3 = new string[] { Name1, Name2, Name3 };

			SwitchCollection switches = new SwitchCollection();

			switches.Add(switchNames2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.AllParsed(switchNames1));
			Assert.IsFalse(parser.AllParsed(switchNames2));
			Assert.IsFalse(parser.AllParsed(switchNames3));
			Assert.IsFalse(EmptyParser.AllParsed(switchNames1));
			Assert.IsFalse(EmptyParser.AllParsed(switchNames2));
			Assert.IsFalse(EmptyParser.AllParsed(switchNames3));

			parser.Parse();

			Assert.IsFalse(parser.AllParsed(switchNames3));
			
			Assert.IsTrue(parser.AllParsed(switchNames1));
			Assert.IsTrue(parser.AllParsed(switchNames2));
		}

		/// <summary>
		/// Test AllParsed() method defining invalid names.
		/// </summary>
		[Test]
		public void AllParsedWithInvalidNames()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.AllParsed(EmptyNames));
			Assert.IsFalse(parser.AllParsed(NoNames));
			Assert.IsFalse(EmptyParser.AllParsed(EmptyNames));
			Assert.IsFalse(EmptyParser.AllParsed(NoNames));

			parser.Parse();

			Assert.IsFalse(parser.AllParsed(EmptyNames));
			Assert.IsFalse(parser.AllParsed(NoNames));
		}

		/// <summary>
		/// Test AnyParsed() method.
		/// </summary>
		[Test]
		public void AnyParsed()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";
			const string Name3 = "arg3";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			string[] switchNames1 = new string[] { Name1 };
			string[] switchNames2 = new string[] { Name1, Name2 };
			string[] switchNames3 = new string[] { Name1, Name2, Name3 };

			SwitchCollection switches = new SwitchCollection();

			switches.Add(switchNames2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.AnyParsed(switchNames1));
			Assert.IsFalse(parser.AnyParsed(switchNames2));
			Assert.IsFalse(parser.AnyParsed(switchNames3));
			Assert.IsFalse(EmptyParser.AnyParsed(switchNames1));
			Assert.IsFalse(EmptyParser.AnyParsed(switchNames2));
			Assert.IsFalse(EmptyParser.AnyParsed(switchNames3));

			parser.Parse();

			Assert.IsTrue(parser.AnyParsed(switchNames1));
			Assert.IsTrue(parser.AnyParsed(switchNames2));
			Assert.IsTrue(parser.AnyParsed(switchNames3));
		}

		/// <summary>
		/// Test AnyParsed() method defining invalid names.
		/// </summary>
		[Test]
		public void AnyParsedWithInvalidNames()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.AnyParsed(EmptyNames));
			Assert.IsFalse(parser.AnyParsed(NoNames));
			Assert.IsFalse(EmptyParser.AnyParsed(EmptyNames));
			Assert.IsFalse(EmptyParser.AnyParsed(NoNames));

			parser.Parse();

			Assert.IsFalse(parser.AnyParsed(EmptyNames));
			Assert.IsFalse(parser.AnyParsed(NoNames));
		}

		/// <summary>
		/// Test GetValue() method.
		/// </summary>
		[Test]
		public void GetValue()
		{
			const string ArgumentName1 = "arg1";
			const string ArgumentName2 = "arg2";
			const string Name1 = "name1";
			const string Name2 = "name2";
			const string Value1 = "value1";
			const string Value2 = "value2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Value1,
				Switch.GetPrefixedName(Name2),
				Value2
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, NoLongName, NoDescription, IsRequired, ArgumentName1);
			switches.Add(Name2, NoLongName, NoDescription, IsRequired, ArgumentName2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsNull(parser.GetValue(EmptyName));
			Assert.IsNull(parser.GetValue(Name1));
			Assert.IsNull(parser.GetValue(Name2));
			Assert.IsNull(parser.GetValue(NoName));
			Assert.IsNull(EmptyParser.GetValue(EmptyName));
			Assert.IsNull(EmptyParser.GetValue(NoName));

			parser.Parse();

			Assert.AreEqual(Value1, parser.GetValue(Name1));
			Assert.AreEqual(Value2, parser.GetValue(Name2));

			Assert.IsNull(parser.GetValue(EmptyName));
			Assert.IsNull(parser.GetValue(NoName));
		}

		/// <summary>
		/// Test GetValue() method specifying argument number.
		/// </summary>
		[Test]
		public void GetValueWithArgumentNumber()
		{
			const int Argument1 = 1;
			const string ArgumentName1 = "arg1";
			const string ArgumentName2 = "arg2";
			const string Name1 = "name1";
			const string Name2 = "name2";
			const string Value1 = "value1";
			const string Value2 = "value2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Value1,
				Switch.GetPrefixedName(Name2),
				Value2
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, NoLongName, NoDescription, IsRequired, ArgumentName1);
			switches.Add(Name2, NoLongName, NoDescription, IsRequired, ArgumentName2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsNull(parser.GetValue(EmptyName, Argument1));
			Assert.IsNull(parser.GetValue(Name1, Argument1));
			Assert.IsNull(parser.GetValue(Name2, Argument1));
			Assert.IsNull(parser.GetValue(NoName, Argument1));
			Assert.IsNull(EmptyParser.GetValue(EmptyName, Argument1));
			Assert.IsNull(EmptyParser.GetValue(NoName, Argument1));

			parser.Parse();

			Assert.AreEqual(Value1, parser.GetValue(Name1, Argument1));
			Assert.AreEqual(Value2, parser.GetValue(Name2, Argument1));

			Assert.IsNull(parser.GetValue(EmptyName, Argument1));
			Assert.IsNull(parser.GetValue(NoName, Argument1));
		}

		/// <summary>
		/// Test GetValue() method specifying invalid argument number.
		/// </summary>
		[Test]
		public void GetValueWithInvalidArgumentNumber()
		{
			const int Argument0 = 0;
			const int Argument1 = 1;
			const int Argument2 = 2;
			const string ArgumentName = "arg1";
			const string Name = "name1";
			const string Value = "value1";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name),
				Value
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name, NoLongName, NoDescription, IsRequired, ArgumentName);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsNull(parser.GetValue(EmptyName, Argument0));
			Assert.IsNull(parser.GetValue(EmptyName, Argument1));
			Assert.IsNull(parser.GetValue(EmptyName, Argument2));
			Assert.IsNull(parser.GetValue(Name, Argument0));
			Assert.IsNull(parser.GetValue(Name, Argument1));
			Assert.IsNull(parser.GetValue(Name, Argument2));
			Assert.IsNull(parser.GetValue(NoName, Argument0));
			Assert.IsNull(parser.GetValue(NoName, Argument1));
			Assert.IsNull(parser.GetValue(NoName, Argument2));
			Assert.IsNull(EmptyParser.GetValue(EmptyName, Argument0));
			Assert.IsNull(EmptyParser.GetValue(EmptyName, Argument1));
			Assert.IsNull(EmptyParser.GetValue(EmptyName, Argument2));
			Assert.IsNull(EmptyParser.GetValue(NoName, Argument0));
			Assert.IsNull(EmptyParser.GetValue(NoName, Argument1));
			Assert.IsNull(EmptyParser.GetValue(NoName, Argument2));

			parser.Parse();

			Assert.AreEqual(Value, parser.GetValue(Name, Argument1));

			Assert.IsNull(parser.GetValue(EmptyName, Argument0));
			Assert.IsNull(parser.GetValue(EmptyName, Argument1));
			Assert.IsNull(parser.GetValue(EmptyName, Argument2));
			Assert.IsNull(parser.GetValue(Name, Argument0));
			Assert.IsNull(parser.GetValue(Name, Argument2));
			Assert.IsNull(parser.GetValue(NoName, Argument0));
			Assert.IsNull(parser.GetValue(NoName, Argument1));
			Assert.IsNull(parser.GetValue(NoName, Argument2));
		}

		/// <summary>
		/// Test GetValues() method.
		/// </summary>
		[Test]
		public void GetValues()
		{
			const string Name1 = "name1";
			const string Name2 = "name2";
			const int NumberArguments = 2;
			const string Value1 = "value1";
			const string Value2 = "value2";
			const string Value3 = "value3";
			const string Value4 = "value4";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Value1, Value2,
				Switch.GetPrefixedName(Name2),
				Value3, Value4
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, NoLongName, NoDescription, NumberArguments, IsRequired);
			switches.Add(Name2, NoLongName, NoDescription, NumberArguments, IsRequired);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsNull(parser.GetValues(EmptyName));
			Assert.IsNull(parser.GetValues(Name1));
			Assert.IsNull(parser.GetValues(Name2));
			Assert.IsNull(parser.GetValues(NoName));
			Assert.IsNull(EmptyParser.GetValues(EmptyName));
			Assert.IsNull(EmptyParser.GetValues(Name1));
			Assert.IsNull(EmptyParser.GetValues(Name2));
			Assert.IsNull(EmptyParser.GetValues(NoName));

			parser.Parse();

			Assert.AreEqual(new string[] { Value1, Value2 }, parser.GetValues(Name1));
			Assert.AreEqual(new string[] { Value3, Value4 }, parser.GetValues(Name2));

			Assert.IsNull(parser.GetValues(EmptyName));
			Assert.IsNull(parser.GetValues(NoName));
		}

		/// <summary>
		/// Test IsParsed() method.
		/// </summary>
		[Test]
		public void IsParsed()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(EmptyName));
			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));
			Assert.IsFalse(parser.IsParsed(NoName));
			Assert.IsFalse(EmptyParser.IsParsed(EmptyName));
			Assert.IsFalse(EmptyParser.IsParsed(Name1));
			Assert.IsFalse(EmptyParser.IsParsed(Name2));
			Assert.IsFalse(EmptyParser.IsParsed(NoName));

			parser.Parse();

			Assert.IsFalse(parser.IsParsed(EmptyName));
			Assert.IsFalse(parser.IsParsed(NoName));

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test NoneParsed() method.
		/// </summary>
		[Test]
		public void NoneParsed()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";
			const string Name3 = "arg3";
			const string Name4 = "arg4";
			const string Name5 = "arg5";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			string[] switchNames1 = new string[] { Name1 };
			string[] switchNames2 = new string[] { Name1, Name2 };
			string[] switchNames3 = new string[] { Name1, Name2, Name3 };
			string[] switchNames4 = new string[] { Name2, Name3, Name4 };
			string[] switchNames5 = new string[] { Name4, Name5 };

			SwitchCollection switches = new SwitchCollection();

			switches.Add(switchNames2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsTrue(parser.NoneParsed(switchNames1));
			Assert.IsTrue(parser.NoneParsed(switchNames2));
			Assert.IsTrue(parser.NoneParsed(switchNames3));
			Assert.IsTrue(parser.NoneParsed(switchNames4));
			Assert.IsTrue(parser.NoneParsed(switchNames5));
			Assert.IsTrue(EmptyParser.NoneParsed(switchNames1));
			Assert.IsTrue(EmptyParser.NoneParsed(switchNames2));
			Assert.IsTrue(EmptyParser.NoneParsed(switchNames3));

			parser.Parse();

			Assert.IsFalse(parser.NoneParsed(switchNames1));
			Assert.IsFalse(parser.NoneParsed(switchNames2));
			Assert.IsFalse(parser.NoneParsed(switchNames3));
			Assert.IsFalse(parser.NoneParsed(switchNames4));
			
			Assert.IsTrue(parser.NoneParsed(switchNames5));
		}

		/// <summary>
		/// Test NoneParsed() method defining invalid names.
		/// </summary>
		[Test]
		public void NoneParsedWithInvalidNames()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.NoneParsed(EmptyNames));
			Assert.IsFalse(parser.NoneParsed(NoNames));
			Assert.IsFalse(EmptyParser.NoneParsed(EmptyNames));
			Assert.IsFalse(EmptyParser.NoneParsed(NoNames));

			parser.Parse();

			Assert.IsFalse(parser.NoneParsed(EmptyNames));
			Assert.IsFalse(parser.NoneParsed(NoNames));
		}

		/// <summary>
		/// Test Parse() method.
		/// </summary>
		[Test]
		public void Parse()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(EmptyName));
			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));
			Assert.IsFalse(parser.IsParsed(NoName));
			Assert.IsFalse(EmptyParser.IsParsed(EmptyName));
			Assert.IsFalse(EmptyParser.IsParsed(Name1));
			Assert.IsFalse(EmptyParser.IsParsed(Name2));
			Assert.IsFalse(EmptyParser.IsParsed(NoName));

			parser.Parse();

			Assert.IsFalse(parser.IsParsed(EmptyName));
			Assert.IsFalse(parser.IsParsed(NoName));

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on duplicate switch names.
		/// </summary>
		[Test]
		public void ParseDuplicateSwitch()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2),
				Switch.GetPrefixedName(Name1)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));

			Assert.Throws<ParsingException>(delegate { parser.Parse(); });

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on switches with missing required switch.
		/// </summary>
		[Test]
		public void ParseMissingSwitch()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";
			const string Name3 = "arg3";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);
			switches.Add(Name3, NoLongName, NoDescription, IsRequired);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));
			Assert.IsFalse(parser.IsParsed(Name3));

			Assert.Throws<ParsingException>(delegate { parser.Parse(); });

			Assert.IsFalse(parser.IsParsed(Name3));

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on command-line arguments with no switches.
		/// </summary>
		[Test]
		public void ParseNoSwitches()
		{
			const string ArgumentName1 = "arg1";
			const string ArgumentName2 = "arg2";
			const string Name1 = "name1";
			const string Name2 = "name2";

			string[] arguments = new string[]
			{
				ArgumentName1,
				ArgumentName2
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(ArgumentName1));
			Assert.IsFalse(parser.IsParsed(ArgumentName2));
			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));

			parser.Parse();

			Assert.IsFalse(parser.IsParsed(ArgumentName1));
			Assert.IsFalse(parser.IsParsed(ArgumentName2));
			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on switch with arguments.
		/// </summary>
		[Test]
		public void ParseSwitchWithArguments()
		{
			const string Name1 = "name1";
			const string Name2 = "name2";
			const string Value1 = "value1";
			const string Value2 = "value2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Value1,
				Value2,
				Switch.GetPrefixedName(Name2)
			};

			string[] argumentNames = new string[] { "arg1", "arg2" };

			string[] values = new string[] { Value1, Value2 };

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, NoLongName, NoDescription, IsRequired, argumentNames);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));

			parser.Parse();

			Assert.AreEqual(values, parser.GetValues(Name1));

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on switch with excess arguments.
		/// </summary>
		[Test]
		public void ParseSwitchWithExcessArguments()
		{
			const string Name1 = "name1";
			const string Name2 = "name2";
			const string Value1 = "value1";
			const string Value2 = "value2";
			const string Value3 = "value3";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Value1,
				Value2,
				Switch.GetPrefixedName(Name2)
			};

			string[] argumentNames = new string[] { "arg1", "arg2" };

			string[] enoughValues = new string[] { Value1, Value2 };

			string[] excessValues = new string[] { Value1, Value2, Value3 };

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1, NoLongName, NoDescription, IsRequired, argumentNames);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));

			// No exceptions should be thrown. Excess arguments should be ignored.
			Assert.DoesNotThrow(delegate { parser.Parse(); });

			Assert.AreEqual(enoughValues, parser.GetValues(Name1));

			Assert.AreNotEqual(excessValues, parser.GetValues(Name1));

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on switch with missing arguments.
		/// </summary>
		[Test]
		public void ParseSwitchWithMissingArguments()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2, NoLongName, NoDescription, HasArguments, !IsRequired);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));

			Assert.Throws<ParsingException>(delegate { parser.Parse(); });

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method called twice in succession.
		/// </summary>
		[Test]
		public void ParseTwice()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));

			parser.Parse();

			// No exceptions should be thrown on second successive call.
			Assert.DoesNotThrow(delegate { parser.Parse(); });

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		/// <summary>
		/// Test Parse() method on undefined switch.
		/// </summary>
		[Test]
		public void ParseUndefinedSwitch()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";
			const string UndefinedName = "arg3";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(UndefinedName),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.IsFalse(parser.IsParsed(Name1));
			Assert.IsFalse(parser.IsParsed(Name2));
			Assert.IsFalse(parser.IsParsed(UndefinedName));

			Assert.Throws<ParsingException>(delegate { parser.Parse(); });

			Assert.IsFalse(parser.IsParsed(Name2));
			Assert.IsFalse(parser.IsParsed(UndefinedName));

			Assert.IsTrue(parser.IsParsed(Name1));
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Test NumberSwitchesParsed property.
		/// </summary>
		[Test]
		public void NumberSwitchesParsed()
		{
			const string Name1 = "arg1";
			const string Name2 = "arg2";

			string[] arguments = new string[]
			{
				Switch.GetPrefixedName(Name1),
				Switch.GetPrefixedName(Name2)
			};

			SwitchCollection switches = new SwitchCollection();

			switches.Add(Name1);
			switches.Add(Name2);

			ArgumentParser parser = new ArgumentParser(arguments, switches);

			Assert.AreEqual(NoSwitches, parser.NumberSwitchesParsed);
			Assert.AreEqual(NoSwitches, EmptyParser.NumberSwitchesParsed);

			parser.Parse();

			Assert.AreEqual(switches.Count, parser.NumberSwitchesParsed);

			Assert.IsTrue(parser.IsParsed(Name1));
			Assert.IsTrue(parser.IsParsed(Name2));
		}

		////////////////////////////////////////////////////////////////////////
		// Helper Properties

		/// <summary>
		/// Get/set argument parser with no arguments or switches.
		/// </summary>
		/// <value>
		/// ArgumentParser representing empty argument parser.
		/// </value>
		private ArgumentParser EmptyParser { get; set; }
	}
}
