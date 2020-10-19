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
        private List<List<Type>> tmp;
        public int N { get; set; }

        [TestInitialize]
        public void Init()
        {
            ass = new AssemblyNode("Test.dll");
        }

        [TestMethod]
        public void NamespaceCheck()
        {
            Assembly execAss = Assembly.GetExecutingAssembly();
            Assert.AreEqual(2, ass.Namespaces.Count);
            foreach(NamespaceNode n in ass.Namespaces)
            {
                Assert.IsNotNull(execAss.GetTypes().Where(type => n.Name == (type.Namespace ?? "<>")));
            }
        }

        [TestMethod]
        public void MembersNameCheck()
        {
            string[] memeberNames = { "private AssemblyNode ass", "private List<List<Type>> tmp", "private Boolean NameExists(List<INode> nodes, String expectedName)" , "public Int32 N"};
            ClassNode cl = (ClassNode)((NamespaceNode)ass.Namespaces.ToArray()[1]).Classes.ToArray()[0];
            List<INode> members = new List<INode>();
            members.AddRange(cl.Fields);
            members.AddRange(cl.Properties);
            members.AddRange(cl.Methods);
            bool allCheckingMemberNamesCorrect = true;
            foreach (string name in memeberNames)
                allCheckingMemberNamesCorrect = allCheckingMemberNamesCorrect && NameExists(members,name);
            Assert.IsTrue(allCheckingMemberNamesCorrect);
        }

        private bool NameExists(List<INode> nodes,string expectedName)
        {
            foreach (INode node in nodes)
                if (node.ToString().Equals(expectedName))
                    return true;
            return false;
        }
    }
}
