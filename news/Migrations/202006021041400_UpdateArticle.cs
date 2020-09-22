namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArticle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Text", c => c.String(unicode: false));
            AlterColumn("dbo.Articles", "Heading", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Articles", "Briefly", c => c.String(nullable: false, maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Briefly", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Articles", "Heading", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Articles", "Text", c => c.String(maxLength: 6000, unicode: false));
        }
    }
}
