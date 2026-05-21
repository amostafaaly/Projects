using Projects.Src.Data;
namespace Projects.Src
{
    public class Program
    {
        static void Main(string[] args)
        {

                using var context = new ApplicationDbContext();
                context.Database.EnsureCreated();



        }
    }
}