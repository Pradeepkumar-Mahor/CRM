﻿using CMR.Domain.DataClass;
using MGMTApp.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMR.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly IConfiguration _configuration;

        public ProductRepository(ISqlDataAccess db, IConfiguration configuration)
        {
            _dataAccess = db;
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(AddProduct product)
        {
            try
            {
                await _dataAccess.SaveData("sp_add_Product", new
                {
                    product.ProductName,
                    product.ProductDescription,
                    product.ImgUrl,
                    product.ProductIsActive
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateProduct product)
        {
            try
            {
                await _dataAccess.SaveData("sp_update_Product", new
                {
                    product.Id,
                    product.ProductName,
                    product.ProductDescription,
                    product.ImgUrl,
                    product.ProductIsActive
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _dataAccess.SaveData("sp_delete_Product", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ProductDetails?> GetByIdAsync(int id)
        {
            IEnumerable<ProductDetails> result = await _dataAccess.GetData<ProductDetails, dynamic>
                ("sp_get_Product", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductDataTableList>> GetAllProductAsync(int pageNumber, int rowsOfPage)
        {
            string query = "sp_get_AllProduct";
            return await _dataAccess.GetData<ProductDataTableList, dynamic>(query, new { PageNumber = pageNumber, RowsOfPage = rowsOfPage });
        }
    }
}