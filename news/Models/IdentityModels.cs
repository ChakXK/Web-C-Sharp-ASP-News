using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace news.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            Articles = new HashSet<Article>();
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Heading)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Briefly)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Article)
                .HasForeignKey(e => e.Id_Article)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Article)
                .HasForeignKey(e => e.Id_Article)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Id_category)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Comment>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Id_user)
                .WillCascadeOnDelete();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}