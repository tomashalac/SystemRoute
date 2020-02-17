using NUnit.Framework;
using System;
using SystemRoute;

namespace SystemRouteTests {
    class ExamplesTests {

        [Test]
        public void Integration_Example1() {
            var absolute = "/home/test/".Build();

            var folderRelative = "folder/file.txt".Build();
            //You can handle all routes as if it were Linux and the system handles them according to the platform.
            var folderAbsolute = "/home/test/folder/file.txt".Build();
            //folderAbsolute on windows="C:/home/test/folder/file.txt"
            //folderAbsolute on linux="/home/test/folder/file.txt"

            //Relative to absolute
            Assert.AreEqual(folderAbsolute.ForceToAbsolutePath(absolute), folderRelative.ForceToAbsolutePath(absolute));

            //GetFileNameWithoutExtension
            Assert.AreEqual(folderAbsolute.GetFileNameWithoutExtension(), folderRelative.GetFileNameWithoutExtension());

            //MoveFolderBack
            Assert.AreEqual(folderAbsolute.MoveFolderBack(), absolute);
        }

    }
}
