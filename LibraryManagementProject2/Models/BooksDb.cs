namespace LibraryManagementProject2.Models
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;

    public class BooksDb : DbContext
    {
        // Your context has been configured to use a 'BooksDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryManagementProject2.Models.BooksDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BooksDb' 
        // connection string in the application configuration file.
        public BooksDb()
            : base("name=BooksDb")
        {

        }
        public static BooksDb Create()
        {
            return new BooksDb();
        }//What is the use of this???
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Books> Mybooks { get; set; }
        public virtual DbSet<User> Myuser { get; set; }
        public virtual DbSet<BorrowHistory> borrowhistory { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultConnection,Configuration>());
        //}
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}