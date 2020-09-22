namespace news.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            Images = new HashSet<Image>();
            Comments = new HashSet<Comment>();
        }

        [Display(Name = "Код")]
        public int Id { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime? Datetime { get; set; }

        [StringLength(12000)]
        [UIHint("MultilineText")]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [StringLength(200)]
        [Required]
        [Display(Name = "Заголовок")]
        public string Heading { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Кратко")]
        public string Briefly { get; set; }

        [Required]
        [UIHint("Boolean")]
        [Display(Name = "Выбор редакции")]
        public bool? Editors_Choice { get; set; }

        [Display(Name = "Количество просмотров")]
        public int? Views { get; set; }

        [Display(Name = "Количество лайков")]
        public int? Likes { get; set; }

        [Display(Name = "Пользователь")]
        public string Id_user { get; set; }

        [Display(Name = "Категория")]
        public int? Id_category { get; set; }

        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Категория")]
        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
