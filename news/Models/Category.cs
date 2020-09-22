namespace news.Models
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name = "Категория")]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
    }

    public partial class LastsCategories
    {

            //(new ApplicationDbContext()).Categories.SelectMany(c => c.Articles,
            //(c, a) => new { Category = c, Articles = a }).OrderByDescending(c => c.Articles).Take(4)
            //.Select(c => c.Category).ToList();
    }
}
