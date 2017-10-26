using System;
using System.Collections.Generic;
using System.Linq;

namespace OneOf.TemplatingModels
{
    public static class Helpers
    {
        public static string GenerateGenericArgs(IEnumerable<int> argNumbers)
        {
            return String.Join(", ", argNumbers.Select(n => "T" + n));
        }

        public static string CreateInterfaceDisambiguator(IEnumerable<int> ints) { return String.Join("_", ints.Select(i => i.ToString())); }


        public static IEnumerable<IEnumerable<int>> CombinationsOfLengthN(IEnumerable<int> elementsToCombine, int n)
        {
            var elementsToCombineList = elementsToCombine.ToList();
            return n == 0 ? new[] { new int[0] } :
              elementsToCombineList.SelectMany((e, i) =>
                CombinationsOfLengthN(elementsToCombineList.Skip(i + 1), n - 1)
                .Select(c => (new[] { e })
                .Concat(c)));
        }

        public static IEnumerable<IEnumerable<int>> CombinationsOfLength1ThroughN(IEnumerable<int> elementsToCombine, int n)
        {
            var accumulatedResult = new List<List<IEnumerable<int>>>();
            var listOfElementsToCombine = elementsToCombine.ToList();

            for (var i = 1; i <= n + 1; i++)
            {
                accumulatedResult.Add(CombinationsOfLengthN(listOfElementsToCombine, i).ToList());
            }

            return accumulatedResult.SelectMany(i => i);
        }

        public static string MakeDummyClassParamList(int arity)
        {
            var dummyNums = Enumerable.Range(arity, Constants.MaxAritySupported - arity).ToArray();
            var dummyParams = string.Join(",", dummyNums.Select(i => "DummyClass")) + (dummyNums.Any() ? ", ":"");
            return dummyParams;
        }
    }
}