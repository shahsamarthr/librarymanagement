namespace LibraryManagementProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Demo7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Contact", c => c.String(nullable: false));
        }
    }
}
