namespace LibraryManagementProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Demp9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Contact");
            DropColumn("dbo.Users", "Address");
        }
    }
}
