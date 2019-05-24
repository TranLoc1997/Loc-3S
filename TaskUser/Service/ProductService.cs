using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskUser.Models;
using TaskUser.Models.Production;
using TaskUser.ViewsModels.Product;

namespace TaskUser.Service
{
    public interface IProductService
    {
        Task<List<ProductViewsModels>> GetProductListAsync();

        Task<bool> AddProductAsync(ProductViewsModels addProduct);

        IEnumerable<Product> GetProduct();
        
        Task<ProductViewsModels> GetIdProductAsync(int id);
        
        Task<bool> EditProductAsync(ProductViewsModels editProduct);
        
        Task<bool> Delete(int id);
    }

    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// show list product
        /// </summary>
        /// <returns>listProduct</returns>
        public async Task<List<ProductViewsModels>> GetProductListAsync()//
        {
            var list = await _context.Products.Include(b=>b.Brand).Include(c=>c.Categorie).ToListAsync();
            var listProduct = _mapper.Map<List<ProductViewsModels>>(list);
            return listProduct;
        }
        public IEnumerable<Product>  GetProduct()
        {
            return _context.Products;
        }
        /// <summary>
        /// create product 
        /// </summary>
        /// <param name="addProduct">ProductViewsModels</param>
        /// <returns>True || False</returns>
        public async Task<bool> AddProductAsync(ProductViewsModels addProduct)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", addProduct.PictureFile.FileName);
                using ( var stream = new FileStream(path,FileMode.Create))
                {
                    await addProduct.PictureFile.CopyToAsync(stream);
                }
                var product = new Product()
                {
                    ProductName = addProduct.ProductName,
                    BrandId = addProduct.BrandId,
                    CategoryId = addProduct.CategoryId,
                    ModelYear = addProduct.ModelYear,
                    ListPrice = addProduct.ListPrice,
                    Picture = addProduct.PictureFile.FileName
                };  
                _context.Products.Add(product);
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
        /// edit get product
        /// </summary>
        /// <param name="id">ProductViewsModels</param>
        /// <returns>productDtos</returns>
        public async Task<ProductViewsModels> GetIdProductAsync(int id)
        {
            var findProduct= await _context.Products.FindAsync(id);
            var productDtos = _mapper.Map<ProductViewsModels>(findProduct);
            return productDtos;
        }
        
        /// <summary>
        /// edit post product
        /// </summary>
        /// <param name="editProduct">ProductViewsModels</param>
        /// <returns>true || flase</returns>
        public async Task<bool> EditProductAsync(ProductViewsModels editProduct)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", editProduct.PictureFile.FileName);
                using ( var stream = new FileStream(path,FileMode.Create))
                {
                    await editProduct.PictureFile.CopyToAsync(stream);
                
                }
                var product =await _context.Products.FindAsync(editProduct.Id);
                {
                product.BrandId = editProduct.BrandId;
                product.CategoryId = editProduct.CategoryId;
                product.ProductName = editProduct.ProductName;
                product.Picture = editProduct.PictureFile.FileName;
                product.ModelYear = editProduct.ModelYear;
                product.ListPrice = editProduct.ListPrice;
                }
                _context.Products.Update(product);
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
        /// delete product
        /// </summary>
        /// <param name="id">ProductViewsModels</param>
        /// <returns>True || false</returns>
        public async Task<bool>Delete(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
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