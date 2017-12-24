namespace Learning_site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12222222 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "code", c => c.String());
            AlterColumn("dbo.Courses", "name", c => c.String());
        }
    }
}
