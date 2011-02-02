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

namespace CSharpCLI.Test.Argument
{
	/// <summary>
	/// NUnit unit tests for Switch class.
	/// </summary>
	[TestFixture]
	public class SwitchTest
	{
		/// <summary>
		/// Switch test description.
		/// </summary>
		const string Description = "Test switch.";

		/// <summary>
		/// Empty switch name.
		/// </summary>
		const string EmptyName = "";

		/// <summary>
		/// Switch has arguments.
		/// </summary>
		const bool HasArguments = true;

		/// <summary>
		/// Switch required.
		/// </summary>
		const bool IsRequired = true;

		/// <summary>
		/// Switch test long name.
		/// </summary>
		const string LongName = "testSwitch";

		/// <summary>
		/// Switch test name.
		/// </summary>
		const string Name = "test";

		/// <summary>
		/// No switch argument names.
		/// </summary>
		static readonly string[] NoArgumentNames = new string[NoArguments];

		/// <summary>
		/// No switch argument values.
		/// </summary>
		static readonly string[] NoArgumentValues = new string[NoArguments];

		/// <summary>
		/// No switch arguments.
		/// </summary>
		const int NoArguments = 0;

		/// <summary>
		/// No switch name.
		/// </summary>
		const string NoName = null;

		/// <summary>
		/// One switch argument.
		/// </summary>
		const int OneArgument = 1;

