namespace SampleMVCWithCQS2Core.DataAccess
{
    using SampleMVCWithCQS2Core.Domain;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                }
                context.SaveChanges();
            }
        }

        public static void ClearData(ApplicationDbContext context)
        {

            context.Products.RemoveRange(context.Products);
            context.SaveChanges();
        }

        private static Product[] Products = {
            new Product { Name = "Kayak", Category = "Watersports",
                Price = 275, Color = Colors.Green, InStock = true },
            new Product { Name = "Lifejacket", Category = "Watersports",
                Price = 48.95m, Color = Colors.Red, InStock = true },
            new Product { Name = "Soccer Ball", Category = "Soccer",
                Price = 19.50m, Color = Colors.Blue, InStock = true },
            new Product { Name = "Corner Flags", Category = "Soccer",
                Price = 34.95m, Color = Colors.Green, InStock = true },
            new Product { Name = "Stadium", Category = "Soccer",
                Price = 79500, Color = Colors.Red, InStock = true },
            new Product { Name = "Thinking Cap", Category = "Chess",
                Price = 16, Color = Colors.Blue, InStock = true },
            new Product { Name = "Unsteady Chair", Category = "Chess",
                Price = 29.95m, Color = Colors.Green, InStock = true },
            new Product { Name = "Human Chess Board", Category = "Chess",
                Price = 75, Color = Colors.Red, InStock = true },
            new Product { Name = "Bling-Bling King", Category = "Chess",
                Price = 1200, Color = Colors.Blue, InStock = true }};
    }
}