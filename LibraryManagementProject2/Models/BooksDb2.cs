namespace LibraryManagementProject2.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BooksDb2 : DbContext
    {
        // Your context has been configured to use a 'BooksDb2' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryManagementProject2.Models.BooksDb2' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BooksDb2' 
        // connection string in the application configuration file.
        public BooksDb2()
            : base("name=BooksDb2")
        {
        }
        public static BooksDb2 Create()
        {
            return new BooksDb2();
        }//What is the use of this???
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Books> Mybooks { get; set; }

        public System.Data.Entity.DbSet<LibraryManagementProject2.Models.BorrowHistory> BorrowHistories { get; set; }

        public System.Data.Entity.DbSet<LibraryManagementProject2.Models.User> Users { get; set; }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}