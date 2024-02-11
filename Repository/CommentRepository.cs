using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Data;
using dotnet8_api.Interfaces;
using dotnet8_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet8_api.Repository
{
    public class CommentRepository : ICommentRepository
    {   
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            return commentModel;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null) 
            {
                return null;
            }

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Title;

            await _context.SaveChangesAsync();
            
            return existingComment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null) 
            {
                return null;
            }

            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}