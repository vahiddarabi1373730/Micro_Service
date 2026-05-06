using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class DiscountRepositories(IConfiguration configuration) : IDiscountRepositories
{
    private readonly string? _connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");

    public async Task<Coupon> GetDiscountById(string productId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "select * from coupon  where productId=@productId";
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { productId });
        return coupon ?? new Coupon();
    }

    public async Task<Coupon> GetDiscountByName(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "select * from coupon  where productName=@productName";
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { productName });
        return coupon ?? new Coupon();
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql =
            "insert into coupon (productId,productName,description,amount) values (@productId,@productName,@description,@amount)";
        var parameters = new { coupon.ProductId, coupon.ProductName, coupon.Description, coupon.Amount, };
        return await connection.ExecuteAsync(sql, parameters) > 0;
    }

    public async Task<bool> UpdateDiscount(int id, Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql =
            "update coupon set  productId=@productId,productName=@productName,description=@description,amount=@amount";
        var parameters = new { coupon.ProductId, coupon.ProductName, coupon.Description, coupon.Amount, };
        return await connection.ExecuteAsync(sql, parameters) > 0;
    }

    public async Task<bool> DeleteDiscount(string id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "delete from coupon where productId=@productId";
        return await connection.ExecuteAsync(sql, new { productId = id }) > 0;
    }

    public async Task<bool> DeleteDiscountByName(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "delete from coupon where productName=@productName";
        return await connection.ExecuteAsync(sql, new { productName = productName }) > 0;
    }
}