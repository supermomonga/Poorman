using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using PoormanD.Models;

namespace PoormanD
{
    public class JsonExtractMethodCallTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public JsonExtractMethodCallTranslator()
            : base(typeof(Event), nameof(Event.JsonExtract), "json_extract")
        {
        }
    }
}