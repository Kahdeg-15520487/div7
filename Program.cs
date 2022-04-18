using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace divisibleby7
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //recur("0", 8).Distinct().ToList().ForEach(r => Console.WriteLine("{0}-{1}>{2}", r.pr, r.a, r.cr));
            //for (int i = 0; i < 128; i++)
            //{
            //    var b = Convert.ToString(i, 2).PadLeft(8, '0');
            //    //Console.WriteLine("{0} {1}", b, i % 7 == 0 ? "accept" : "reject");
            //    if (i % 7 == 0)
            //    {
            //        Console.WriteLine(b);
            //    }
            //}
            //Console.WriteLine("separator");
            //for (int i = 0; i < 128; i++)
            //{
            //    var b = Convert.ToString(i, 2).PadLeft(8, '0');
            //    //Console.WriteLine("{0} {1}", b, i % 7 == 0 ? "accept" : "reject");
            //    if (i % 7 != 0)
            //    {
            //        Console.WriteLine(b);
            //    }
            //}

            //var states = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
            //var actions = new List<int>() { 0, 1 };
            //var transitions = new List<Transition<int, int>>
            //{
            //    new Transition<int, int>(0,0,0),
            //    new Transition<int, int>(0,1,1),
            //    new Transition<int, int>(1,0,2),
            //    new Transition<int, int>(1,1,3),
            //    new Transition<int, int>(2,0,4),
            //    new Transition<int, int>(2,1,5),
            //    new Transition<int, int>(3,0,6),
            //    new Transition<int, int>(3,1,0),
            //    new Transition<int, int>(4,0,1),
            //    new Transition<int, int>(4,1,2),
            //    new Transition<int, int>(5,0,3),
            //    new Transition<int, int>(5,1,4),
            //    new Transition<int, int>(6,0,5),
            //    new Transition<int, int>(6,1,6),
            //};
            //var start = 0;
            //var finalStates = new List<int> { 0 };
            //var dfa = new DFA<int, int>(states, actions, transitions, start, finalStates);

            //for (int i = 0; i < 128; i++)
            //{
            //    var b = Convert.ToString(i, 2).Select(c => c - 48);
            //    Console.WriteLine("7|{0}:{1}", i, dfa.Accepts(b, out var s0));
            //}

            //var substitutions = new Dictionary<string, string>()
            //{
            //    {"q0", "E+q3.1+q0.0" },
            //    {"q1", "q0.1+q4.0" },
            //    {"q2", "q1.0+q4.1" },
            //    {"q3", "q1.1+q5.0" },
            //    {"q4", "q2.0+q5.1" },
            //    {"q5", "q2.1+q6.0" },
            //    {"q6", "q3.0+q6.1" },
            //};

            //var temp = substitutions["q0"];
            //substitutions.Remove("q0");

            //do
            //{
            //    temp = string.Join("+", temp.Split("+").Select(t =>
            //     {
            //         if (t == "E")
            //         {
            //             return t;
            //         }
            //         var s = t.Substring(0, 2);
            //         if (s == "q0")
            //         {
            //             return t;
            //         }
            //         var r = t.Substring(2);
            //         var sub = substitutions[s].Split("+");
            //         return $"{sub[0]}{r}+{sub[1]}{r}";
            //     }));
            //    Console.WriteLine(temp);
            //    if (!temp.Contains("q1")
            //     && !temp.Contains("q2")
            //     && !temp.Contains("q3")
            //     && !temp.Contains("q4")
            //     && !temp.Contains("q5")
            //     && !temp.Contains("q6"))
            //    {
            //        break;
            //    }
            //} while (true);
            //Console.WriteLine(temp);
        }

        static IEnumerable<(int pr, char a, int cr)> recur(string previous, int limit = 8)
        {
            if (previous.Length <= limit)
            {
                foreach (var r in recur(previous + '0', limit)) yield return r;
                foreach (var r in recur(previous + '1', limit)) yield return r;
            }
            else
            {
                int prev = Convert.ToInt32(previous.Substring(0, previous.Length - 1), 2);
                int prevRemainder = prev % 7;
                char added = previous[previous.Length - 1];
                int curr = Convert.ToInt32(previous, 2);
                int currRemainder = curr % 7;
                yield return (prevRemainder, added, currRemainder);
            }
        }
    }
}
