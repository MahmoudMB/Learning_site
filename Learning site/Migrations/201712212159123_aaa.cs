namespace Learning_site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "subject", c => c.String(nullable: false));
            AlterColumn("dbo.Assignments", "File", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assignments", "File", c => c.String());
            AlterColumn("dbo.Assignments", "subject", c => c.String());
        }
    }
}
