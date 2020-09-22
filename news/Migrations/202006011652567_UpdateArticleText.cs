namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArticleText : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Text", c => c.String(maxLength: 6000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Text", c => c.String(maxLength: 1000, unicode: false));
        }
    }
}
