using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PoormanD
{
    public class CustomSqliteOptionsExtension : SqliteOptionsExtension
    {
        public CustomSqliteOptionsExtension()
        {
        }

        public CustomSqliteOptionsExtension(SqliteOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public override void ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            var mct = services.FirstOrDefault(_ => _.ServiceType == typeof(SqliteCompositeMethodCallTranslator));
            services.Remove(mct);
            services.AddScoped<SqliteCompositeMethodCallTranslator>(
                sp => new CompositeMethodCallTranslator(sp.GetService<ILogger<SqliteCompositeMethodCallTranslator>>()));
        }
    }
}