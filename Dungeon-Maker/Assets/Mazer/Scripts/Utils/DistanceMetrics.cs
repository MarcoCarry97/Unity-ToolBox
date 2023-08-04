using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazer.Utils.Distance
{
    public static class DistanceMetrics
    {
        public static int CountDistance(List<string> current, List<string> previous)
        {
            int distance = 0;
            foreach (string currentFact in current)
            {
                if(previous.Contains(currentFact))
                {
                    distance++;
                }
            }
            return distance;
        }


        /*public static int CenterDistance(List<string> current, List<string> previous)
        {

        }*/


    }
}
