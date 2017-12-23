namespace Learning_site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "File", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assignments", "File", c => c.String(nullable: false));
        }
    }
}
