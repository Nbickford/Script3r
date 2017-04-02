using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Script3r_Library
{
    public class file_move
    {
        public static void move_and_org(List<string> files_to_move, Dictionary<string, List<string>> source_file_dict, string destination)
        {
            foreach (string path in files_to_move)
            {
                if (source_file_dict[path][0] == "match_fail")
                {
                    System.IO.Directory.CreateDirectory(destination + "\\match_fail");
                    File.Move(path, destination + "\\match_fail\\" + Path.GetFileName(path));
                }
                else
                {
                    string scene = source_file_dict[path][0];
                    string take = source_file_dict[path][1];
                    string extention = Path.GetExtension(path);
                    System.IO.Directory.CreateDirectory(destination + "\\" + "Scene_" + scene);
                    File.Move(path, destination + "\\" + "Scene_" + scene + "\\" + scene + "_" + take + extention);
                }
            }
        }
    }
}
