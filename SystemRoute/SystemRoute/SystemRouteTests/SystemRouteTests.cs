using NUnit.Framework;
using System.IO;
using SystemRoute;

namespace SystemRouteTests {
    public class SystemRouteTests {

        public static string TestAbsolutePath = Path.GetTempPath().Replace("\\", "/");

        [Test]
        public void Unit_ToAbsolutePathSubFolderFile() {
            string path = "folder/filename.txt";

            string copy = path.Build();
            copy = copy.ForceToAbsolutePath(TestAbsolutePath);
            Assert.AreEqual(TestAbsolutePath + path, copy);
        }

        [Test]
        public void Unit_ToAbsolutePath() {
            string path = "folder2/";

            string copy = path.Build();
            copy = copy.ForceToAbsolutePath(TestAbsolutePath);
            Assert.AreEqual(TestAbsolutePath + path, copy);
        }

        [Test]
        public void Unit_ToAbsolutePathOnlyFile() {
            string path = "filename.txt";

            string copy = path.Build();
            copy = copy.ForceToAbsolutePath(TestAbsolutePath);
            Assert.AreEqual(TestAbsolutePath + path, copy);
        }

        [Test]
        public void Unit_ToAbsoluteFileInAbsolute() {
            string path = TestAbsolutePath + "filename.pdf";

            string copy = path.Build();
            copy = copy.ForceToAbsolutePath(TestAbsolutePath);
            Assert.AreEqual(path, copy);
        }

        [Test]
        public void Units_Build() {
            Assert.AreEqual("", "".Build());

            Assert.AreEqual("filename.pdf", @"filename.pdf".Build());
            Assert.AreEqual("filename.pdf2", @"filename.pdf2".Build());
            Assert.AreEqual("filename.pdfcopy", @"filename.pdfcopy".Build());

            Assert.AreEqual("folder3/filename.pdf", @"folder3/filename.pdf".Build());
            Assert.AreEqual("folder3/filename.pdf", @"folder3\filename.pdf".Build());
            Assert.AreEqual("C:/folder3/filename.pdf", @"\folder3\filename.pdf".Build());


            Assert.AreEqual("C:/folder3/filename.pdf", @"C:\folder3/filename.pdf".Build());
            Assert.AreEqual("C:/folder3/filename.pdf2", @"C:\folder3\filename.pdf2".Build());
            Assert.AreEqual("C:/folder3/filename.txt", @"C:\folder3\\filename.txt".Build());
        }

        [Test]
        public void Units_Build2() {
            Assert.AreEqual("path/filename.png", "path".Build("filename.png"));
            Assert.AreEqual("path/filename.png", "path/".Build("filename.png"));
            Assert.AreEqual("path/filename.png", "path/".Build("/filename.png"));
            Assert.AreEqual("path/filename.png", "path".Build("/filename.png"));

            Assert.AreEqual("C:/path/filename.png", "/path".Build("filename.png"));
            Assert.AreEqual("C:/path/filename.png", "/path/".Build("filename.png"));
            Assert.AreEqual("C:/path/filename.png", "/path/".Build("/filename.png"));
            Assert.AreEqual("C:/path/filename.png", "/path".Build("/filename.png"));


            Assert.AreEqual("la/path/filename.png", "la/path".Build("filename.png"));
            //there may be folders that contain points so that behavior is correct
            Assert.AreEqual("la/path/game.awd/filename.png", "la/path/game.awd".Build("filename.png"));
        }

        [Test]
        public void Units_GetPath() {
            Assert.AreEqual("folder1/folder2/folder3/", "folder1/folder2/folder3/folder1.jpg".Build().GetPath());
            Assert.AreEqual("folder1/folder2/folder3/", "folder1/folder2/folder3/folder1.".Build().GetPath());
            Assert.AreEqual("folder1/folder2/folder3/", "folder1/folder2/folder3/".Build().GetPath());
            Assert.AreEqual("folder1/folder2/", "folder1/folder2/folder3".Build().GetPath());

            Assert.AreEqual("C:/folder1/folder2/folder3/", "/folder1/folder2/folder3/folder1.jpg".Build().GetPath());
            Assert.AreEqual("C:/folder1/folder2/folder3/", "/folder1/folder2/folder3/folder1.".Build().GetPath());
            Assert.AreEqual("C:/folder1/folder2/folder3/", "/folder1/folder2/folder3/".Build().GetPath());
            Assert.AreEqual("C:/folder1/folder2/", "/folder1/folder2/folder3".Build().GetPath());
        }


