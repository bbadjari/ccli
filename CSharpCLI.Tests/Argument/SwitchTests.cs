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
using CSharpCLI.Argument;
using NUnit.Framework;

namespace CSharpCLI.Tests.Argument
{
	/// <summary>
	/// Unit tests for Switch class.
	/// </summary>
	[TestFixture]
	public class SwitchTests
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
		/// Switch has no arguments.
		/// </summary>
		private const bool HasNoArguments = false;

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
		/// No switch description.
		/// </summary>
		private const string NoDescription = null;

		/// <summary>
		/// No switch long name.
		/// </summary>
		private const string NoLongName = null;

		/// <summary>
		/// No switch name.
		/// </summary>
		private const string NoName = null;

		/// <summary>
		/// One switch argument.
		/// </summary>
		private const int OneArgument = 1;

		/// <summary>
		/// Switch has unknown number of arguments.
		/// </summary>
		private const int UnknownNumberArguments = int.MaxValue;

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Test constructor defining argument name expected to follow switch.
		/// </summary>
		[Test]
		public void WithArgumentName()
		{
			const string ArgumentName = "arg";

			Switch switchObject = new Switch(Name, LongName, Description, IsRequired, ArgumentName);

			Assert.AreEqual(Description, switchObject.Description);
			Assert.AreEqual(new string[] { ArgumentName }, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(LongName, switchObject.LongName);
			Assert.AreEqual(Name, switchObject.Name);
			Assert.AreEqual(OneArgument, switchObject.NumberArguments);

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
		/// Test constructor defining argument names expected to follow switch.
		/// </summary>
		[Test]
		public void WithArgumentNames()
		{
			string[] argumentNames = new string[] { "arg1", "arg2", "arg3" };

			Switch switchObject = new Switch(Name, LongName, Description, IsRequired, argumentNames);

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
		/// Test constructor specifying name of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void WithArgumentsName()
		{
			const string ArgumentName = "args";

			Switch switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired, ArgumentName);

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
		/// Test constructor specifying description.
		/// </summary>
		[Test]
		public void WithDescription()
		{
			Switch switchObject = new Switch(Name, Description);

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
		/// Test constructor specifying name with empty name.
		/// </summary>
		[Test]
		public void WithEmptyName()
		{
			Assert.Throws<ArgumentException>(delegate { new Switch(EmptyName); });
		}

		/// <summary>
		/// Test constructor specifying arguments expected to follow switch.
		/// </summary>
		[Test]
		public void WithHasArguments()
		{
			Switch switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired);

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
		/// Test constructor specifying arguments expected to follow non-printable switch.
		/// </summary>
		[Test]
		public void WithHasArgumentsNonPrintable()
		{
			Switch switchObject = new Switch(Name, HasArguments, IsRequired);

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
		/// Test constructor specifying no arguments expected to follow non-printable switch.
		/// </summary>
		[Test]
		public void WithHasNoArgumentsNonPrintable()
		{
			Switch switchObject = new Switch(Name, HasNoArguments, IsRequired);

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
		/// Test constructor specifying switch optional.
		/// </summary>
		[Test]
		public void WithIsOptional()
		{
			Switch switchObject = new Switch(Name, LongName, Description, !IsRequired);

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
		/// Test constructor specifying switch required.
		/// </summary>
		[Test]
		public void WithIsRequired()
		{
			Switch switchObject = new Switch(Name, LongName, Description, IsRequired);

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
		/// Test constructor specifying non-printable switch required.
		/// </summary>
		[Test]
		public void WithIsRequiredNonPrintable()
		{
			Switch switchObject = new Switch(Name, IsRequired);

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
		/// Test constructor specifying long name.
		/// </summary>
		[Test]
		public void WithLongName()
		{
			Switch switchObject = new Switch(Name, LongName, Description);

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
		/// Test constructor specifying name.
		/// </summary>
		[Test]
		public void WithName()
		{
			Switch switchObject = new Switch(Name);

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
		/// Test constructor defining negative number of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void WithNegativeNumberArguments()
		{
			const int NegativeNumberArguments = -3;

			Switch switchObject = new Switch(Name, LongName, Description, NegativeNumberArguments, IsRequired);

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
		/// Test constructor specifying name with no name.
		/// </summary>
		[Test]
		public void WithNoName()
		{
			Assert.Throws<ArgumentException>(delegate { new Switch(NoName); });
		}

		/// <summary>
		/// Test constructor defining no number of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void WithNoNumberArguments()
		{
			Switch switchObject = new Switch(Name, LongName, Description, NoArguments, IsRequired);

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
		/// Test constructor defining number of arguments expected to follow switch.
		/// </summary>
		[Test]
		public void WithNumberArguments()
		{
			const int NumberArguments = 2;

			Switch switchObject = new Switch(Name, LongName, Description, NumberArguments);

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
		/// Test constructor defining number of arguments expected to follow switch and whether switch required.
		/// </summary>
		[Test]
		public void WithNumberArgumentsIsRequired()
		{
			const int NumberArguments = 3;

			Switch switchObject = new Switch(Name, LongName, Description, NumberArguments, IsRequired);

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
		/// Test constructor defining number of arguments expected to follow non-printable switch and whether switch required.
		/// </summary>
		[Test]
		public void WithNumberArgumentsIsRequiredNonPrintable()
		{
			const int NumberArguments = 7;

			Switch switchObject = new Switch(Name, NumberArguments, IsRequired);

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
		/// Test constructor defining number of arguments expected to follow non-printable switch.
		/// </summary>
		[Test]
		public void WithNumberArgumentsNonPrintable()
		{
			const int NumberArguments = 5;

			Switch switchObject = new Switch(Name, NumberArguments);

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
		/// Test constructor specifying same name and long name.
		/// </summary>
		[Test]
		public void WithSameNames()
		{
			Switch switchObject = new Switch(Name, Name, Description);

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

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Test AddArgumentName() method.
		/// </summary>
		[Test]
		public void AddArgumentName()
		{
			const string ArgumentName1 = "arg1";
			const string ArgumentName2 = "arg2";

			string[] argumentNames1 = new string[] { ArgumentName1 };
			string[] argumentNames2 = new string[] { ArgumentName1, ArgumentName2 };

			Switch switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired);

			// Add first argument name.

			switchObject.AddArgumentName(ArgumentName1);

			Assert.AreEqual(argumentNames1, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(UnknownNumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughValues);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasEnoughNames);

			// Add second argument name.

			switchObject.AddArgumentName(ArgumentName2);

			Assert.AreEqual(argumentNames2, switchObject.GetArgumentNames());
			Assert.AreEqual(NoArgumentValues, switchObject.GetArgumentValues());
			Assert.AreEqual(UnknownNumberArguments, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughValues);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasEnoughNames);
		}

		/// <summary>
		/// Test AddArgumentValue() method.
		/// </summary>
		[Test]
		public void AddArgumentValue()
		{
			const string ArgumentValue1 = "value1";
			const string ArgumentValue2 = "value2";

			string[] argumentNames = new string[] { "arg1", "arg2" };
			string[] argumentValues1 = new string[] { ArgumentValue1 };
			string[] argumentValues2 = new string[] { ArgumentValue1, ArgumentValue2 };

			Switch switchObject = new Switch(Name, LongName, Description, IsRequired, argumentNames);

			// Add first argument value.

			switchObject.AddArgumentValue(ArgumentValue1);

			Assert.AreEqual(argumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(argumentValues1, switchObject.GetArgumentValues());
			Assert.AreEqual(argumentNames.Length, switchObject.NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughValues);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasEnoughNames);

			// Add second argument value.

			switchObject.AddArgumentValue(ArgumentValue2);

			Assert.AreEqual(argumentNames, switchObject.GetArgumentNames());
			Assert.AreEqual(argumentValues2, switchObject.GetArgumentValues());
			Assert.AreEqual(argumentNames.Length, switchObject.NumberArguments);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasEnoughNames);
			Assert.IsTrue(switchObject.HasEnoughValues);
		}

		////////////////////////////////////////////////////////////////////////
		// Static Methods

		/// <summary>
		/// Test static GetLongPrefixedName() method.
		/// </summary>
		[Test]
		public void GetLongPrefixedName()
		{
			const string NameWithLongPrefix = Switch.LongPrefix + Name;
			const string NameWithPrefix = Switch.Prefix + Name;

			Assert.AreEqual(EmptyName, Switch.GetLongPrefixedName(EmptyName));
			Assert.AreEqual(NameWithLongPrefix, Switch.GetLongPrefixedName(NameWithLongPrefix));
			Assert.AreEqual(NameWithPrefix, Switch.GetLongPrefixedName(NameWithPrefix));
			Assert.AreEqual(NoName, Switch.GetLongPrefixedName(NoName));
			Assert.AreEqual(NameWithLongPrefix, Switch.GetLongPrefixedName(Name));

			Assert.AreNotEqual(Name, Switch.GetLongPrefixedName(Name));
		}

		/// <summary>
		/// Test static GetName() method.
		/// </summary>
		[Test]
		public void GetName()
		{
			const string ArgumentWithNoPrefix = "test";
			const string ArgumentWithLongPrefix = Switch.LongPrefix + ArgumentWithNoPrefix;
			const string ArgumentWithPrefix = Switch.Prefix + ArgumentWithNoPrefix;
			const string ArgumentWithUnknownPrefix = "/" + ArgumentWithNoPrefix;
			const string EmptyArgument = "";
			const string NoArgument = null;

			Assert.AreEqual(Name, Switch.GetName(ArgumentWithLongPrefix));
			Assert.AreEqual(ArgumentWithNoPrefix, Switch.GetName(ArgumentWithNoPrefix));
			Assert.AreEqual(Name, Switch.GetName(ArgumentWithPrefix));
			Assert.AreEqual(ArgumentWithUnknownPrefix, Switch.GetName(ArgumentWithUnknownPrefix));
			Assert.AreEqual(EmptyArgument, Switch.GetName(EmptyArgument));
			Assert.AreEqual(NoArgument, Switch.GetName(NoArgument));

			Assert.AreNotEqual(ArgumentWithLongPrefix, Switch.GetName(ArgumentWithLongPrefix));
			Assert.AreNotEqual(ArgumentWithPrefix, Switch.GetName(ArgumentWithPrefix));
			Assert.AreNotEqual(Name, Switch.GetName(ArgumentWithUnknownPrefix));
		}

		/// <summary>
		/// Test static GetPrefixedName() method.
		/// </summary>
		[Test]
		public void GetPrefixedName()
		{
			const string NameWithLongPrefix = Switch.LongPrefix + Name;
			const string NameWithPrefix = Switch.Prefix + Name;

			Assert.AreEqual(EmptyName, Switch.GetPrefixedName(EmptyName));
			Assert.AreEqual(NameWithPrefix, Switch.GetPrefixedName(Name));
			Assert.AreEqual(NameWithLongPrefix, Switch.GetPrefixedName(NameWithLongPrefix));
			Assert.AreEqual(NameWithPrefix, Switch.GetPrefixedName(NameWithPrefix));
			Assert.AreEqual(NoName, Switch.GetPrefixedName(NoName));

			Assert.AreNotEqual(Name, Switch.GetPrefixedName(Name));
		}

		/// <summary>
		/// Test static IsValid() method.
		/// </summary>
		[Test]
		public void IsValid()
		{
			const string ArgumentWithNoPrefix = "test";
			const string ArgumentWithLongPrefix = Switch.LongPrefix + ArgumentWithNoPrefix;
			const string ArgumentWithPrefix = Switch.Prefix + ArgumentWithNoPrefix;
			const string ArgumentWithUnknownPrefix = "/" + ArgumentWithNoPrefix;
			const string EmptyArgument = "";
			const string NoArgument = null;

			Assert.IsFalse(Switch.IsValid(ArgumentWithNoPrefix));
			Assert.IsFalse(Switch.IsValid(ArgumentWithUnknownPrefix));
			Assert.IsFalse(Switch.IsValid(EmptyArgument));
			Assert.IsFalse(Switch.IsValid(NoArgument));

			Assert.IsTrue(Switch.IsValid(ArgumentWithLongPrefix));
			Assert.IsTrue(Switch.IsValid(ArgumentWithPrefix));
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Test Description property.
		/// </summary>
		[Test]
		public void DescriptionProperty()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsNull(switchObject.Description);

			switchObject = new Switch(Name, Description);

			Assert.AreEqual(Description, switchObject.Description);
		}

		/// <summary>
		/// Test HasAllNames property.
		/// </summary>
		[Test]
		public void HasAllNames()
		{
			const string ArgumentName1 = "arg1";
			const string ArgumentName2 = "arg2";
			const string ArgumentName3 = "arg3";
			const string ArgumentName4 = "arg4";
			const int NumberArguments = 3;

			// Test when number of arguments known.

			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasAllNames);

			switchObject = new Switch(Name, LongName, Description, NumberArguments, IsRequired);

			Assert.IsFalse(switchObject.HasAllNames);

			switchObject.AddArgumentName(ArgumentName1);

			Assert.IsFalse(switchObject.HasAllNames);

			switchObject.AddArgumentName(ArgumentName2);

			Assert.IsFalse(switchObject.HasAllNames);

			switchObject.AddArgumentName(ArgumentName3);

			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentNames);

			Assert.IsTrue(switchObject.HasAllNames);

			// Add extra argument name, exceeding necessary number.

			switchObject.AddArgumentName(ArgumentName4);

			// Extra argument name should not be added.
			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentNames);

			Assert.IsTrue(switchObject.HasAllNames);

			// Test when number of arguments unknown.

			switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired, ArgumentName1);

			Assert.IsFalse(switchObject.HasAllNames);

			switchObject.AddArgumentName(ArgumentName2);

			Assert.IsFalse(switchObject.HasAllNames);
		}

		/// <summary>
		/// Test HasAllValues property.
		/// </summary>
		[Test]
		public void HasAllValues()
		{
			const string ArgumentName = "args";
			const int NumberArguments = 3;
			const string Value1 = "arg1";
			const string Value2 = "arg2";
			const string Value3 = "arg3";
			const string Value4 = "arg4";

			// Test when number of arguments known.

			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasEnoughValues);

			switchObject = new Switch(Name, LongName, Description, NumberArguments, IsRequired);

			Assert.IsFalse(switchObject.HasAllValues);

			switchObject.AddArgumentValue(Value1);

			Assert.IsFalse(switchObject.HasAllValues);

			switchObject.AddArgumentValue(Value2);

			Assert.IsFalse(switchObject.HasAllValues);

			switchObject.AddArgumentValue(Value3);

			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentValues);

			Assert.IsTrue(switchObject.HasAllValues);

			// Add extra argument value, exceeding necessary number.

			switchObject.AddArgumentValue(Value4);

			// Extra argument value should not be added.
			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentValues);

			Assert.IsTrue(switchObject.HasAllValues);

			// Test when number of arguments unknown.

			switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired, ArgumentName);

			Assert.IsFalse(switchObject.HasAllValues);

			switchObject.AddArgumentValue(Value1);

			Assert.IsFalse(switchObject.HasAllValues);
		}

		/// <summary>
		/// Test HasArguments property.
		/// </summary>
		[Test]
		public void HasArgumentsProperty()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasArguments);

			switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired);

			Assert.IsTrue(switchObject.HasArguments);
		}

		/// <summary>
		/// Test HasDescription property.
		/// </summary>
		[Test]
		public void HasDescription()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasDescription);

			switchObject = new Switch(Name, Description);

			Assert.IsTrue(switchObject.HasDescription);
		}

		/// <summary>
		/// Test HasEnoughNames property.
		/// </summary>
		[Test]
		public void HasEnoughNames()
		{
			const string ArgumentName1 = "arg1";
			const string ArgumentName2 = "arg2";
			const string ArgumentName3 = "arg3";
			const string ArgumentName4 = "arg4";
			const int NumberArguments = 3;

			// Test when number of arguments known.

			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasEnoughNames);

			switchObject = new Switch(Name, LongName, Description, NumberArguments, IsRequired);

			Assert.IsFalse(switchObject.HasEnoughNames);

			switchObject.AddArgumentName(ArgumentName1);

			Assert.IsFalse(switchObject.HasEnoughNames);

			switchObject.AddArgumentName(ArgumentName2);

			Assert.IsFalse(switchObject.HasEnoughNames);

			switchObject.AddArgumentName(ArgumentName3);

			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentNames);

			Assert.IsTrue(switchObject.HasEnoughNames);

			// Add extra argument name, exceeding necessary number.

			switchObject.AddArgumentName(ArgumentName4);

			// Extra argument name should not be added.
			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentNames);

