using Commands;
using FileParser;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeKataSafeAutoTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ProcessACorrectFile()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CodeKataSafeAutoTest.Input.txt"))
            {
                var parser = new Parser(stream);
                var processor = new Processor(parser.Parse());
                processor.Process();
                var summaries = processor.Summary();
                Assert.AreEqual(summaries.Count, 3);
                var summary = summaries.First();
                // Alex: 42 miles @ 34 mph
                Assert.AreEqual(summary.Name, "Alex");
                Assert.AreEqual(summary.Miles, 42);
                Assert.AreEqual(summary.Mph, 34);
                // Dan: 39 miles @ 47 mph
                summary = summaries.Skip(1).First();
                Assert.AreEqual(summary.Name, "Dan");
                Assert.AreEqual(summary.Miles, 39);
                Assert.AreEqual(summary.Mph, 47);
                // Bob: 0 miles
                summary = summaries.Skip(2).First();
                Assert.AreEqual(summary.Name, "Bob");
                Assert.AreEqual(summary.Miles, 0);
                Assert.AreEqual(summary.Mph, 0);
            }
        }

        [Test]
        public void ProcessACorrectFileWithBoundries()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CodeKataSafeAutoTest.InputBoundaries.txt"))
            {
                var parser = new Parser(stream);
                var processor = new Processor(parser.Parse());
                processor.Process();
                var summaries = processor.Summary();
                Assert.AreEqual(summaries.Count, 3);
                // Bob: 105 miles @ 52
                var summary = summaries.First();
                Assert.AreEqual(summary.Name, "Bob");
                Assert.AreEqual(summary.Miles, 105);
                Assert.AreEqual(summary.Mph, 52);
                // Alex: 42 miles @ 34 mph
                summary = summaries.Skip(1).First();
                Assert.AreEqual(summary.Name, "Alex");
                Assert.AreEqual(summary.Miles, 42);
                Assert.AreEqual(summary.Mph, 34);
                // Dan: 39 miles @ 47 mph
                summary = summaries.Skip(2).First();
                Assert.AreEqual(summary.Name, "Dan");
                Assert.AreEqual(summary.Miles, 39);
                Assert.AreEqual(summary.Mph, 47);
            }
        }

        [Test]
        public void ProcessACorrectFileWithBoundriesAndErrors()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CodeKataSafeAutoTest.InputBoundariesWithErrors.txt"))
            {
                var parser = new Parser(stream);
                var processor = new Processor(parser.Parse());
                Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("End Time before Start Time"), () => processor.Process());
                var summaries = processor.Summary();
                Assert.AreEqual(summaries.Count, 3);
                // Bob: 105 miles @ 52
                var summary = summaries.First();
                Assert.AreEqual("Alex", summary.Name);
                Assert.AreEqual(42, summary.Miles);
                Assert.AreEqual(34, summary.Mph);
                // Dan: 39 miles @ 47 mph
                // Alex: 42 miles @ 34 mph
                summary = summaries.Skip(1).First();
                Assert.AreEqual("Dan", summary.Name);
                Assert.AreEqual(39, summary.Miles);
                Assert.AreEqual(47, summary.Mph);
                summary = summaries.Skip(2).First();
                Assert.AreEqual("Bob", summary.Name);
                Assert.AreEqual(0, summary.Mph);
                Assert.AreEqual(0, summary.Mph);
            }
        }
    }
}