using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ControlForestal
{
    public class DroneController
    {
        public string processFile(string file)
        {
            try
            {
                string result = "";
                string[] lines = File.ReadAllLines(file);
                //the first line is the flying area               
                int[] area = getAreaFromString(lines[0].Replace(" ", ""));
                if (area != null)
                {
                    for (var i = 1; i < lines.Length; i += 1)
                    {
                        //
                        if (i%2 == 0)
                        {
                            Drone oDrone = new Drone(area, lines[i - 1], lines[i]);
                            result = result + oDrone.startRoute() + Environment.NewLine;
                        }
                    }
                }
                return result;
            } 
            catch (IOException e)
            {
                return "The file could not be read:";
            }
        }

        private int[] getAreaFromString(string sArea)
        {
            if (sArea.Length == 2)
            {
                int x;
                int y;                
                if (int.TryParse(sArea[0].ToString(), out x) && int.TryParse(sArea[1].ToString(), out y))
                {
                    return new int[] { x, y };
                } 
                else
                {
                    return null;
                }
            } 
            else
            {
                return null;
            }
        }
    }
}
