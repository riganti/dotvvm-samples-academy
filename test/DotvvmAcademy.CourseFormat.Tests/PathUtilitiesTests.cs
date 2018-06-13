using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotvvmAcademy.CourseFormat.Tests
{
    [TestClass]
    public class PathUtilitiesTests
    {
        [TestMethod]
        public void BasicNormalizeTests()
        {
            // absolute paths
            Assert.AreEqual("/", PathUtilities.Normalize("/"));
            Assert.AreEqual("/dir", PathUtilities.Normalize("/dir"));
            Assert.AreEqual("/dir/subdir/file.txt", PathUtilities.Normalize("/dir/subdir/file.txt"));
            Assert.AreEqual("/", PathUtilities.Normalize("/dir/.."));
            Assert.AreEqual("/", PathUtilities.Normalize("/.."));
            Assert.AreEqual("/dir/subdir", PathUtilities.Normalize("/./dir/./subdir/./."));

            // ./ relative paths
            Assert.AreEqual("./dir", PathUtilities.Normalize("./dir/subdir/.."));
            Assert.AreEqual(".", PathUtilities.Normalize("./dir1/../dir2/.."));

            // ../ relative paths
            Assert.AreEqual("..", PathUtilities.Normalize("./.."));
            Assert.AreEqual("..", PathUtilities.Normalize("../dir/.."));

            // dir/ relative paths
            Assert.AreEqual("dir/subdir", PathUtilities.Normalize("dir/subdir"));
            Assert.AreEqual("dir/subdir", PathUtilities.Normalize("dir//subdir"));
            Assert.AreEqual("dir/subdir", PathUtilities.Normalize("dir/././subdir/."));
            Assert.AreEqual("dir/subdir", PathUtilities.Normalize("dir/subdir/"));
        }
    }
}