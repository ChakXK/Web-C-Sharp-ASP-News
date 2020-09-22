namespace news.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        public DateTime? Datetime { get; set; }

        [StringLength(50)]
        public string User_Name { get; set; }

        public int? Likes { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int? Id_Article { get; set; }

        public virtual Article Article { get; set; }
    }
}
