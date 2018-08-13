using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotvvmAcademy.CourseFormat.Tests
{
    [TestClass]
    public class PathUtilitiesTests
    {
        [TestMethod]
        public void BasicNormalizeTest()
        {
            // absolute paths
            Assert.AreEqual("/", SourcePath.Normalize("/"));
            Assert.AreEqual("/dir", SourcePath.Normalize("/dir"));
            Assert.AreEqual("/dir/subdir/file.txt", SourcePath.Normalize("/dir/subdir/file.txt"));
            Assert.AreEqual("/", SourcePath.Normalize("/dir/.."));
            Assert.AreEqual("/", SourcePath.Normalize("/.."));
            Assert.AreEqual("/dir/subdir", SourcePath.Normalize("/./dir/./subdir/./."));

            // ./ relative paths
            Assert.AreEqual("./dir", SourcePath.Normalize("./dir/subdir/.."));
            Assert.AreEqual(".", SourcePath.Normalize("./dir1/../dir2/.."));

            // ../ relative paths
            Assert.AreEqual("..", SourcePath.Normalize("./.."));
            Assert.AreEqual("..", SourcePath.Normalize("../dir/.."));

            // dir/ relative paths
            Assert.AreEqual("dir/subdir", SourcePath.Normalize("dir/subdir"));
            Assert.AreEqual("dir/subdir", SourcePath.Normalize("dir//subdir"));
            Assert.AreEqual("dir/subdir", SourcePath.Normalize("dir/././subdir/."));
            Assert.AreEqual("dir/subdir", SourcePath.Normalize("dir/subdir/"));
        }
    }
}