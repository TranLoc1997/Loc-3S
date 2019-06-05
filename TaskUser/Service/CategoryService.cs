using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskUser.Models;
using TaskUser.Models.Production;
using TaskUser.ViewsModels.Category;

namespace TaskUser.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryViewsModels>> GetCategoryListAsync();
        
        Task<bool> AddCategoryAsync(CategoryViewsModels addCategory);
        
        IEnumerable<Category> GetCategory();
        
        Task<CategoryViewsModels> GetIdCategoryAsync(int id);
        
        Task<bool> EditCategoryAsync(CategoryViewsModels editCategory);
        
        bool IsExistedName(int id, string name);
        
        Task<bool> Delete(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// show category list
        /// </summary>
        /// <returns>listCategory</returns>
        public async Task<List<CategoryViewsModels>> GetCategoryListAsync()//
        {
            var list = await _context.Categories.ToListAsync();
            var listCategory = _mapper.Map<List<CategoryViewsModels>>(list);
            return listCategory;
        }
        /// <summary>
        ///  create category service
        /// </summary>
        /// <param name="addCategory">CategoryViewsModels</param>
        /// <returns>true || false</returns>
        public async Task<bool> AddCategoryAsync(CategoryViewsModels addCategory)
        {
            try
            {
                var category = new Category()
                {
                    CategoryName = addCategory.CategoryName
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Add Category Async Error: {0}",e.Message);

                return false;
            }
        }
        public IEnumerable<Category> GetCategory()
        {
            return _context.Categories;
        }
        /// <summary>
        /// get id category edit 
        /// </summary>
        /// <param name="id">CategoryViewsModels</param>
        /// <returns></returns>
        public async Task<CategoryViewsModels> GetIdCategoryAsync(int id)
        {
            var findCategory=await _context.Categories.FindAsync(id);
            var categoryDtos = _mapper.Map<CategoryViewsModels>(findCategory);
            return categoryDtos;
        }
        /// <summary>
        /// post edit category
        /// </summary>
        /// <param name="editCategory">CategoryViewsModels</param>
        /// <returns></returns>
        public async Task<bool> EditCategoryAsync(CategoryViewsModels editCategory)
        {
            try
            {
                var category =await _context.Categories.FindAsync(editCategory.Id);
            
                category.CategoryName = editCategory.CategoryName;
            
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Edit Category Async Error: {0}",e.Message);
                return false;
            }
        }
        public bool IsExistedName(int id,string name)
        {
            return _context.Categories.Any(x => x.CategoryName == name && x.Id != id);
        }
        /// <summary>
        /// delete category
        /// </summary>
        /// <param name="id">CategoryViewsModels</param>
        /// <returns>True || false</returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Delete Category Async Error: {0}",e.Message);

                return false;
            }
        }
    }
}