using PetCatalog.Data.Repositories;
using PetCatalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Services
{
    public class CommentService : ICommentService
    {

        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.CommmetTime = DateTime.Now;
            await _commentRepository.AddAsync(comment);
        }

        public async Task DeleteCommentsAsync(int animalId)
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsToRemove = comments.Where(c => c.AnimalId == animalId).ToList();
            foreach (var comment in commentsToRemove)
            {
                await _commentRepository.DeleteAsync(comment.CommentId);
            }
        }

        public async Task UpdateComment(int commentId, string editedText)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId)!;
            comment!.CommentText = editedText;
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);

        }
    }
}
