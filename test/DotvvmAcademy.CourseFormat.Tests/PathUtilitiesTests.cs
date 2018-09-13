using Xunit;

namespace DotvvmAcademy.CourseFormat.Tests
{ 
    public class PathUtilitiesTests
    {
        [Fact]
        public void SourcePath_Normalize_IsNormalized()
        {
            // absolute paths
            Assert.Equal("/", SourcePath.Normalize("/"));
            Assert.Equal("/dir", SourcePath.Normalize("/dir"));
            Assert.Equal("/dir/subdir/file.txt", SourcePath.Normalize("/dir/subdir/file.txt"));
            Assert.Equal("/", SourcePath.Normalize("/dir/.."));
            Assert.Equal("/", SourcePath.Normalize("/.."));
            Assert.Equal("/dir/subdir", SourcePath.Normalize("/./dir/./subdir/./."));

            // ./ relative paths
            Assert.Equal("./dir", SourcePath.Normalize("./dir/subdir/.."));
            Assert.Equal(".", SourcePath.Normalize("./dir1/../dir2/.."));

            // ../ relative paths
            Assert.Equal("..", SourcePath.Normalize("./.."));
            Assert.Equal("..", SourcePath.Normalize("../dir/.."));

            // dir/ relative paths
            Assert.Equal("dir/subdir", SourcePath.Normalize("dir/subdir"));
            Assert.Equal("dir/subdir", SourcePath.Normalize("dir//subdir"));
            Assert.Equal("dir/subdir", SourcePath.Normalize("dir/././subdir/."));
            Assert.Equal("dir/subdir", SourcePath.Normalize("dir/subdir/"));
        }
    }
}