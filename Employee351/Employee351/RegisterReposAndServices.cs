
namespace Employee351
{
    public static class RegisterReposAndServices
    {  
        public static void RegisterServices(this IServiceCollection services)
        {
            Configure(services, Employee351.Repository.DataRegister.GetTypes());
            Configure(services, Employee351.Services.ServiceRegister.GetTypes());
        }
        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (KeyValuePair<Type, Type> type in types)
                services.AddScoped(type.Key, type.Value);
        }
    }
}
