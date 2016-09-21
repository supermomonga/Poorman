using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.Extensions.Logging;

namespace PoormanD
{
    public class CompositeMethodCallTranslator : SqliteCompositeMethodCallTranslator
    {
        public CompositeMethodCallTranslator(ILogger<SqliteCompositeMethodCallTranslator> logger) : base(logger)
        {
            AddTranslators(new[] {new JsonExtractMethodCallTranslator()});
        }
    }
}