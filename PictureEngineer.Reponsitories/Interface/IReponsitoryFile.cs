using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories.Interface
{
    public interface IReponsitoryFile
    {
        Task Create(Files file);
        Task<IEnumerable<Files>> GetAll();
        Task Delete(int id);
        Task<string> GetFilePath(int Id);
    }
}
