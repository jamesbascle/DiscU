using System.Collections.Generic;
using System.Linq;

namespace OneOf.TemplatingModels
{
    public class SwitchMethodModel
    {
        private OneOfModel Parent { get; }
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
        private IEnumerable<int> LesserArityArgNumbers => ArgNumbers.Except(new[] { ParameterNumber });
    }
}