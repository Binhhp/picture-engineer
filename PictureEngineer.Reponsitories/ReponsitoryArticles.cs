using Firebase.Storage;
using Microsoft.EntityFrameworkCore;
using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories.Firebase;
using PictureEngineer.Reponsitories.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories
{
    public class ReponsitoryArticles : IReponsitoryArticle
    {
        private readonly PictureEngineerDbContext _context;
        private FirebaseStorage storage;

        public ReponsitoryArticles(PictureEngineerDbContext context, string storageBuket)
        {
            _context = context;
            storage = new FirebaseStorage(storageBuket);
        }
        /// <summary>
        /// Create blog
        /// </summary>
        /// <param name="blog">blog input</param>
        /// <returns>Create blog</returns>
        public async Task<string> Create(Blog blog, Stream file)
        {
            var isArticle = _context.Blogs.FirstOrDefault(x => x.Title == blog.Title);
            if (isArticle != null) return "Tiêu đề bài viết đã tồn tại";

            if (file != null)
            {
                var cancleToken = new CancellationTokenSource();
                await storage.Child(ApplicationFirebase.BLOGS).Child(blog.ImageName).PutAsync(file, cancleToken.Token);
                var path = await storage.Child(ApplicationFirebase.BLOGS).Child(blog.ImageName).GetDownloadUrlAsync();
                blog.ImagePath = path;
                blog.DateCreated = DateTime.UtcNow;
            }

            var obj = await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return "";
        }

        public async Task<Blog> Update(Blog blog, Stream file)
        {
            var isArticle = _context.Blogs.FirstOrDefault(x => x.Id == blog.Id);
            if (isArticle == null) return null;

            if (file != null)
            {
                await storage.Child(ApplicationFirebase.BLOGS).Child(isArticle.ImageName).DeleteAsync();
                var cancleToken = new CancellationTokenSource();
                await storage.Child(ApplicationFirebase.BLOGS).Child(blog.ImageName).PutAsync(file, cancleToken.Token);
                var path = await storage.Child(ApplicationFirebase.BLOGS).Child(blog.ImageName).GetDownloadUrlAsync();
                isArticle.ImagePath = path;
                isArticle.ImageName = blog.ImageName;
            }

            isArticle.Title = blog.Title;
            isArticle.Contents = blog.Contents;
            isArticle.Description = blog.Description;
            isArticle.MetaTitle = blog.MetaTitle;
            isArticle.ServiceId = blog.ServiceId;
            await _context.SaveChangesAsync();
            return isArticle;
        }

        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return blogs;
        }

        public async Task<Blog> GetCurentBlogAsync(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            return blog;
        }
        
        public async Task<Blog> GetCurentBlogByMetaAsync(string meta)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.MetaTitle == meta);
            return blog;
        }
        /// <summary>
        /// Delete blog
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int key)
        {
            try
            {
                var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == key);
                if (blog == null) return false;

                await storage.Child(ApplicationFirebase.BLOGS).Child(blog.ImageName).DeleteAsync();
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
