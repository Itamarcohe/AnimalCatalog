using Microsoft.AspNetCore.Http;
using PetCatalog.Data.Repositories;
using PetCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Services
{
    public class AnimalService : IAnimalService
    {
        private const string ImageLocationRoot = "wwwroot/Images";
        private readonly IRepository<Animal> _animalRepository;
        private readonly IRepository<AnimalCategory> _categoryRepository;
        public AnimalService(IRepository<Animal> animalRepository, IRepository<AnimalCategory> categoryRepository)
        {
            _animalRepository = animalRepository;
            _categoryRepository = categoryRepository;
        }

        #nullable disable
        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await _animalRepository.GetByIdAsync(id)!;
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByCategory(int id)
        {
            return await _animalRepository.GetAllByIdAsync(id);

        }

        public async Task AddAnimal(Animal model, IFormFile image)
        {
            model.ImagePath = SaveImage(image, model!.Category!.Name!);
            await _animalRepository.AddAsync(model);
        }

        public async Task DeleteAnimal(Animal animal, string categoryName)
        {

            string imagePath = Path.Combine(ImageLocationRoot, categoryName, animal.ImagePath!);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            await _animalRepository.DeleteAsync(animal.AnimalId);

        }


        public void UpdateAnimalImage(Animal animal, IFormFile newImage)
        {
            if (newImage != null)
            {
                string oldImagePath = Path.Combine(ImageLocationRoot, animal.Category.Name, animal.ImagePath!);
                File.Delete(oldImagePath);
                animal.ImagePath = SaveImage(newImage, animal.Category!.Name!);
            }

        }

        private static string SaveImage(IFormFile image, string categoryName)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            string imagePath = Path.Combine(ImageLocationRoot, categoryName, fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return fileName;

        }

        public async Task UpdateAnimal(Animal animal)
        {

            await _animalRepository.UpdateAsync(animal);
        }

        public async Task<IEnumerable<AnimalCategory>> GetAnimalCategories()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public void CategoryChanged(Animal existingPet, Animal editedPet)
        {

            if (existingPet!.CategoryId != editedPet.CategoryId)
            {
                string newImagePath = Path.Combine(ImageLocationRoot, editedPet.Category!.Name!, editedPet.ImagePath!);
                string oldImagePath = Path.Combine(ImageLocationRoot, existingPet!.Category!.Name!, editedPet.ImagePath!);
                System.IO.File.Move(oldImagePath, newImagePath);
                existingPet.CategoryId = editedPet.CategoryId;
                existingPet.Category = editedPet.Category;
            }

            existingPet.Description = editedPet.Description;
            existingPet.Name = editedPet.Name;
            existingPet.Birth = editedPet.Birth;
        }

        public async Task<List<Animal>> GetTopRatedAnimals(int v)
        {
            return await _animalRepository.GetMostCommented(v);
        }



    }
}
