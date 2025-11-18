using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class LocationSeeder
    {
        public static void SeedLocations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locations>().HasData(
                // DÃY A
                new Locations { LocationID = 1, LocName = "Kệ 1, dãy A" },
                new Locations { LocationID = 2, LocName = "Kệ 2, dãy A" },
                new Locations { LocationID = 3, LocName = "Kệ 3, dãy A" },
                new Locations { LocationID = 4, LocName = "Kệ 4, dãy A" },
                new Locations { LocationID = 5, LocName = "Kệ 5, dãy A" },

                // DÃY B
                new Locations { LocationID = 6, LocName = "Kệ 1, dãy B" },
                new Locations { LocationID = 7, LocName = "Kệ 2, dãy B" },
                new Locations { LocationID = 8, LocName = "Kệ 3, dãy B" },
                new Locations { LocationID = 9, LocName = "Kệ 4, dãy B" },
                new Locations { LocationID = 10, LocName = "Kệ 5, dãy B" },

                // DÃY C
                new Locations { LocationID = 11, LocName = "Kệ 1, dãy C" },
                new Locations { LocationID = 12, LocName = "Kệ 2, dãy C" },
                new Locations { LocationID = 13, LocName = "Kệ 3, dãy C" },
                new Locations { LocationID = 14, LocName = "Kệ 4, dãy C" },
                new Locations { LocationID = 15, LocName = "Kệ 5, dãy C" },

                // DÃY D
                new Locations { LocationID = 16, LocName = "Kệ 1, dãy D" },
                new Locations { LocationID = 17, LocName = "Kệ 2, dãy D" },
                new Locations { LocationID = 18, LocName = "Kệ 3, dãy D" },
                new Locations { LocationID = 19, LocName = "Kệ 4, dãy D" },
                new Locations { LocationID = 20, LocName = "Kệ 5, dãy D" },

                // DÃY E
                new Locations { LocationID = 21, LocName = "Kệ 1, dãy E" },
                new Locations { LocationID = 22, LocName = "Kệ 2, dãy E" },
                new Locations { LocationID = 23, LocName = "Kệ 3, dãy E" },
                new Locations { LocationID = 24, LocName = "Kệ 4, dãy E" },
                new Locations { LocationID = 25, LocName = "Kệ 5, dãy E" }
            );
        }

    }
}
