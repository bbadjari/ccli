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
using CSharpCLI.Argument;
using NUnit.Framework;

namespace CSharpCLI.Tests.Argument
{
	/// <summary>
	/// Unit tests for SwitchCollection class.
	/// </summary>
	[TestFixture]
	public class SwitchCollectionTests
	{
		/// <summary>
		/// Switch test description.
		/// </summary>
		private const string Description = "Test switch.";

		/// <summary>
		/// Empty switch name.
		/// </summary>
		private const string EmptyName = "";

		/// <summary>
		/// Switch has arguments.
		/// </summary>
		private const bool HasArguments = true;

		/// <summary>
		/// Switch required.
		/// </summary>
		private const bool IsRequired = true;

		/// <summary>
		/// Switch test long name.
		/// </summary>
		private const string LongName = "testSwitch";

		/// <summary>
		/// Switch test name.
		/// </summary>
		private const string Name = "test";

		/// <summary>
		/// No switch argument names.
		/// </summary>
		private static readonly string[] NoArgumentNames = new string[NoArguments];

		/// <summary>
		/// No switch argument values.
		/// </summary>
		private static readonly string[] NoArgumentValues = new string[NoArguments];

		/// <summary>
		/// No switch arguments.
		/// </summary>
		private const int NoArguments = 0;

		/// <summary>
		/// No switch name.
		/// </summary>
		private const string NoName = null;

		/// <summary>
		/// No switches.
		/// </summary>
		private const int NoSwitches = 0;

		/// <summary>
		/// One switch argument.
		/// </summary>
		private const int OneArgument = 1;

		/// <summary>
		/// One switch in collection.
		/// </summary>
		private const int OneSwitch = 1;

		/// <summary>
		/// Switch has unknown number of arguments.
		/// </summary>
		private const int UnknownNumberArguments = int.MaxValue;

		////////////////////////////////////////////////////////////////////////
		// Helper Methods

		/// <summary>
		/// Called before each test executed.
		/// </summary>
		[SetUp]
		public void BeforeTest()
		{
			Switches = new SwitchCollection();
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Test Add() method defining argument name expected to follow switch.
		/// </summary>
		[Test]
		public void AddWithArgumentName()
		{
			const string ArgumentName = "arg";

			Switches.Add(Name, LongName, Description, IsRequired, ArgumentName);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasEnoughNames);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);

			string[] argumentNames = switchObject.GetArgumentNames();

			Assert.AreEqual(OneArgument, argumentNames.Length);
			Assert.AreEqual(ArgumentName, argumentNames[0]);
		}

