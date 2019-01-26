//using System.IO;
//using System.Text;
//using Waes.Diff.Core.UnitTests.AutoData;
//using Xunit;

//namespace Waes.Diff.Core.UnitTests
//{
//    public class UnitTest1
//    {
//        [Theory, AutoNSubstituteData]
//        public void CASE1_FilesAreTheSameSizeAndEquals(BinaryCompare sut)
//        { 
//            string path = @"C:\tmp\file1"; 
//            using (FileStream fs = File.Create(path))
//            {                
//                string dataasstring = "data"; 
//                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
//                fs.Write(info, 0, info.Length);
//            }

//            string path2 = @"C:\tmp\file2";
//            using (FileStream fs = File.Create(path2))
//            {
//                string dataasstring = "data";
//                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
//                fs.Write(info, 0, info.Length);
//            }

//            sut.Diff(File.Open(@"C:\tmp\file1", FileMode.Open), File.Open(@"C:\tmp\file2", FileMode.Open));
//        }

//        [Theory, AutoNSubstituteData]
//        public void CASE2_FilesAreTheSameSizeAndDifferents(BinaryCompare sut)
//        {
//            string path = @"C:\tmp\file3";
//            using (FileStream fs = File.Create(path))
//            {
//                string dataasstring = "data";
//                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
//                fs.Write(info, 0, info.Length);
//            }

//            string path2 = @"C:\tmp\file4";
//            using (FileStream fs = File.Create(path2))
//            {
//                string dataasstring = "atda";
//                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
//                fs.Write(info, 0, info.Length);
//            }

//            sut.Diff(File.Open(@"C:\tmp\file3", FileMode.Open), File.Open(@"C:\tmp\file4", FileMode.Open));
//        }

//        [Theory, AutoNSubstituteData]
//        public void CASE3_FilesAreNotTheSameSizeAndDifferents(BinaryCompare sut)
//        {
//            string path = @"C:\tmp\file5";
//            using (FileStream fs = File.Create(path))
//            {
//                string dataasstring = "data";
//                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
//                fs.Write(info, 0, info.Length);
//            }

//            string path2 = @"C:\tmp\file6";
//            using (FileStream fs = File.Create(path2))
//            {
//                string dataasstring = "dataa";
//                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
//                fs.Write(info, 0, info.Length);
//            }

//            sut.Diff(File.Open(@"C:\tmp\file5", FileMode.Open), File.Open(@"C:\tmp\file6", FileMode.Open));
//        }
//    }
//}
