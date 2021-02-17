using Har.Application.Services;
using Har.Infrastructure.Data.Kontent.Repositories;
using Har.Infrastructure.Data.Kontent.Resolvers;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Har.Infrastructure
{
    public static class StartupExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProjectRepository, KontentProjectRepository>();
            
            services.AddHttpClient<IDeliveryHttpClient, DeliveryHttpClient>();
            services.AddDeliveryInlineContentItemsResolver<ImageSlider, ImageSliderResolver>();
            services.AddSingleton<ITypeProvider, CustomTypeProvider>();
            
            services.AddDeliveryClient(builder => builder
                .WithProjectId(configuration.GetValue<string>("DeliveryOptions:ProjectId"))
                .UseProductionApi()
                .Build());
        }
    }
}