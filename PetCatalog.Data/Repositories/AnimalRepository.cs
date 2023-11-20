using Microsoft.EntityFrameworkCore;
using PetCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Data.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {

        private PetContext _context;

        public AnimalRepository(PetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> GetAllByIdAsync(int categoryId)
        {
            var data = _context.Animals?.Include(x => x.Comments).Where(animal => categoryId == 0 || animal.CategoryId == categoryId);
            return await data!.ToListAsync();
        }

        public async Task AddAsync(Animal entity)
        {
            _context.Animals!.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Animal? animalToDelete = await _context.Animals!.FindAsync(id);

            if (animalToDelete != null)
            {
                _context.Animals.Remove(animalToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals!.ToListAsync();
        }

        public async Task<Animal?> GetByIdAsync(int id)
        {
            return await _context.Animals!.FindAsync(id);
        }

        public async Task UpdateAsync(Animal entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Animal>> GetMostCommented(int v) => await _context.Animals!.OrderByDescending(animal => animal.Comments.Count).Take(v).ToListAsync();
    }
}
