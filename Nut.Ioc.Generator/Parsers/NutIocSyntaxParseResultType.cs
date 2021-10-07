using Nut.Ioc.Generator.Extensions;

namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxParseResultType
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        private string[] genericParameters;
        public string[] GenericParameters
        {
            get { return genericParameters ?? (genericParameters = new string[0]); }
            set { genericParameters = value; }
        }

        private NutIocSyntaxParseResultType[] constructorParameters;
        public NutIocSyntaxParseResultType[] ConstructorParameters
        {
            get { return constructorParameters ?? (constructorParameters = new NutIocSyntaxParseResultType[0]); }
            set { constructorParameters = value; }
        }

        public bool IsGeneric => GenericParameters.Length > 0;

        public bool IsAbstract { get; set; }

        public NutIocSyntaxParseResultType Clone()
        {
            return new NutIocSyntaxParseResultType
            {
                Name = Name,
                Namespace = Namespace,
                IsAbstract = IsAbstract,
                GenericParameters = GenericParameters.SelectArray(x => x),
                ConstructorParameters = ConstructorParameters.SelectArray(x => x.Clone())
            };
        }

        public NutIocSyntaxParseResultType EncloseGenericParameters(string[] parameters)
        {
            Name = IsGeneric ? Name.SwitchGenericParameters(parameters) : Name;
            GenericParameters = IsGeneric ? parameters : GenericParameters;
            ConstructorParameters = ConstructorParameters.SelectArray(x => x.Clone().EncloseGenericParameters(parameters));

            return this;
        }

        public override string ToString()
        {
            return $"{(IsAbstract ? "abstract " : null)}{Name}";
        }
    }
}