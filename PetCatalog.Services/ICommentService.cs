using PetCatalog.Models;

namespace PetCatalog.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentsAsync(int animalId);
        Task UpdateComment(int commentId, string editedText);
        Task DeleteCommentAsync(int commentId);

    }
}