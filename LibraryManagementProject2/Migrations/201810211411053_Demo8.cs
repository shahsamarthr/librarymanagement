namespace LibraryManagementProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Demo8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Contact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Contact", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
        }
    }
}
