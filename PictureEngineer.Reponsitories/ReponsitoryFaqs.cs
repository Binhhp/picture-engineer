using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.Reponsitories
{
    public class ReponsitoryFaqs : IReponsitory<FAQs>
    {
        private readonly PictureEngineerDbContext _context;
        public ReponsitoryFaqs(PictureEngineerDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create faqs
        /// </summary>
        /// <param name="blog">faqs input</param>
        /// <returns>Create faqs</returns>
        public async Task<FAQs> Create(FAQs faqs)
        { 
            var obj = await _context.FAQs.AddAsync(faqs);
            await _context.SaveChangesAsync();
            return obj.Entity;
        }
        /// <summary>
        /// Update faqs
        /// </summary>
        /// <param name="faqs">faqs input</param>
        public void Update(FAQs faqs)
        {
            _context.FAQs.Update(faqs);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FAQs> GetAll()
        {
            return _context.FAQs.ToList();
        }
        /// <summary>
        /// Delete faqs
        /// </summary>
        /// <param name="faqs"> Delete faqs</param>
        public void Delete(FAQs faqs)
        {
            _context.FAQs.Remove(faqs);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get faqs by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Get faqs by id</returns>
        public FAQs GetId(int Id)
        {
            var item = _context.FAQs.FirstOrDefault(x => x.Id.Equals(Id));
            return item;
        }

    }
}