		/// <summary>
		/// Test Add() method defining argument names expected to follow switch.
		/// </summary>
		[Test]
		public void AddWithArgumentNames()
		{
			string[] argumentNames = new string[] { "arg1", "arg2", "arg3" };

			Switches.Add(Name, LongName, Description, IsRequired, argumentNames);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(argumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(argumentNames.Length, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasEnoughNames);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining name of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void AddWithArgumentsName()
		{
			const string ArgumentName = "args";

			Switches.Add(Name, LongName, Description, HasArguments, IsRequired, ArgumentName);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(new string[] { ArgumentName }, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(UnknownNumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasEnoughNames);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch description.
		/// </summary>
		[Test]
		public void AddWithDescription()
		{
			Switches.Add(Name, Description);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsFalse(Switches.HasSwitch(LongName));

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch name with empty name.
		/// </summary>
		[Test]
		public void AddWithEmptyName()
		{
			Assert.Throws<ArgumentException>(delegate { Switches.Add(EmptyName); });
		}

		/// <summary>
		/// Test Add() method defining switch names with empty names.
		/// </summary>
		[Test]
		public void AddWithEmptyNames()
		{
			const int NumberSwitches = 3;

			string[] emptyNames = new string[NumberSwitches];

			Assert.Throws<ArgumentException>(delegate { Switches.Add(emptyNames); });
		}

		/// <summary>
		/// Test Add() method defining whether switch has arguments.
		/// </summary>
		[Test]
		public void AddWithHasArguments()
		{
			Switches.Add(Name, LongName, Description, HasArguments, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(UnknownNumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining whether non-printable switch has arguments.
		/// </summary>
		[Test]
		public void AddWithHasArgumentsNonPrintable()
		{
			Switches.Add(Name, HasArguments, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(UnknownNumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch names with identical names.
		/// </summary>
		[Test]
		public void AddWithIdenticalNames()
		{
			string[] switchNames = new string[] { Name, Name, Name };

			Switches.Add(switchNames);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch optional.
		/// </summary>
		[Test]
		public void AddWithIsOptional()
		{
			Switches.Add(Name, LongName, Description, !IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch required.
		/// </summary>
		[Test]
		public void AddWithIsRequired()
		{
			Switches.Add(Name, LongName, Description, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining non-printable switch required.
		/// </summary>
		[Test]
		public void AddWithIsRequiredNonPrintable()
		{
			Switches.Add(Name, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);
			
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch long name.
		/// </summary>
		[Test]
		public void AddWithLongName()
		{
			Switches.Add(Name, LongName, Description);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch name.
		/// </summary>
		[Test]
		public void AddWithName()
		{
			Switches.Add(Name);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsFalse(Switches.HasSwitch(LongName));

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch names.
		/// </summary>
		[Test]
		public void AddWithNames()
		{
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const string Name3 = "switch3";

			string[] switchNames = new string[] { Name1, Name2, Name3 };

			Switches.Add(switchNames);

			Assert.AreEqual(switchNames.Length, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name1));
			Assert.IsTrue(Switches.HasSwitch(Name2));
			Assert.IsTrue(Switches.HasSwitch(Name3));

			foreach (string switchName in switchNames)
			{
				Switch switchObject = Switches[switchName];

				Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
				Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
				Assert.AreEqual(switchName, switchObject.Name);
				Assert.AreEqual(NoArguments, switchObject.NumberArguments);

				Assert.IsFalse(switchObject.HasArguments);
				Assert.IsFalse(switchObject.HasDescription);
				Assert.IsFalse(switchObject.HasEnoughNames);
				Assert.IsFalse(switchObject.HasEnoughValues);
				Assert.IsFalse(switchObject.HasLongName);
				Assert.IsFalse(switchObject.IsRequired);

				Assert.IsNotNull(switchObject.Name);

				Assert.IsNull(switchObject.Description);
				Assert.IsNull(switchObject.LongName);

				Assert.IsTrue(switchObject.IsOptional);
			}
		}

		/// <summary>
		/// Test Add() method defining negative number of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void AddWithNegativeNumberArguments()
		{
			const int NegativeNumberArguments = -3;

			Switches.Add(Name, LongName, Description, NegativeNumberArguments, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.AreNotEqual(NegativeNumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch name with no name.
		/// </summary>
		[Test]
		public void AddWithNoName()
		{
			Assert.Throws<ArgumentException>(delegate { Switches.Add(NoName); });
		}

		/// <summary>
		/// Test Add() method defining switch names with no names.
		/// </summary>
		[Test]
		public void AddWithNoNames()
		{
			string[] nullNames = null;

			Assert.DoesNotThrow(delegate { Switches.Add(nullNames); });

			Assert.AreEqual(NoSwitches, Switches.Count);

			string[] noNames = new string[NoSwitches];

			Assert.DoesNotThrow(delegate { Switches.Add(noNames); });

			Assert.AreEqual(NoSwitches, Switches.Count);
		}

		/// <summary>
		/// Test Add() method defining no number of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void AddWithNoNumberArguments()
		{
			Switches.Add(Name, LongName, Description, NoArguments, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining number of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void AddWithNumberArguments()
		{
			const int NumberArguments = 3;

			Switches.Add(Name, LongName, Description, NumberArguments);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining number of arguments expected to follow switch and whether switch required.
		/// </summary>
		[Test]
		public void AddWithNumberArgumentsIsRequired()
		{
			const int NumberArguments = 4;

			Switches.Add(Name, LongName, Description, NumberArguments, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining number of arguments expected to follow non-printable switch and whether switch required.
		/// </summary>
		[Test]
		public void AddWithNumberArgumentsIsRequiredNonPrintable()
		{
			const int NumberArguments = 7;

			Switches.Add(Name, NumberArguments, IsRequired);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining number of arguments expected to follow non-printable switch.
		/// </summary>
		[Test]
		public void AddWithNumberArgumentsNonPrintable()
		{
			const int NumberArguments = 5;

			Switches.Add(Name, NumberArguments);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining same switch name and long name.
		/// </summary>
		[Test]
		public void AddWithSameNames()
		{
			Switches.Add(Name, Name, Description);

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.AreNotEqual(Name, switchObject.LongName);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method adding specified Switch object.
		/// </summary>
		[Test]
		public void AddWithSwitch()
		{
			Switches.Add(new Switch(Name, LongName, Description));

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));

			Switch switchObject = Switches[Name];

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(NoArgumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Clear() method.
		/// </summary>
		[Test]
		public void Clear()
		{
			string[] switchNames = new string[] { "switch1", "switch2" };

			Switches.Add(switchNames);

			Assert.AreEqual(switchNames.Length, Switches.Count);

			Switches.Clear();

			Assert.AreEqual(NoSwitches, Switches.Count);
		}

		/// <summary>
		/// Test Contains() method.
		/// </summary>
		[Test]
		public void Contains()
		{
			Switch switchObject = new Switch(Name, LongName, Description);

			Assert.IsFalse(Switches.Contains(switchObject));

			Switches.Add(switchObject);

			Assert.IsTrue(Switches.Contains(switchObject));
		}

		/// <summary>
		/// Test CopyTo() method.
		/// </summary>
		[Test]
		public void CopyTo()
		{
			const int StartIndex = 0;

			string[] switchNames = new string[] { "switch1", "switch2", "switch3" };

			Switch[] switchArray = new Switch[switchNames.Length];

			Switches.Add(switchNames);

			Assert.AreEqual(switchNames.Length, Switches.Count);

			Switches.CopyTo(switchArray, StartIndex);

			for (int index = StartIndex; index < switchArray.Length; index++)
				Assert.AreEqual(switchNames[index], switchArray[index].Name);
		}

		/// <summary>
		/// Test GetEnumerator() method.
		/// </summary>
		[Test]
		public void GetEnumerator()
		{
			string[] switchNames = new string[] { "switch1", "switch2", "switch3" };

			Switches.Add(switchNames);

			Assert.AreEqual(switchNames.Length, Switches.Count);

			IEnumerator<Switch> enumerator = Switches.GetEnumerator();

			for (int index = 0; enumerator.MoveNext(); index++)
			{
				Switch switchObject = enumerator.Current;

				Assert.AreEqual(switchNames[index], switchObject.Name);
			}
		}

		/// <summary>
		/// Test HasSwitch() method.
		/// </summary>
		[Test]
		public void HasSwitch()
		{
			Switches.Add(new Switch(Name, LongName, Description));

			Assert.AreEqual(OneSwitch, Switches.Count);

			Assert.IsFalse(Switches.HasSwitch(EmptyName));
			Assert.IsFalse(Switches.HasSwitch(NoName));

			Assert.IsTrue(Switches.HasSwitch(Name));
			Assert.IsTrue(Switches.HasSwitch(LongName));
		}

		/// <summary>
		/// Test IndexOf() method.
		/// </summary>
		[Test]
		public void IndexOf()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const int Index3 = 2;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const string Name3 = "switch3";
			const int NumberSwitches = 3;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);
			Switch switch3 = new Switch(Name3);

			Switches.Add(switch1);
			Switches.Add(switch2);
			Switches.Add(switch3);

			Assert.AreEqual(NumberSwitches, Switches.Count);

			Assert.AreEqual(Index1, Switches.IndexOf(switch1));
			Assert.AreEqual(Index2, Switches.IndexOf(switch2));
			Assert.AreEqual(Index3, Switches.IndexOf(switch3));
		}

		/// <summary>
		/// Test Insert() method.
		/// </summary>
		[Test]
		public void Insert()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const int NumberSwitches = 2;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);

			Switches.Insert(Index1, switch1);

			Assert.AreEqual(OneSwitch, Switches.Count);
			Assert.AreEqual(switch1, Switches[Index1]);

			Assert.IsTrue(Switches.Contains(switch1));

			Switches.Insert(Index2, switch2);

			Assert.AreEqual(NumberSwitches, Switches.Count);
			Assert.AreEqual(switch1, Switches[Index1]);
			Assert.AreEqual(switch2, Switches[Index2]);

			Assert.IsTrue(Switches.Contains(switch1));
			Assert.IsTrue(Switches.Contains(switch2));
		}

		/// <summary>
		/// Test Remove() method.
		/// </summary>
		[Test]
		public void Remove()
		{
			Switch switchObject = new Switch(Name);

			Switches.Add(switchObject);

			Assert.IsTrue(Switches.Contains(switchObject));

			Switches.Remove(switchObject);

			Assert.IsFalse(Switches.Contains(switchObject));
		}

		/// <summary>
		/// Test RemoveAt() method.
		/// </summary>
		[Test]
		public void RemoveAt()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const int NumberSwitches = 2;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);

			Switches.Insert(Index1, switch1);
			Switches.Insert(Index2, switch2);

			Assert.AreEqual(NumberSwitches, Switches.Count);
			Assert.AreEqual(switch1, Switches[Index1]);
			Assert.AreEqual(switch2, Switches[Index2]);

			Switches.RemoveAt(Index1);

			Assert.AreEqual(OneSwitch, Switches.Count);
			Assert.AreEqual(switch2, Switches[Index1]);

			Assert.IsFalse(Switches.Contains(switch1));

			Switches.RemoveAt(Index1);

			Assert.AreEqual(NoSwitches, Switches.Count);

			Assert.IsFalse(Switches.Contains(switch2));
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Test Count property.
		/// </summary>
		[Test]
		public void Count()
		{
			string[] switchNames = new string[] { "switch1", "switch2", "switch3" };

			Assert.AreEqual(NoSwitches, Switches.Count);

			Switches.Add(switchNames);

			Assert.AreEqual(switchNames.Length, Switches.Count);
		}

		/// <summary>
		/// Test indexer on collection index.
		/// </summary>
		[Test]
		public void IndexIndexer()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const int NumberSwitches = 2;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);

			Switches.Insert(Index1, switch1);
			Switches.Insert(Index2, switch2);

			Assert.AreEqual(NumberSwitches, Switches.Count);
			Assert.AreEqual(switch1, Switches[Index1]);
			Assert.AreEqual(switch2, Switches[Index2]);
		}

		/// <summary>
		/// Test indexer on switch name.
		/// </summary>
		[Test]
		public void NameIndexer()
		{
			Switch switchObject = new Switch(Name, LongName, Description);

			Switches.Add(switchObject);

			Assert.AreEqual(OneSwitch, Switches.Count);
			Assert.AreEqual(switchObject, Switches[Name]);
			Assert.AreEqual(switchObject, Switches[LongName]);
		}

		/// <summary>
		/// Test SortedSwitches property.
		/// </summary>
		[Test]
		public void SortedSwitches()
		{
			string[] switchNames = new string[] { "switch3", "switch1", "switch2" };
			string[] sortedSwitchNames = new string[] { "switch1", "switch2", "switch3" };

			Switches.Add(switchNames);

			Assert.AreEqual(switchNames.Length, Switches.Count);

			IList<Switch> switches = Switches.Switches;

			for (int index = 0; index < switches.Count; index++)
			{
				Switch switchObject = switches[index];

				Assert.AreEqual(switchNames[index], switchObject.Name);
			}

			IList<Switch> sortedSwitches = Switches.SortedSwitches;

			for (int index = 0; index < sortedSwitches.Count; index++)
			{
				Switch switchObject = sortedSwitches[index];

				Assert.AreEqual(sortedSwitchNames[index], switchObject.Name);
			}
		}

		////////////////////////////////////////////////////////////////////////
		// Helper Properties

		/// <summary>
		/// Get/set switch collection.
		/// </summary>
		/// <value>
		/// SwitchCollection representing switch collection.
		/// </value>
		private SwitchCollection Switches { get; set; }
	}
}
