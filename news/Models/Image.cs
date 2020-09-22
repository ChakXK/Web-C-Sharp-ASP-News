namespace news.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? Number { get; set; }

        public int? Id_Article { get; set; }

        public virtual Article Article { get; set; }
    }
}
