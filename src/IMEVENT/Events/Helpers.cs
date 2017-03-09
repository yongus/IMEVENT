using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Events
{
    public struct Section
    {
        public int Id { get; set; }
        public int TableNbr { get; set; }
        public int IdRef { get; set; }
        public int PlaceNbr { get; set; }
    }

    public static class Extensions
    {
        //shuffle according to Fisher-Yates algorithm

        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while(n > 1)//iterate backwards through the list
            {
                n--;
                int randomIndex = random.Next(n + 1);//grab a number through any spot, including the one we are currently on
                //Swap elements
                T tmp = list[randomIndex]; //value to be swapped               
                list[randomIndex] = list[n];
                list[n] = tmp;                
            }
        }      
        
        public static void AddIfNotExists<T>(this List<T> list, T elem)
        {
            if (!list.Contains(elem))
            {
                list.Add(elem);
            }
        } 
        
        public static bool IsNullOrEmpty<T, H>(this Dictionary<T, H> elem)
        {
            if(elem == null || !elem.Any())
            {
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<T>(this List<T> elem)
        {
            if (elem == null || !elem.Any())
            {
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<T>(this Stack<T> elem)
        {
            if (elem == null || !elem.Any())
            {
                return true;
            }

            return false;
        }

        public static int Count<T, H>(this Dictionary<T, Stack<H>> elem)
        {
            if (elem == null || !elem.Any())
            {
                return 0;
            }

            int ret = 0;
            foreach (KeyValuePair<T, Stack<H>> e in elem)
            {
                ret += e.Value.Count;
            }

            return ret;
        }

        public static int Count<T,P,H>(this Dictionary<T, Dictionary<P,Stack<H>>> elem)
        {
            if (elem == null || !elem.Any())
            {
                return 0;
            }

            int ret = 0;
            foreach (KeyValuePair<T, Dictionary<P, Stack<H>>> e in elem)
            {               
               ret += e.Value.Count();               
            }

            return ret;
        }
    }
}
