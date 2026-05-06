using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Infrastructure.Extension;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using var scoped = host.Services.CreateScope();
        var serviceProvider = scoped.ServiceProvider;
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        try
        {
            logger.LogInformation("Start Migration in on {DbContextName}", typeof(TContext).Name);
            ApplyMigration<TContext>(configuration, logger);
            logger.LogInformation("Database migration completed");
        }
        catch (Exception e)
        {
            logger.LogError(e, "an error occurred while migrating the database {dbContextName}", typeof(TContext).Name);
            throw;
        }

        return host;
    }

    private static void ApplyMigration<TContext>(IConfiguration configuration, ILogger logger)
    {
        var retry = 5;
        while (retry > 0)
        {
            try
            {
                using var connection =
                    new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();
                using var cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "Drop Table If Exists Coupon";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"CREATE Table Coupon(Id Serial PRIMARY KEY ,ProductId Text,ProductName varchar(500) not null ,Description Text,Amount int)";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"Insert into Coupon (ProductName,Description,Amount,ProductId) values ('Essence Mascara Lash Princess','The Essence Mascara Lash Princess is a popular mascara known for its volumizing and lengthening effects. Achieve dramatic lashes with this long-lasting and cruelty-free formula.',1500,'66b6a1010000000000000001')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"insert into Coupon(ProductName,Description,Amount,ProductId) values ('Eyeshadow Palette with Mirror','The Eyeshadow Palette with Mirror offers a versatile range of eyeshadow shades for creating stunning eye looks. With a built-in mirror, its convenient for on-the-go makeup application.',2000,'66b6a1010000000000000002')";
                cmd.ExecuteNonQuery();
                break;
            }
            catch (Exception e)
            {
                retry--;
                if (retry == 0)
                {
                    throw;
                }

                logger.LogError(e, "Retrying database migration,attemps left : {retry}", retry);
                Thread.Sleep(2000);
            }
        }
    }
}