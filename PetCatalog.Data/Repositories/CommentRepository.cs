using Microsoft.EntityFrameworkCore;
using PetCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Data.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {

        private PetContext _context;
        public CommentRepository(PetContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment entity)
        {
            _context.Comments!.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments!.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByIdAsync(int petId)
        {
            return await _context.Comments!.AsNoTracking().Where(c => c.AnimalId == petId).ToListAsync();
        }


        public async Task DeleteAsync(int id)
        {
            try
            {
                Comment commentToDelete = await _context.Comments!.SingleAsync(m => m.CommentId == id);
                _context.Comments!.Remove(commentToDelete);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {

            }
        }

        public async Task UpdateAsync(Comment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments!.FindAsync(id);
        }

        public Task<List<Animal>> GetMostCommented(int count)
        {
            throw new NotImplementedException();
        }
    }
}
