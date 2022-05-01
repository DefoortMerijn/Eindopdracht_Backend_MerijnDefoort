namespace StoreTests.Helper;

public class ApiHelper
{
    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IArticleRepository));
                services.Remove(descriptor);
                var fakeArticleRepository = new ServiceDescriptor(typeof(IArticleRepository), typeof(FakeArticleRepository), ServiceLifetime.Transient);
                services.Add(fakeArticleRepository);

                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ILoginRepository));
                services.Remove(descriptor);
                var fakeLoginRepository = new ServiceDescriptor(typeof(ILoginRepository), typeof(FakeLoginRepository), ServiceLifetime.Transient);
                services.Add(fakeLoginRepository);


            });

        });

        return application;

    }

}