namespace Stact.Specs.Activator
{
	using System;
	using Classes;
	using Magnum;
	using Magnum.Reflection;
	using NUnit.Framework;
	using Magnum.TestFramework;

	[TestFixture]
	public class When_generating_an_object_with_arguments
	{
		private ClassWithOneConstructorArg _instance;

		[Test]
		public void The_object_should_be_created()
		{
			const int expected = 47;

			_instance = FastActivator<ClassWithOneConstructorArg>.Create(expected);

			_instance.ShouldNotBeNull();
			_instance.ShouldBeAnInstanceOf<ClassWithOneConstructorArg>();
			_instance.Value.ShouldEqual(expected);
			_instance.Name.ShouldBeNull();
		}

		[Test]
		public void The_object_should_be_created_with_the_name()
		{
			const string expected = "The Name";

			_instance = FastActivator<ClassWithOneConstructorArg>.Create(expected);

			_instance.ShouldNotBeNull();
			_instance.ShouldBeAnInstanceOf<ClassWithOneConstructorArg>();
			_instance.Value.ShouldEqual(default(int));
			_instance.Name.ShouldEqual(expected);
		}

		[Test]
		public void The_object_should_be_created_with_a_null_argument()
		{
			_instance = FastActivator<ClassWithOneConstructorArg>.Create<string>(null);

			_instance.ShouldNotBeNull();
			_instance.ShouldBeAnInstanceOf<ClassWithOneConstructorArg>();
			_instance.Value.ShouldEqual(default(int));
			_instance.Name.ShouldEqual(null);
		}

		[Test]
		public void The_object_should_be_created_with_a_null_argument_via_type()
		{
			_instance = (ClassWithOneConstructorArg)FastActivator.Create(typeof(ClassWithOneConstructorArg), new object[]{null});

			_instance.ShouldNotBeNull();
			_instance.ShouldBeAnInstanceOf<ClassWithOneConstructorArg>();
			_instance.Value.ShouldEqual(default(int));
			_instance.Name.ShouldEqual(null);
		}

		[Test]
		public void The_object_should_be_created_with_the_id()
		{
			Guid expected = CombGuid.Generate();

			_instance = FastActivator<ClassWithOneConstructorArg>.Create(expected);

			_instance.ShouldNotBeNull();
			_instance.ShouldBeAnInstanceOf<ClassWithOneConstructorArg>();
			_instance.Value.ShouldEqual(default(int));
			_instance.Name.ShouldBeNull();
			_instance.Id.ShouldEqual(expected);
		}
	}

	[TestFixture]
	public class When_generating_an_object_with_two_arguments
	{
		private ClassWithTwoConstructorArgs _instance;

		[Test]
		public void The_object_should_be_created()
		{
			const int expected = 47;
			const string expectedName = "The Name";

			_instance = FastActivator<ClassWithTwoConstructorArgs>.Create(expected, expectedName);

			_instance.ShouldNotBeNull();
			_instance.ShouldBeAnInstanceOf<ClassWithTwoConstructorArgs>();
			_instance.Value.ShouldEqual(expected);
			_instance.Name.ShouldEqual(expectedName);
		}
	}
}