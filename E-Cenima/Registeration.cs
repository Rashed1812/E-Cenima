using DAL.Data.Repositories.Intrfaces;

namespace E_Cenima
{
    public static class Registeration
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DataSeedingObject = scope.ServiceProvider
                .GetRequiredService<IDataSeeding>();
            await DataSeedingObject.DataSeedAsync();
            await DataSeedingObject.IdentityDataSeedAsync();

            return app;
        }
    }
}
