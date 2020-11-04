using Microsoft.VisualStudio.TestTools.UnitTesting;

using AssemblyLib;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using NuGet.Frameworks;

namespace Test
{
    [TestClass]
    public class Test
    {
        private AssemblyNode ass;
        private List<INode> classes = new List<INode>();
        private List<INode> methods = new List<INode>();
        private List<INode> fields = new List<INode>();
        private List<INode> properties = new List<INode>();

        [TestInitialize]
        public void Init()
        {
            ass = new AssemblyNode("..\\..\\..\\Faker.dll");
            int[] indexesOfCheckedNamespaces = { 1, 6 };
            foreach (int ind in indexesOfCheckedNamespaces)
            {
                List<INode> cl = ((NamespaceNode)ass.Namespaces[ind]).Classes;
                classes.AddRange(cl);
                foreach(ClassNode _class in cl)
                {
                    fields.AddRange(_class.Fields);
                    properties.AddRange(_class.Properties);
                    methods.AddRange(_class.Methods);
                }
            }
        }

        [TestMethod]
        public void NamespaceCheck()
        {
            Assert.AreEqual(7, ass.Namespaces.Count);
            string[] expectedNames = { "Faker", "Faker.ValueGenerator", "Faker.ValueGenerator.SystemClassGenerator", "Faker.ValueGenerator.GenericGenerators", 
                "Faker.ValueGenerator.CleverGenerators", "Faker.ValueGenerator", "Faker.ValueGenerator.BaseTypesGenerators", "Faker.TestClasses" };
            bool allCheckingMemberNamesCorrect = true;
            foreach (string s in expectedNames)
            {
                allCheckingMemberNamesCorrect = allCheckingMemberNamesCorrect && NameExists(ass.Namespaces, s);
            }
            Assert.IsTrue(allCheckingMemberNamesCorrect);
        }

        [TestMethod]
        public void ClassesCheck()
        {
            int count = 0;
            foreach(NamespaceNode _namespace in ass.Namespaces)
            {
                count += _namespace.Classes.Count;
            }
            Assert.AreEqual(22,count);
            Assert.AreEqual(7, classes.Count);

            string[] expectedClassesNames = { "public  class GeneratorFactory", "public abstract interface IBaseGenerator", "public  class A", 
                                        "public  class B", "public  class C", "public  class D", "public  class E" };

            Assert.IsTrue(Check(expectedClassesNames,classes));
        }

        [TestMethod]
        public void MethodsCheck()
        {
            Assert.AreEqual(3, methods.Count);

            string[] expectedMethodsNames = { "public static List<IBaseGenerator> CraeteBaseTypesGenerators()", 
                                            "public virtual Object Generate(GeneratorContext generatorContext)","public virtual Boolean CanGenerate(Type t)" };
            Assert.IsTrue(Check(expectedMethodsNames, methods));
        }

        [TestMethod]
        public void FieldssCheck()
        {
            Assert.AreEqual(10, fields.Count);

            string[] expectedFieldsNames = { "public  String str","public  Int32 k","public  Char ch","public  B b","private  Single temp","private  Int64 k",
                                                "public  D d","public  C c","public  E e","public  C c" };
            Assert.IsTrue(Check(expectedFieldsNames, fields));
        }

        [TestMethod]
        public void PropertiesCheck()
        { 
            Assert.AreEqual(2, properties.Count);
            string[] expectedPropertiesNames = { "  Int32 Number { public get;  }", "  String S { public get; public set }" };
            Assert.IsTrue(Check(expectedPropertiesNames, properties));
        }

        private bool NameExists(List<INode> nodes,string expectedName)
        {
            foreach (INode node in nodes)
                if (node.GetFullName().Equals(expectedName))
                    return true;
            return false;
        }

        private bool Check(string[] memberNames,List<INode> members)
        {
            bool allCheckingMemberNamesCorrect = true;
            foreach (string name in memberNames)
                allCheckingMemberNamesCorrect = allCheckingMemberNamesCorrect && NameExists(members, name);
            return allCheckingMemberNamesCorrect;
        }
    }
}
