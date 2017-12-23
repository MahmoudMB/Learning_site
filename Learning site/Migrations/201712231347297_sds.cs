namespace Learning_site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EnrolledCourses", "assignment_Id", "dbo.Assignments");
            DropIndex("dbo.EnrolledCourses", new[] { "assignment_Id" });
            DropColumn("dbo.EnrolledCourses", "assignment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnrolledCourses", "assignment_Id", c => c.Int());
            CreateIndex("dbo.EnrolledCourses", "assignment_Id");
            AddForeignKey("dbo.EnrolledCourses", "assignment_Id", "dbo.Assignments", "Id");
        }
    }
}
