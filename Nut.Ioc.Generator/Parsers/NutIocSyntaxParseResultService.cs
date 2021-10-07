namespace Nut.Ioc.Generator.Parsers
{
    public class NutIocSyntaxParseResultService
    {
        public NutIocSyntaxParseResultType Base { get; set; }

        public NutIocSyntaxParseResultType Class { get; set; }

        public NutIocSyntaxParseResultService Clone()
        {
            return new NutIocSyntaxParseResultService
            {
                Base = Base?.Clone(),
                Class = Class?.Clone()
            };
        }

        public override string ToString()
        {
            return Class + (Base == null ? null : " : " + Base);
        }
    }
}