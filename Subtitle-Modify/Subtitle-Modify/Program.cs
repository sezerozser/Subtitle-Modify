using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtitle_Modify
{
    class Program
    {
        private static List<FileInfo> fileList;

        static void Main(string[] args)
        {
            if (args.Count() == 1)
            {
                fileList = new List<FileInfo>();

                DirWalker(args[0]);

                foreach (FileInfo f in fileList)
                {
                    ModifyFile(f);
                }
            }
            else
            {
                Console.WriteLine("This program removes any line start with <b> or <i> in a .srt file");
                Console.WriteLine("To use the program supply folder path.");
                Console.WriteLine("example usage: Subtitle-Modify c:\\Users\\Sezer\\Desktop\\CourseName");
            }
        }

        private static void DirWalker(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (string f in files)
            {
                FileInfo fi = new FileInfo(f);

                if (fi.Extension == ".srt")
                    fileList.Add(fi);
            }

            foreach (string d in dirs)
                DirWalker(d);
        }

        private static void ModifyFile(FileInfo source_f)
        {
            FileInfo target_f = new FileInfo(source_f.Directory.FullName + "\\temp.srt");
            string line;

            try
            {
                StreamReader sr = new StreamReader(source_f.FullName);
                StreamWriter sw = new StreamWriter(target_f.FullName);

                line = sr.ReadLine();

                while (line != null)
                {
                    if (line.Length > 3 && (line.Substring(0, 3) == "<b>" || line.Substring(0, 3) == "<i>"))
                    {
                        Console.WriteLine(line);
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                    line = sr.ReadLine();
                }


                sr.Close();
                sw.Close();

                source_f.Delete();
                target_f.MoveTo(source_f.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
