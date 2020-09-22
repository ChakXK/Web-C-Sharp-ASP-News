namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Heading", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Articles", "Briefly", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Articles", "Editors_Choice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Editors_Choice", c => c.Boolean());
            AlterColumn("dbo.Articles", "Briefly", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Articles", "Heading", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
