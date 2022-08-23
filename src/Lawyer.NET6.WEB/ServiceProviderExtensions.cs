using Lawyer.NET6.BLL.Service;

namespace Lawyer.NET6.WEB
{
    public static class ServiceProviderExtensions
    {
        public static void AddEmailService(this IServiceCollection services)
        {
            services.AddTransient<EmailService>();
        }
        public static void AddLawyerService(this IServiceCollection services)
        {
            services.AddTransient<LawyerService>();
        }
    }
}
