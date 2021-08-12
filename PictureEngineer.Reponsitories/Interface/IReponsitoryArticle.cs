using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories.Interface
{
    public interface IReponsitoryArticle
    {
        Task<string> Create(Blog blog, Stream file);
        Task<Blog> Update(Blog blog, Stream file);
        Task<IEnumerable<Blog>> GetBlogsAsync();
        Task<bool> DeleteAsync(int key);
        Task<Blog> GetCurentBlogAsync(int id);
        Task<Blog> GetCurentBlogByMetaAsync(string meta);
    }
}
