using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskUser.Models;
using TaskUser.Models.Production;
using TaskUser.ViewsModels.Stock;

namespace TaskUser.Service
{

    public interface IStockService
    {
        Task<List<StockViewModels>> GetStockListAsync();

        Task<bool> AddStockAsync(StockViewModels addStock);

        IEnumerable<Stock> GetStock();

        Task<StockViewModels> GetIdStockAsync(int productId, int storeId);

        Task<bool> EditStockAsync(StockViewModels editStock);

        Task<bool> Delete(int productId, int storeId);

    }

    public class StockService : IStockService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StockService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// get show list stock
        /// </summary>
        /// <returns>listStock</returns>
        public async Task<List<StockViewModels>> GetStockListAsync()
        {
            var list = await _context.Stocks.Include(s => s.Store).Include(p => p.Product).ToListAsync();
            var listStock = _mapper.Map<List<StockViewModels>>(list);
            return listStock;
        }

        public IEnumerable<Stock> GetStock()
        {
            return _context.Stocks;
        }

        /// <summary>
        /// get create stock
        /// </summary>
        /// <param name="addStock">StockViewModels</param>
        /// <returns>true || false</returns>
        public async Task<bool> AddStockAsync(StockViewModels addStock)
        {
            try
            {
                var ckeck = await _context.Stocks.FindAsync(addStock.ProductId, addStock.StoreId);
                if (ckeck != null)
                {
                    ckeck.Quantity += addStock.Quantity;
                    _context.Stocks.Update(ckeck);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var stock = new Stock()
                    {
                        ProductId = addStock.ProductId,
                        StoreId = addStock.StoreId,
                        Quantity = addStock.Quantity
                    };
                    _context.Stocks.Add(stock);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }



        }

        /// <summary>
        /// get id edit stock
        /// </summary>
        /// <param name="productId">StockViewModels</param>
        /// <param name="storeId">StockViewModels</param>
        /// <returns>stockDtos</returns>
        public async Task<StockViewModels> GetIdStockAsync(int productId, int storeId)
        {
            var findStock = await _context.Stocks.FindAsync(productId, storeId);
            var stockDtos = _mapper.Map<StockViewModels>(findStock);
            return stockDtos;
        }

        /// <summary>
        /// post edit stock
        /// </summary>
        /// <param name="editStock">StockViewModels</param>
        /// <returns>true || false</returns>
        public async Task<bool> EditStockAsync(StockViewModels editStock)
        {
            try
            {
                var checkEdit = await _context.Stocks.FindAsync(editStock.ProductId,editStock.StoreId);
                checkEdit.Quantity = editStock.Quantity;
                _context.Stocks.Update(checkEdit);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        /// <summary>
        /// delete stock
        /// </summary>
        /// <param name="productId">StockViewModels</param>
        /// <param name="storeId">StockViewModels</param>
        /// <returns>True || False</returns>
        public async Task<bool> Delete(int productId, int storeId)
        {
            try
            {
                var stock = await _context.Stocks.FindAsync(productId, storeId);
                _context.Stocks.Remove(stock);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


        }

    }
}