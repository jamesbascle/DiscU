using System.Collections.Generic;
using System.Linq;

namespace OneOf.TemplatingModels
{
    public class OneOfModel
    {
        public const string TypeName = "OneOf";
        public string FullTypeName { get; }
        public OneOfModel(int arity)
        {
            Arity = arity;
            FullTypeName = $"OneOf<{GenericArgs}>";
        }
        public int Arity { get; }

        public string GenericArgs => Helpers.GenerateGenericArgs(Enumerable.Range(1, Arity));

        public IEnumerable<string> GenericParameters => Enumerable.Range(1, Arity).Select(i => "T" + i);

        public string IOneOfMatcherTypeName => $"IOneOfMatcher{Helpers.CreateInterfaceDisambiguator(Enumerable.Range(1, Arity))}<{GenericArgs}, TResult>";
        public string OneOfMatcherTypeName => $"OneOfMatcher<{GenericArgs}, TResult>";

        public IEnumerable<SwitchMethodModel> SwitchMethods => Enumerable.Range(1, Arity).Select(i => new SwitchMethodModel(i, this));

        public IEnumerable<MatchMethodModel> MatchMethods => Enumerable.Range(1, Arity).Select(i => new MatchMethodModel(i, this));

        public class SwitchMethodModel
        {
            private OneOfModel Parent { get;  }
            public int ParameterNumber { get; }

            public string GenericArg => "T" + ParameterNumber;

            internal SwitchMethodModel(int parameterNumber, OneOfModel parent)
            {
                ParameterNumber = parameterNumber;
                Parent = parent;
            }

            public string SwitcherTypeName => $"{OneOfSwitcherModel.TypeName}<{GenericArgs}>";

            public string LesserAritySwitcherResultTypeName => $"{OneOfSwitcherModel.TypeName}<{LesserArityGenericArgs}>";
            private string LesserArityGenericArgs => Helpers.GenerateGenericArgs(LesserArityArgNumbers);

            private string GenericArgs => Helpers.GenerateGenericArgs(ArgNumbers);

            public IEnumerable<int> ArgNumbers => Enumerable.Range(1, Parent.Arity);
            private IEnumerable<int> LesserArityArgNumbers => ArgNumbers.Except(new[] {ParameterNumber});
        }

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
}
