using Microsoft.EntityFrameworkCore;
using PetCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Data.Repositories
{
    public class CategoryRepository : IRepository<AnimalCategory>
    {

        private PetContext _context;

        public CategoryRepository(PetContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AnimalCategory entity)
        {
            _context.AnimalCategories!.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            AnimalCategory categoryToDelete = _context.AnimalCategories!.Single(m => m.CategoryId == id);

            if (categoryToDelete != null)
            {
                _context.AnimalCategories!.Remove(categoryToDelete);
                _context.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            AnimalCategory? categoryToDelete = await _context.AnimalCategories!.FindAsync(id);

            if (categoryToDelete != null)
            {
                _context.AnimalCategories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
            }
        }


     
        public async Task<AnimalCategory?> GetByIdAsync(int id)
        {
            return await _context.AnimalCategories!.SingleOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task<IEnumerable<AnimalCategory>> GetAllAsync()
        {
            var categories = await _context.AnimalCategories!.ToListAsync();
            return categories.AsEnumerable();
        }

        public Task UpdateAsync(AnimalCategory entity)
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<AnimalCategory>> GetAllByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Animal>> GetMostCommented(int count)
        {
            throw new NotImplementedException();
        }
    }
}
