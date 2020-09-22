namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AritcleTypeChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Text", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("dbo.Articles", "Heading", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Articles", "Briefly", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Briefly", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.Articles", "Heading", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.Articles", "Text", c => c.String(maxLength: 20, unicode: false));
        }
    }
}
