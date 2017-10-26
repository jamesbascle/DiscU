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

        public string OneOfMatcherTypeNameWithDummies => $"OneOfMatcher<{GenericArgs}, {Helpers.MakeDummyClassParamList(Arity)}TResult>";

        public IEnumerable<SwitchMethodModel> SwitchMethods => Enumerable.Range(1, Arity).Select(i => new SwitchMethodModel(i, this));

        public IEnumerable<MatchMethodModel> MatchMethods => Enumerable.Range(1, Arity).Select(i => new MatchMethodModel(i, this));
    }
}
