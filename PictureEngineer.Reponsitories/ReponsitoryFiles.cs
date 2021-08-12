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
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories
{
    public class ReponsitoryFiles : IReponsitoryFile
    {
        private readonly PictureEngineerDbContext _context;
        private readonly FirebaseStorage storage;

        public ReponsitoryFiles(PictureEngineerDbContext context, string storageBuket)
        {
            _context = context;
            storage = new FirebaseStorage(storageBuket);
        }
        public async Task Create(Files file)
        {
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Files>> GetAll()
        {
            var list = await _context.Files.ToListAsync();
            return list;
        }
        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="file"> Delete file</param>
        public async Task Delete(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            await storage.Child(ApplicationFirebase.FILES).Child(file.FileName).DeleteAsync();
            _context.Files.Remove(file);
            _context.SaveChanges(); 
        }
        /// <summary>
        /// Get file by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Get file by id</returns>
        public async Task<string> GetFilePath(int Id)
        {
            var item = await _context.Files.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            if (item == null) return "";
            string path = await storage.Child(ApplicationFirebase.FILES).Child(item.FileName).GetDownloadUrlAsync();
            return path;
        }

    }
}
