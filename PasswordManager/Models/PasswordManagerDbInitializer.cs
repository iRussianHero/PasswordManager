namespace PasswordManager.Models
{
    public class PasswordManagerDbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<ManagerDbContext>();

            if (!context.Types.Any())
            {
                context.Types.Add(new Type { Name = "Windows" });
                context.Types.Add(new Type { Name = "Games" });
                context.Types.Add(new Type { Name = "Post" });
                context.Types.Add(new Type { Name = "Work" });
                context.Types.Add(new Type { Name = "Study" });
                context.Types.Add(new Type { Name = "Others" });
                context.SaveChanges();
            }
        }
    }
}
