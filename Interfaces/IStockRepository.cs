using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Dtos.Stock;
using dotnet8_api.Helpers;
using dotnet8_api.Models;

namespace dotnet8_api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);
  
    }
}