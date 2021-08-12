using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PictureEngineer.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace PictureEngineer.Data
{
    public partial class PictureEngineerDbContext : IdentityDbContext<Entities.AspNetUser, IdentityRole, string>
    {
        public PictureEngineerDbContext(DbContextOptions<PictureEngineerDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Entities.Files> Files { get; set; }
        public virtual DbSet<Entities.AspNetUser> User { get; set; }
        public virtual DbSet<IdentityRole> Role { get; set; }
        public virtual DbSet<Entities.Blog> Blogs { get; set; }
        public virtual DbSet<Entities.Services> Services{ get; set; }
        public virtual DbSet<Entities.FAQs> FAQs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seeder();
            base.OnModelCreating(modelBuilder);
            #region
            //Replace table names
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase(true);

                // Replace column names            
                //foreach (var property in entity.GetProperties())
                //{
                //    property.Relational().ColumnName = property.Name.ToSnakeCase(true);
                //}

                //foreach (var key in entity.GetKeys())
                //{
                //    key.Relational().Name = key.Relational().Name.ToSnakeCase(true);
                //}

                //foreach (var key in entity.GetForeignKeys())
                //{
                //    key.Relational().Name = key.Relational().Name.ToSnakeCase(true);
                //}

                //foreach (var index in entity.GetIndexes())
                //{
                //    index.Relational().Name = index.Relational().Name.ToSnakeCase(true);
                //}
            }
            #endregion
        }
    }
}
