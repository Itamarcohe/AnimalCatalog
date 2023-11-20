using Microsoft.AspNetCore.Http;
using PetCatalog.Data.Repositories;
using PetCatalog.Models;

namespace PetCatalog.Services
{
    public interface IAnimalService
    {
        Task AddAnimal(Animal animal, IFormFile image);
        Task DeleteAnimal(Animal animal, string categoryName);
        void UpdateAnimalImage(Animal animal, IFormFile? newImage);
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<IEnumerable<AnimalCategory>> GetAnimalCategories();
        Task UpdateAnimal(Animal animal);
        Task<IEnumerable<Animal>> GetAnimalsByCategory(int id);
        void CategoryChanged(Animal existingPet, Animal editedPet);
        Task<List<Animal>> GetTopRatedAnimals(int v);
    }
}