        [Test]
        public void Units_GetAsPath() {


            //there may be folders that contain points so that behavior is correct
            //here I force any path to a path ending in / and the beginning determined
            Assert.AreEqual("homefolder/2/3/folder4.txt/", "homefolder/2/3/folder4.txt".Build().GetAsPath());
            Assert.AreEqual("homefolder/2/3/folder4.test/", "homefolder/2/3/folder4.test".Build().GetAsPath());
            Assert.AreEqual("homefolder/2/3/", "homefolder/2/3/".Build().GetAsPath());
            Assert.AreEqual("homefolder/2/3/", "homefolder/2/3".Build().GetAsPath());

            Assert.AreEqual("C:/homefolder/2/3/folder4.txt/", "/homefolder/2/3/folder4.txt".Build().GetAsPath());
            Assert.AreEqual("C:/homefolder/2/3/folder4.test/", "/homefolder/2/3/folder4.test".Build().GetAsPath());
            Assert.AreEqual("C:/homefolder/2/3/", "/homefolder/2/3/".Build().GetAsPath());
            Assert.AreEqual("C:/homefolder/2/3/", "/homefolder/2/3".Build().GetAsPath());

            Assert.AreEqual("myfolder/path/game.pdf/folder4.png/", "myfolder/path/game.pdf/folder4.png".Build().GetAsPath());
            Assert.AreEqual("myfolder/path/game.pdf/folder4.test/", "myfolder/path/game.pdf/folder4.test".Build().GetAsPath());
            Assert.AreEqual("myfolder/path/game.pdf/", "myfolder/path/game.pdf/".Build().GetAsPath());

            Assert.AreEqual("myfolder/path/game.pdf/folder4.png/", "myfolder/path/game.pdf/folder4.png/".Build().GetAsPath());
            Assert.AreEqual("myfolder/path/game.pdf/folder4./", "myfolder/path/game.pdf/folder4./".Build().GetAsPath());
            Assert.AreEqual("myfolder/path/game.pdf/", "myfolder/path/game.pdf/".Build().GetAsPath());
        }

        [Test]
        public void Units_IsPath() {
            Assert.True("1/2/3/".Build().IsPath());
            Assert.False("1/2/3".Build().IsPath());
            Assert.False("".Build().IsPath());
        }

        [Test]
        public void Units_GetPaths() {
            Assert.AreEqual(new string[] { "one", "two" }, "one/two/three".Build().GetPaths());
            Assert.AreEqual(new string[] { "one", "two", "three" }, "one/two/three/".Build().GetPaths());
            Assert.AreEqual(new string[] { "C:", "one", "two", "three" }, "/one/two/three/".Build().GetPaths());
            Assert.AreEqual(new string[] { "C:", "one", "two", "three" }, "/one/two/three/filename".Build().GetPaths());

            Assert.AreEqual(new string[] { }, "".Build().GetPaths());
        }

        [Test]
        public void Units_GetFileName() {
            Assert.AreEqual("filename", "1/12/filename".Build().GetFileName());
            Assert.AreEqual("filename.txt", "test/folder/filename.txt".Build().GetFileName());
            Assert.AreEqual(".txt", "folder/test/.txt".Build().GetFileName());

            Assert.AreEqual("", "one/test/".Build().GetFileName());
            Assert.AreEqual("", "".Build().GetFileName());
        }

        [Test]
        public void Units_GetFileNameWithoutExtension() {
            Assert.AreEqual("filename", "test/2/filename".Build().GetFileNameWithoutExtension());
            Assert.AreEqual("filename", "test/2/filename.pdf".Build().GetFileNameWithoutExtension());
            Assert.AreEqual("", "test/2/.pdf".Build().GetFileNameWithoutExtension());

            Assert.AreEqual("", "1/2/".Build().GetFileNameWithoutExtension());
            Assert.AreEqual("", "".Build().GetFileNameWithoutExtension());
        }

