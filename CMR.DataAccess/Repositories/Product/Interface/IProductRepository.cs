using CMR.Domain.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMR.DataAccess.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> AddAsync(AddProduct product);

        public Task<bool> UpdateAsync(UpdateProduct product);

        public Task<bool> DeleteAsync(int id);

        public Task<ProductDetails?> GetByIdAsync(int id);

        public Task<IEnumerable<ProductDataTableList>> GetAllProductAsync(int pageNumber = 1, int rowsOfPage = 1);
    }
}