		/// <summary>
		/// Switch has unknown number of arguments.
		/// </summary>
		const int UnknownNumberArguments = int.MaxValue;

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Test constructor defining argument name expected to follow switch.
		/// switch.
		/// </summary>
		[Test]
		public void WithArgumentName()
		{
			const string ArgumentName = "arg";

			Switch switchObject = new Switch(Name, LongName, Description,
				IsRequired, ArgumentName);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), new string[] { ArgumentName });
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, OneArgument);

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
		/// switch.
		/// </summary>
		[Test]
		public void WithArgumentNames()
		{
			string[] argumentNames = new string[] { "arg1", "arg2", "arg3" };

			Switch switchObject = new Switch(Name, LongName, Description,
				IsRequired, argumentNames);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), argumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, argumentNames.Length);

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
		/// Test constructor specifying name of arguments expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void WithArgumentsName()
		{
			const string ArgumentName = "args";

			Switch switchObject = new Switch(Name, LongName, Description,
				HasArguments, IsRequired, ArgumentName);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), new string[] { ArgumentName });
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, UnknownNumberArguments);

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
		/// Test constructor specifying description.
		/// </summary>
		[Test]
		public void WithDescription()
		{
			Switch switchObject = new Switch(Name, Description);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

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
			Assert.Throws(typeof(ArgumentException),
				delegate { new Switch(EmptyName); });
		}

		/// <summary>
		/// Test constructor specifying arguments expected to follow switch.
		/// </summary>
		[Test]
		public void WithHasArguments()
		{
			Switch switchObject = new Switch(Name, LongName, Description,
				HasArguments, IsRequired);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, UnknownNumberArguments);

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
		/// Test constructor specifying switch optional.
		/// </summary>
		[Test]
		public void WithIsOptional()
		{
			Switch switchObject = new Switch(Name, LongName, Description,
				!IsRequired);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

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
			Switch switchObject = new Switch(Name, LongName, Description,
				IsRequired);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

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
		/// Test constructor specifying long name.
		/// </summary>
		[Test]
		public void WithLongName()
		{
			Switch switchObject = new Switch(Name, LongName, Description);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

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

			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

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
		/// Test constructor defining negative number of arguments expected to
		/// follow switch.
		/// </summary>
		[Test]
		public void WithNegativeNumberArguments()
		{
			const int NegativeNumberArguments = -3;

			Switch switchObject = new Switch(Name, LongName, Description,
				NegativeNumberArguments, IsRequired);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.AreNotEqual(switchObject.NumberArguments, NegativeNumberArguments);

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
			Assert.Throws(typeof(ArgumentException),
				delegate { new Switch(NoName); });
		}

		/// <summary>
		/// Test constructor defining no number of arguments expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void WithNoNumberArguments()
		{
			Switch switchObject = new Switch(Name, LongName, Description,
				NoArguments, IsRequired);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

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
		/// Test constructor defining number of arguments expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void WithNumberArguments()
		{
			const int NumberArguments = 3;

			Switch switchObject = new Switch(Name, LongName, Description,
				NumberArguments, IsRequired);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NumberArguments);

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
		/// Test constructor specifying same name and long name.
		/// </summary>
		[Test]
		public void WithSameNames()
		{
			Switch switchObject = new Switch(Name, Name, Description);

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.AreNotEqual(switchObject.LongName, Name);

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

			Switch switchObject = new Switch(Name, LongName, Description,
				HasArguments, IsRequired);

			// Add first argument name.

			switchObject.AddArgumentName(ArgumentName1);

			Assert.AreEqual(switchObject.GetArgumentNames(), argumentNames1);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.NumberArguments, UnknownNumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);

			Assert.IsTrue(switchObject.HasArguments);

			// Add second argument name.

			switchObject.AddArgumentName(ArgumentName2);

			Assert.AreEqual(switchObject.GetArgumentNames(), argumentNames2);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.NumberArguments, UnknownNumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);

			Assert.IsTrue(switchObject.HasArguments);
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

			Switch switchObject = new Switch(Name, LongName, Description,
				IsRequired, argumentNames);

			// Add first argument value.

			switchObject.AddArgumentValue(ArgumentValue1);

			Assert.AreEqual(switchObject.GetArgumentNames(), argumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), argumentValues1);
			Assert.AreEqual(switchObject.NumberArguments, argumentNames.Length);

			Assert.IsFalse(switchObject.HasEnoughValues);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasEnoughNames);

			// Add second argument value.

			switchObject.AddArgumentValue(ArgumentValue2);

			Assert.AreEqual(switchObject.GetArgumentNames(), argumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), argumentValues2);
			Assert.AreEqual(switchObject.NumberArguments, argumentNames.Length);

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

			Assert.AreEqual(Switch.GetLongPrefixedName(EmptyName), EmptyName);
			Assert.AreEqual(Switch.GetLongPrefixedName(NameWithLongPrefix), NameWithLongPrefix);
			Assert.AreEqual(Switch.GetLongPrefixedName(NameWithPrefix), NameWithPrefix);
			Assert.AreEqual(Switch.GetLongPrefixedName(NoName), NoName);
			Assert.AreEqual(Switch.GetLongPrefixedName(Name), NameWithLongPrefix);

			Assert.AreNotEqual(Switch.GetLongPrefixedName(Name), Name);
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

			Assert.AreEqual(Switch.GetName(ArgumentWithLongPrefix), Name);
			Assert.AreEqual(Switch.GetName(ArgumentWithNoPrefix), ArgumentWithNoPrefix);
			Assert.AreEqual(Switch.GetName(ArgumentWithPrefix), Name);
			Assert.AreEqual(Switch.GetName(ArgumentWithUnknownPrefix), ArgumentWithUnknownPrefix);
			Assert.AreEqual(Switch.GetName(EmptyArgument), EmptyArgument);
			Assert.AreEqual(Switch.GetName(NoArgument), NoArgument);

			Assert.AreNotEqual(Switch.GetName(ArgumentWithLongPrefix), ArgumentWithLongPrefix);
			Assert.AreNotEqual(Switch.GetName(ArgumentWithPrefix), ArgumentWithPrefix);
			Assert.AreNotEqual(Switch.GetName(ArgumentWithUnknownPrefix), Name);
		}

		/// <summary>
		/// Test static GetPrefixedName() method.
		/// </summary>
		[Test]
		public void GetPrefixedName()
		{
			const string NameWithLongPrefix = Switch.LongPrefix + Name;
			const string NameWithPrefix = Switch.Prefix + Name;

			Assert.AreEqual(Switch.GetPrefixedName(EmptyName), EmptyName);
			Assert.AreEqual(Switch.GetPrefixedName(Name), NameWithPrefix);
			Assert.AreEqual(Switch.GetPrefixedName(NameWithLongPrefix), NameWithLongPrefix);
			Assert.AreEqual(Switch.GetPrefixedName(NameWithPrefix), NameWithPrefix);
			Assert.AreEqual(Switch.GetPrefixedName(NoName), NoName);

			Assert.AreNotEqual(Switch.GetPrefixedName(Name), Name);
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
	}
}
