using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories
{
    public class ReponsitoryServices : IReponsitory<Services>
    {
        private readonly PictureEngineerDbContext _context;
        public ReponsitoryServices(PictureEngineerDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create service
        /// </summary>
        /// <param name="service">service input</param>
        /// <returns>Create service</returns>
        public async Task<Services> Create(Services service)
        {
            var obj = await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return obj.Entity;
        }
        /// <summary>
        /// Update service
        /// </summary>
        /// <param name="service">service input</param>
        public void Update(Services menu)
        {
            _context.Services.Update(menu);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Services> GetAll()
        {
            return _context.Services.ToList();
        }
        /// <summary>
        /// Delete service
        /// </summary>
        /// <param name="service"> Delete service</param>
        public void Delete(Services service)
        {
            _context.Services.Remove(service);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get service by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Get service by id</returns>
        public Services GetId(int Id)
        {
            var item = _context.Services.FirstOrDefault(x => x.Id.Equals(Id));
            return item;
        }

    }
}
