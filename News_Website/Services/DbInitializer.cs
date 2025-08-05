using dotless.Core.Parser;
using dotless.Core.Parser.Tree;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using News_Website.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;

public static class DbInitializer
{
    public static void Seed(NewsWebsiteContext context)
    {
        // Seed NewsCategories
        if (!context.NewsCategories.Any())
        {
            context.NewsCategories.AddRange(
                new NewsCategory { Name = "Articles" },
                new NewsCategory { Name = "Reports" },
                new NewsCategory { Name = "Breaking" }
            );
            context.SaveChanges();
        }

        // Seed Roles
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Name = "Admin" },
                new Role { Name = "Editor" }
            );
            context.SaveChanges();
        }

            // Seed Users
        User userAdmin = null;
        if (!context.Users.Any())
        {
            userAdmin = new User
            {
                Phone = "6123456789",
                Email = "admin@news.com",
                Password = "admin@news.com",
                UserRoles = new List<UserRole>()
            };

            var adminrole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (adminrole != null)
            {
                userAdmin.UserRoles.Add(new UserRole { Role = adminrole });
            }

            context.Users.Add(userAdmin);
            context.SaveChanges();
        }
        else
        {
            userAdmin = context.Users.FirstOrDefault(u => u.Email == "admin@news.com");
        }

        // Seed News
        if (!context.News.Any())
        {
            var allNewsCategories = context.NewsCategories.Select(q => q.Id).ToList();
            var images = new[] { "image1.jpg", "image2.jpg", "image3.jpg" };
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                context.News.Add(new News
                {
                    NewsCategoryId = allNewsCategories[rnd.Next(allNewsCategories.Count)],
                    User = userAdmin,
                    CreatedDate = DateTime.UtcNow,
                    Title = "Hi here",
                    NewsStatus = NewsStatus.Draft,
                    Summary = "<ul>...</ul>",
                    ImageUrl = images[rnd.Next(images.Length)],
                    Content = @"<p>...</p>"
                });
            }
            context.SaveChanges();
        }
    }
}