        [Test]
        public void Units_GetFileExtension() {
            Assert.AreEqual("", "1/2/filename".Build().GetFileExtension());
            Assert.AreEqual(".txt", "1/2/filename.txt".Build().GetFileExtension());
            Assert.AreEqual(".txt", "1/2/.txt".Build().GetFileExtension());

            Assert.AreEqual("", "1/2/".Build().GetFileExtension());
            Assert.AreEqual("", "".Build().GetFileExtension());
        }

        [Test]
        public void Units_GetFullFileNameWithoutExtension() {
            Assert.AreEqual("folder/2/filename", "folder/2/filename".Build().GetFullFileNameWithoutExtension());
            Assert.AreEqual("folder/2/filename", "folder/2/filename.txt".Build().GetFullFileNameWithoutExtension());
            Assert.AreEqual("folder/2/", "folder/2/.txt".Build().GetFullFileNameWithoutExtension());

            Assert.AreEqual("folder/2/", "folder/2/".Build().GetFullFileNameWithoutExtension());
            Assert.AreEqual("", "".Build().GetFullFileNameWithoutExtension());
        }

        [Test]
        public void Units_SetName() {
            Assert.AreEqual("1/2/newName.txt", "1/2/hola".Build().SetName("newName.txt"));
            Assert.AreEqual("1/2/newName", "1/2/hola.pdf".Build().SetName("newName"));
            Assert.AreEqual("1/2/newName.test", "1/2/.pdf".Build().SetName("newName.test"));

            Assert.AreEqual("1/2/test.txt", "1/2/".Build().SetName("test.txt"));
            Assert.AreEqual(".newName", "".Build().SetName(".newName"));
        }

        [Test]
        public void Units_MoveFolderBack() {
            Assert.AreEqual("1/", "1/2/filename".Build().MoveFolderBack());
            Assert.AreEqual("1/", "1/2/filename.pdf".Build().MoveFolderBack());
            Assert.AreEqual("1/", "1/2/.pdf".Build().MoveFolderBack());
            Assert.AreEqual("1/2/", "1/2/lel/".Build().MoveFolderBack());
            Assert.AreEqual("C:/1/2/", "C:/1/2/lel/".Build().MoveFolderBack());

            Assert.AreEqual("1/", "1/2/".Build().MoveFolderBack());
            Assert.AreEqual("../", "".Build().MoveFolderBack());
            Assert.AreEqual("../../", "../".Build().MoveFolderBack());
            Assert.AreEqual("C:/", "C:/filename".Build().MoveFolderBack());
            Assert.AreEqual("", "C:/".Build().MoveFolderBack());
        }

        [Test]
        public void Units_IsAbsolutePath() {
            Assert.False("1/2/filename".Build().IsAbsolutePath());
            Assert.False("1/2/filename.txt".Build().IsAbsolutePath());
            Assert.False("1/2/.txt".Build().IsAbsolutePath());
            Assert.False("1/2/3/".Build().IsAbsolutePath());

            Assert.True("C:/1/2/3/filename.pdf".Build().IsAbsolutePath());
            Assert.True("C:/1/2/3/filename.txt".Build().IsAbsolutePath());
            Assert.True("C:/1/2/3/.txt".Build().IsAbsolutePath());
            Assert.True("C:/1/2/3/.jpg".Build().IsAbsolutePath());
            Assert.True("C:/1/2/3/".Build().IsAbsolutePath());
            Assert.True("C:/1/2/3".Build().IsAbsolutePath());

            Assert.True("/1/2/3/filename.pdf".Build().IsAbsolutePath());
            Assert.True("/1/2/3/filename.txt".Build().IsAbsolutePath());
            Assert.True("/1/2/3/.txt".Build().IsAbsolutePath());
            Assert.True("/1/2/3/.jpg".Build().IsAbsolutePath());
            Assert.True("/1/2/3/".Build().IsAbsolutePath());
            Assert.True("/1/2/3".Build().IsAbsolutePath());

            Assert.False("1/2/".Build().IsAbsolutePath());
            Assert.False("".Build().IsAbsolutePath());
            Assert.False("../".Build().IsAbsolutePath());
            Assert.True("C:/filename.txt".Build().IsAbsolutePath());
            Assert.True("C:/".Build().IsAbsolutePath());
        }

    }
}