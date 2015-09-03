using Component.Tools.Configurations;

namespace Exam.Api.Configuration.API_CONFIG
{
    public interface IApiConfiguration : IConfiguration<ApiConfiguration>
    {
        bool HasApi(string apiName);
    }
}
