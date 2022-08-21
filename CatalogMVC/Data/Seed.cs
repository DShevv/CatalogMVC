using CatalogMVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace CatalogMVC.Data
{
    public class Seed
    {
        public static void SeedData (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
              
                context.Database.EnsureCreated();

               
                
                

             

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Еда"
                        },
                        new Category() 
                        { 
                            Name = "Вода" 
                        },
                        new Category()
                        {
                            Name = "Вкусности"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Products.Any())
                {
                    var categories = context.Categories.ToList();
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Селёдка",
                            Category = categories.ElementAt(0),
                            Description = "Селедка соленая",
                            Price = 10,
                            PublicNote = "Акция",
                            PrivateNote = "Пересоленая"
                        },
                        new Product()
                        {
                            Name = "Тушенка",
                            Category = categories.ElementAt(0),
                            Description = "Тушенка говяжая",
                            Price = 20,
                            PublicNote = "Вкусная",
                            PrivateNote = "Жилы"
                        },
                        new Product()
                        {
                            Name = "Сгущенка",
                            Category = categories.ElementAt(2),
                            Description = "В банках",
                            Price = 30,
                            PublicNote = "С ключом",
                            PrivateNote = "Вкусная"
                        },
                        new Product()
                        {
                            Name = "Квас",
                            Category = categories.ElementAt(1),
                            Description = "В бутылках",
                            Price = 15,
                            PublicNote = "Вятский",
                            PrivateNote = "Теплый"
                        },

                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Advanced))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Advanced));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                        Login = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Password = "123"
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "user",
                        Login = "user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Password= "123"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
                string appAdvancedEmail = "useradvanced@gmail.com";

                var appAdvanced = await userManager.FindByEmailAsync(appAdvancedEmail);
                if (appAdvanced == null)
                {
                    var newAppAcvanced = new User()
                    {
                        UserName = "advanced",
                        Login = "advanced",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Password = "123"
                    };
                    await userManager.CreateAsync(newAppAcvanced, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppAcvanced, UserRoles.Advanced);
                }
            }
        }
    }
}
