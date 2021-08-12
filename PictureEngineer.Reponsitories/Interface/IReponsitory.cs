using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories
{
    public interface IReponsitory<T>
    {
        /// <summary>
        /// Create T
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        Task<T> Create(T _object);
        /// <summary>
        /// Update T
        /// </summary>
        /// <param name="_object"></param>
        void Update(T _object);
        /// <summary>
        /// Get all list T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Get T by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        T GetId(int Id);
        /// <summary>
        /// Delete T
        /// </summary>
        /// <param name="_object"></param>
        void Delete(T _object);
    }
}
