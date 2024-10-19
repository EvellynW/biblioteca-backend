using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<ClassRoom> ClassRooms { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<LiteraryGenre> LiteraryGenres { get; set; }
    public DbSet<BookRecommendation> BookRecommendations { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<TeacherClassRoom> TeacherClassRooms { get; set; }

    // Adicione este construtor que aceita DbContextOptions
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne(s => s.ClassRoom)
            .WithMany(c => c.Students)
            .HasForeignKey("ClassRoomId");

        modelBuilder.Entity<TeacherClassRoom>()
            .HasKey(tc => new { tc.TeacherId, tc.ClassRoomId });

        modelBuilder.Entity<TeacherClassRoom>()
            .HasOne(tc => tc.Teacher)
            .WithMany(t => t.TeacherClassRooms)
            .HasForeignKey(tc => tc.TeacherId);

        modelBuilder.Entity<TeacherClassRoom>()
            .HasOne(tc => tc.ClassRoom)
            .WithMany(c => c.TeacherClassRooms)
            .HasForeignKey(tc => tc.ClassRoomId);

        modelBuilder.Entity<BookRecommendation>()
            .HasOne(br => br.Teacher)
            .WithMany()
            .HasForeignKey("TeacherId");

        modelBuilder.Entity<BookRecommendation>()
            .HasOne(br => br.Book)
            .WithMany()
            .HasForeignKey("BookId");

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Student)
            .WithMany(s => s.Rentals)
            .HasForeignKey(r => r.StudentId);

            
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Book)
            .WithMany(s => s.Rentals)
            .HasForeignKey(r => r.BookId);

            
        modelBuilder.Entity<Book>()
            .HasOne(r => r.LiteraryGenre)
            .WithMany(s => s.Books)
            .HasForeignKey(r => r.LiteraryGenreId);
    }
}
