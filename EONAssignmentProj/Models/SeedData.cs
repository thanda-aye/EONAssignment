using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EONAssignmentProj.Models;
using System;
using System.Linq;

namespace EONAssignmentProj.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EONAssignmentDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EONAssignmentDBContext>>()))
            {
                // Look for any movies.
                if (context.UserTbls.Any())
                {
                    return;   // DB has been seeded
                }

                context.UserTbls.AddRange(
                    new UserTbl
                    {
                        Name = "Thanda Aye",
                        Email = "ms.thandaaye@gmail.com",
                        Gender = "F",
                        DateReg = "06/06/2022",
                        SelectedDays ="Day 1,Day 2",
                        AreaOfInterest ="" ,
                        AddRequest= ""
                    },

                    new UserTbl
                    {
                        Name = "Peter Even",
                        Email = "pet@gmail.com",
                        Gender = "M",
                        DateReg = "2/3/2022",
                        SelectedDays = "Day 1,Day 3" ,
                        AreaOfInterest = "",
                        AddRequest ="To provide rooms."
                    },

                    new UserTbl
                    {
                        Name = "Aung Aung",
                        Email = "Arqueen.77@gmail.com",
                        Gender = "M",
                        DateReg = "07/06/2022",
                        SelectedDays = "Day 2,Day3",
                        AreaOfInterest = "",
                        AddRequest = "To test for something."
                    } 
                );
                context.SaveChanges();
            }
        }
    }
}