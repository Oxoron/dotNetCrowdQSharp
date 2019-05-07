using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace Quantum.dotNetCrowdQSharp
{
    class Driver
    {
        static void Main(string[] args)
        {
            // (Un)comment sample methods to test operations over a quantum simulator.

            //TestRandomQubit();
            //TestRandomGuid();
            TestBellState();
            //TestThreeOfFourState();
            //TestOneBitOfFour();

            Console.ReadLine();            
        }


        static void TestRandomQubit()
        {  
            var zeroCounts = 0;
            var totalCounts = 1000;

            using (var qsim = new QuantumSimulator())
            {
                for(int i = 0; i<totalCounts; i++)
                {
                    Result random = RandomQubit.Run(qsim).Result;
                    if (random == Result.Zero) { zeroCounts++; }
                }                
            }

            Console.WriteLine($"Zero: {zeroCounts}.\r\n One: {totalCounts-zeroCounts}");
        }


        static void TestRandomGuid()
        {                 
            var totalCounts = 10;

            using (var qsim = new QuantumSimulator())
            {
                for (int i = 0; i < totalCounts; i++)
                {
                    QArray<Result> guid = RandomGuid.Run(qsim, 25).Result;
                    Console.WriteLine
                    (
                        guid.Aggregate
                        (
                            seed: String.Empty, 
                            func: (sum, current) => { return sum + (current == Result.Zero ? "0" : "1"); }
                        )
                    );
                }
            }            
        }


        static void TestBellState()
        {
            var totalCounts = 1000;
            Dictionary<string, int> resutls = new Dictionary<string, int>()
            {
                { "00", 0},
                { "01", 0},
                { "10", 0},
                { "11", 0}
            };

            using (var qsim = new QuantumSimulator())
            {
                for (int i = 0; i < totalCounts; i++)
                {
                    (Result, Result) bell = BellState.Run(qsim).Result;
                    var key = $"{bell.Item1}{bell.Item2}"
                        .Replace("Zero","0")
                        .Replace("One", "1");

                    resutls[key] = 1 + resutls[key];
                }

                foreach (var res in resutls)
                {
                    Console.WriteLine($"{res.Key}: {res.Value}");
                }
            }            
        }

         
        static void TestThreeOfFourState()
        {
            var totalCounts = 1000;
            Dictionary<string, int> resutls = new Dictionary<string, int>()
            {
                { "00", 0},
                { "01", 0},
                { "10", 0},
                { "11", 0}
            };

            using (var qsim = new QuantumSimulator())
            {
                for (int i = 0; i < totalCounts; i++)
                {
                    var bell = ThreeOfFourState.Run(qsim).Result;
                    var key = $"{bell.Item1}{bell.Item2}"
                        .Replace("Zero", "0")
                        .Replace("One", "1");

                    resutls[key] = 1 + resutls[key];
                }

                foreach (var res in resutls)
                {
                    Console.WriteLine($"{res.Key}: {res.Value}");
                }
            }
        }

        
        static void TestOneBitOfFour()
        {
            var totalCounts = 1000;
            Dictionary<string, int> resutls = new Dictionary<string, int>(){};

            using (var qsim = new QuantumSimulator())
            {
                for (int i = 0; i < totalCounts; i++)
                {
                    var fourQubits = OneBitOfFour.Run(qsim).Result;
                    var key = $"{fourQubits.Item1}{fourQubits.Item2}{fourQubits.Item3}{fourQubits.Item4}"
                        .Replace("Zero", "0")
                        .Replace("One", "1");

                    if (resutls.ContainsKey(key))
                    {
                        resutls[key] = 1 + resutls[key];
                    }
                    else
                    {
                        resutls.Add(key, 1);
                    }
                }

                foreach (var res in resutls.OrderBy(res => res.Key))
                {
                    Console.WriteLine($"{res.Key}: {res.Value}");
                }
            }
        }     
    }
}