			Assert.IsTrue(switchObject.HasEnoughNames);

			// Test when number of arguments unknown.

			switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired, ArgumentName1);

			Assert.IsTrue(switchObject.HasEnoughNames);
		}

		/// <summary>
		/// Test HasEnoughValues property.
		/// </summary>
		[Test]
		public void HasEnoughValues()
		{
			const string ArgumentName = "args";
			const int NumberArguments = 3;
			const string Value1 = "arg1";
			const string Value2 = "arg2";
			const string Value3 = "arg3";
			const string Value4 = "arg4";

			// Test when number of arguments known.

			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasEnoughValues);

			switchObject = new Switch(Name, LongName, Description, NumberArguments, IsRequired);

			Assert.IsFalse(switchObject.HasEnoughValues);

			switchObject.AddArgumentValue(Value1);

			Assert.IsFalse(switchObject.HasEnoughValues);

			switchObject.AddArgumentValue(Value2);

			Assert.IsFalse(switchObject.HasEnoughValues);

			switchObject.AddArgumentValue(Value3);

			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentValues);

			Assert.IsTrue(switchObject.HasEnoughValues);

			// Add extra argument value, exceeding necessary number.

			switchObject.AddArgumentValue(Value4);

			// Extra argument value should not be added.
			Assert.AreEqual(NumberArguments, switchObject.NumberArgumentValues);

			Assert.IsTrue(switchObject.HasEnoughValues);

			// Test when number of arguments unknown.

			switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired, ArgumentName);

			Assert.IsFalse(switchObject.HasEnoughValues);

			switchObject.AddArgumentValue(Value1);

			Assert.IsTrue(switchObject.HasEnoughValues);
		}

		/// <summary>
		/// Test HasLongName property.
		/// </summary>
		[Test]
		public void HasLongName()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.HasLongName);

			switchObject = new Switch(Name, LongName, Description);

			Assert.IsTrue(switchObject.HasLongName);
		}

		/// <summary>
		/// Test IsNonPrintable property.
		/// </summary>
		[Test]
		public void IsNonPrintable()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsTrue(switchObject.IsNonPrintable);

			switchObject = new Switch(Name, HasArguments, IsRequired);

			Assert.IsTrue(switchObject.IsNonPrintable);

			switchObject = new Switch(Name, NoLongName, NoDescription);

			Assert.IsTrue(switchObject.IsNonPrintable);

			switchObject = new Switch(Name, Description);

			Assert.IsFalse(switchObject.IsNonPrintable);

			switchObject = new Switch(Name, LongName, Description);

			Assert.IsFalse(switchObject.IsNonPrintable);

			switchObject = new Switch(Name, LongName, NoDescription);

			Assert.IsFalse(switchObject.IsNonPrintable);

			switchObject = new Switch(Name, NoLongName, Description);

			Assert.IsFalse(switchObject.IsNonPrintable);
		}

		/// <summary>
		/// Test IsOptional property.
		/// </summary>
		[Test]
		public void IsOptional()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsTrue(switchObject.IsOptional);

			switchObject = new Switch(Name, LongName, Description, IsRequired);

			Assert.IsFalse(switchObject.IsOptional);
		}

		/// <summary>
		/// Test IsRequired property.
		/// </summary>
		[Test]
		public void IsRequiredProperty()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsFalse(switchObject.IsRequired);

			switchObject = new Switch(Name, LongName, Description, IsRequired);

			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test LongName property.
		/// </summary>
		[Test]
		public void LongNameProperty()
		{
			Switch switchObject = new Switch(Name);

			Assert.IsNull(switchObject.LongName);

			switchObject = new Switch(Name, LongName, Description);

			Assert.AreEqual(LongName, switchObject.LongName);
		}

		/// <summary>
		/// Test Name property.
		/// </summary>
		[Test]
		public void NameProperty()
		{
			Switch switchObject = new Switch(Name);

			Assert.AreEqual(Name, switchObject.Name);
		}

		/// <summary>
		/// Test NumberArgumentNames property.
		/// </summary>
		[Test]
		public void NumberArgumentNames()
		{
			string[] argumentNames = new string[] { "arg1", "arg2", "arg3" };

			Switch switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired);

			int numberArgumentNames = 0;

			Assert.AreEqual(numberArgumentNames, switchObject.NumberArgumentNames);

			foreach (string argumentName in argumentNames)
			{
				switchObject.AddArgumentName(argumentName);

				numberArgumentNames++;

				Assert.AreEqual(numberArgumentNames, switchObject.NumberArgumentNames);
			}
		}

		/// <summary>
		/// Test NumberArgumentValues property.
		/// </summary>
		[Test]
		public void NumberArgumentValues()
		{
			string[] argumentValues = new string[] { "arg1", "arg2", "arg3" };

			Switch switchObject = new Switch(Name, LongName, Description, HasArguments, IsRequired);

			int numberArgumentValues = 0;

			Assert.AreEqual(numberArgumentValues, switchObject.NumberArgumentValues);

			foreach (string argumentValue in argumentValues)
			{
				switchObject.AddArgumentValue(argumentValue);

				numberArgumentValues++;

				Assert.AreEqual(numberArgumentValues, switchObject.NumberArgumentValues);
			}
		}

		/// <summary>
		/// Test NumberArguments property.
		/// </summary>
		[Test]
		public void NumberArgumentsProperty()
		{
			const int NumberArguments = 3;

			Switch switchObject = new Switch(Name);

			Assert.AreEqual(NoArguments, switchObject.NumberArguments);

			switchObject = new Switch(Name, LongName, Description, NumberArguments, IsRequired);

			Assert.AreEqual(NumberArguments, switchObject.NumberArguments);
		}
	}
}
