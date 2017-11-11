using System.Collections.Generic;
using System.Linq;

namespace OneOf.TemplatingModels
{
    public class MatchMethodModel
    {
        private IEnumerable<int> ArgNumbers => Enumerable.Range(1, Parent.Arity).Except(new[] { ParameterNumber });
        public int ParameterNumber { get; }
        private OneOfModel Parent { get; }

        public MatchMethodModel(int parameterNumber, OneOfModel parent)
        {
            ParameterNumber = parameterNumber;
            Parent = parent;

        }
        private string GenericArgs => Helpers.GenerateGenericArgs(ArgNumbers);

        public string LesserArityMatcherResultTypeName
            => $"IOneOfMatcher{Helpers.CreateInterfaceDisambiguator(ArgNumbers)}<{GenericArgs}, TResult>";
    }
}