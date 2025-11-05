using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class BookCopySeeder
    {
        public static void SeedBookCopy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCopies>().HasData(
                // Book 1–10 (3 copies each = 30)
                new BookCopies { CopyID = "C001", BookID = 1, Status = "1", Location = "Shelf A1", DateAdded = new DateTime(2023, 1, 2) },
                new BookCopies { CopyID = "C002", BookID = 1, Status = "0", Location = "Shelf B1", DateAdded = new DateTime(2023, 2, 5) },
                new BookCopies { CopyID = "C003", BookID = 1, Status = "-1", Location = "Shelf C1", DateAdded = new DateTime(2023, 3, 7) },

                new BookCopies { CopyID = "C004", BookID = 2, Status = "1", Location = "Shelf A2", DateAdded = new DateTime(2023, 1, 10) },
                new BookCopies { CopyID = "C005", BookID = 2, Status = "0", Location = "Shelf B2", DateAdded = new DateTime(2023, 2, 12) },
                new BookCopies { CopyID = "C006", BookID = 2, Status = "-1", Location = "Shelf C2", DateAdded = new DateTime(2023, 3, 15) },

                new BookCopies { CopyID = "C007", BookID = 3, Status = "1", Location = "Shelf A3", DateAdded = new DateTime(2023, 1, 18) },
                new BookCopies { CopyID = "C008", BookID = 3, Status = "0", Location = "Shelf B3", DateAdded = new DateTime(2023, 2, 20) },
                new BookCopies { CopyID = "C009", BookID = 3, Status = "-1", Location = "Shelf C3", DateAdded = new DateTime(2023, 3, 22) },

                new BookCopies { CopyID = "C010", BookID = 4, Status = "1", Location = "Shelf A4", DateAdded = new DateTime(2023, 1, 25) },
                new BookCopies { CopyID = "C011", BookID = 4, Status = "0", Location = "Shelf B4", DateAdded = new DateTime(2023, 2, 27) },
                new BookCopies { CopyID = "C012", BookID = 4, Status = "-1", Location = "Shelf C4", DateAdded = new DateTime(2023, 3, 29) },

                new BookCopies { CopyID = "C013", BookID = 5, Status = "1", Location = "Shelf A5", DateAdded = new DateTime(2023, 4, 1) },
                new BookCopies { CopyID = "C014", BookID = 5, Status = "0", Location = "Shelf B5", DateAdded = new DateTime(2023, 4, 3) },
                new BookCopies { CopyID = "C015", BookID = 5, Status = "-1", Location = "Shelf C5", DateAdded = new DateTime(2023, 4, 5) },

                new BookCopies { CopyID = "C016", BookID = 6, Status = "1", Location = "Shelf A6", DateAdded = new DateTime(2023, 4, 7) },
                new BookCopies { CopyID = "C017", BookID = 6, Status = "0", Location = "Shelf B6", DateAdded = new DateTime(2023, 4, 9) },
                new BookCopies { CopyID = "C018", BookID = 6, Status = "-1", Location = "Shelf C6", DateAdded = new DateTime(2023, 4, 11) },

                new BookCopies { CopyID = "C019", BookID = 7, Status = "1", Location = "Shelf A7", DateAdded = new DateTime(2023, 5, 1) },
                new BookCopies { CopyID = "C020", BookID = 7, Status = "0", Location = "Shelf B7", DateAdded = new DateTime(2023, 5, 3) },
                new BookCopies { CopyID = "C021", BookID = 7, Status = "-1", Location = "Shelf C7", DateAdded = new DateTime(2023, 5, 5) },

                new BookCopies { CopyID = "C022", BookID = 8, Status = "1", Location = "Shelf A8", DateAdded = new DateTime(2023, 5, 7) },
                new BookCopies { CopyID = "C023", BookID = 8, Status = "0", Location = "Shelf B8", DateAdded = new DateTime(2023, 5, 9) },
                new BookCopies { CopyID = "C024", BookID = 8, Status = "-1", Location = "Shelf C8", DateAdded = new DateTime(2023, 5, 11) },

                new BookCopies { CopyID = "C025", BookID = 9, Status = "1", Location = "Shelf A9", DateAdded = new DateTime(2023, 6, 1) },
                new BookCopies { CopyID = "C026", BookID = 9, Status = "0", Location = "Shelf B9", DateAdded = new DateTime(2023, 6, 3) },
                new BookCopies { CopyID = "C027", BookID = 9, Status = "-1", Location = "Shelf C9", DateAdded = new DateTime(2023, 6, 5) },

                new BookCopies { CopyID = "C028", BookID = 10, Status = "1", Location = "Shelf A10", DateAdded = new DateTime(2023, 6, 7) },
                new BookCopies { CopyID = "C029", BookID = 10, Status = "0", Location = "Shelf B10", DateAdded = new DateTime(2023, 6, 9) },
                new BookCopies { CopyID = "C030", BookID = 10, Status = "-1", Location = "Shelf C10", DateAdded = new DateTime(2023, 6, 11) },

                // Book 11–40 (each 3 copies → 30 books * 3 = 90 copies, total now 120)
                new BookCopies { CopyID = "C031", BookID = 11, Status = "1", Location = "Shelf D1", DateAdded = new DateTime(2023, 7, 1) },
                new BookCopies { CopyID = "C032", BookID = 11, Status = "0", Location = "Shelf E1", DateAdded = new DateTime(2023, 7, 3) },
                new BookCopies { CopyID = "C033", BookID = 11, Status = "1", Location = "Shelf F1", DateAdded = new DateTime(2023, 7, 5) },

                new BookCopies { CopyID = "C034", BookID = 12, Status = "1", Location = "Shelf D2", DateAdded = new DateTime(2023, 7, 7) },
                new BookCopies { CopyID = "C035", BookID = 12, Status = "0", Location = "Shelf E2", DateAdded = new DateTime(2023, 7, 9) },
                new BookCopies { CopyID = "C036", BookID = 12, Status = "1", Location = "Shelf F2", DateAdded = new DateTime(2023, 7, 11) },

                new BookCopies { CopyID = "C037", BookID = 13, Status = "-1", Location = "Shelf D3", DateAdded = new DateTime(2023, 7, 13) },
                new BookCopies { CopyID = "C038", BookID = 13, Status = "1", Location = "Shelf E3", DateAdded = new DateTime(2023, 7, 15) },
                new BookCopies { CopyID = "C039", BookID = 13, Status = "0", Location = "Shelf F3", DateAdded = new DateTime(2023, 7, 17) },

                new BookCopies { CopyID = "C040", BookID = 14, Status = "1", Location = "Shelf D4", DateAdded = new DateTime(2023, 7, 19) },
                new BookCopies { CopyID = "C041", BookID = 14, Status = "0", Location = "Shelf E4", DateAdded = new DateTime(2023, 7, 21) },
                new BookCopies { CopyID = "C042", BookID = 14, Status = "-1", Location = "Shelf F4", DateAdded = new DateTime(2023, 7, 23) },

                new BookCopies { CopyID = "C043", BookID = 15, Status = "1", Location = "Shelf D5", DateAdded = new DateTime(2023, 8, 1) },
                new BookCopies { CopyID = "C044", BookID = 15, Status = "0", Location = "Shelf E5", DateAdded = new DateTime(2023, 8, 3) },
                new BookCopies { CopyID = "C045", BookID = 15, Status = "1", Location = "Shelf F5", DateAdded = new DateTime(2023, 8, 5) },

                new BookCopies { CopyID = "C046", BookID = 16, Status = "-1", Location = "Shelf D6", DateAdded = new DateTime(2023, 8, 7) },
                new BookCopies { CopyID = "C047", BookID = 16, Status = "1", Location = "Shelf E6", DateAdded = new DateTime(2023, 8, 9) },
                new BookCopies { CopyID = "C048", BookID = 16, Status = "0", Location = "Shelf F6", DateAdded = new DateTime(2023, 8, 11) },

                new BookCopies { CopyID = "C049", BookID = 17, Status = "1", Location = "Shelf D7", DateAdded = new DateTime(2023, 8, 13) },
                new BookCopies { CopyID = "C050", BookID = 17, Status = "0", Location = "Shelf E7", DateAdded = new DateTime(2023, 8, 15) },
                new BookCopies { CopyID = "C051", BookID = 17, Status = "-1", Location = "Shelf F7", DateAdded = new DateTime(2023, 8, 17) },

                new BookCopies { CopyID = "C052", BookID = 18, Status = "1", Location = "Shelf D8", DateAdded = new DateTime(2023, 8, 19) },
                new BookCopies { CopyID = "C053", BookID = 18, Status = "0", Location = "Shelf E8", DateAdded = new DateTime(2023, 8, 21) },
                new BookCopies { CopyID = "C054", BookID = 18, Status = "1", Location = "Shelf F8", DateAdded = new DateTime(2023, 8, 23) },

                new BookCopies { CopyID = "C055", BookID = 19, Status = "1", Location = "Shelf D9", DateAdded = new DateTime(2023, 9, 1) },
                new BookCopies { CopyID = "C056", BookID = 19, Status = "0", Location = "Shelf E9", DateAdded = new DateTime(2023, 9, 3) },
                new BookCopies { CopyID = "C057", BookID = 19, Status = "-1", Location = "Shelf F9", DateAdded = new DateTime(2023, 9, 5) },

                new BookCopies { CopyID = "C058", BookID = 20, Status = "1", Location = "Shelf D10", DateAdded = new DateTime(2023, 9, 7) },
                new BookCopies { CopyID = "C059", BookID = 20, Status = "0", Location = "Shelf E10", DateAdded = new DateTime(2023, 9, 9) },
                new BookCopies { CopyID = "C060", BookID = 20, Status = "1", Location = "Shelf F10", DateAdded = new DateTime(2023, 9, 11) },

                new BookCopies { CopyID = "C061", BookID = 21, Status = "-1", Location = "Shelf G1", DateAdded = new DateTime(2023, 10, 1) },
                new BookCopies { CopyID = "C062", BookID = 21, Status = "1", Location = "Shelf H1", DateAdded = new DateTime(2023, 10, 3) },
                new BookCopies { CopyID = "C063", BookID = 21, Status = "0", Location = "Shelf I1", DateAdded = new DateTime(2023, 10, 5) },

                new BookCopies { CopyID = "C064", BookID = 22, Status = "1", Location = "Shelf G2", DateAdded = new DateTime(2023, 10, 7) },
                new BookCopies { CopyID = "C065", BookID = 22, Status = "0", Location = "Shelf H2", DateAdded = new DateTime(2023, 10, 9) },
                new BookCopies { CopyID = "C066", BookID = 22, Status = "-1", Location = "Shelf I2", DateAdded = new DateTime(2023, 10, 11) },

                new BookCopies { CopyID = "C067", BookID = 23, Status = "1", Location = "Shelf G3", DateAdded = new DateTime(2023, 10, 13) },
                new BookCopies { CopyID = "C068", BookID = 23, Status = "0", Location = "Shelf H3", DateAdded = new DateTime(2023, 10, 15) },
                new BookCopies { CopyID = "C069", BookID = 23, Status = "1", Location = "Shelf I3", DateAdded = new DateTime(2023, 10, 17) },

                new BookCopies { CopyID = "C070", BookID = 24, Status = "-1", Location = "Shelf G4", DateAdded = new DateTime(2023, 10, 19) },
                new BookCopies { CopyID = "C071", BookID = 24, Status = "1", Location = "Shelf H4", DateAdded = new DateTime(2023, 10, 21) },
                new BookCopies { CopyID = "C072", BookID = 24, Status = "0", Location = "Shelf I4", DateAdded = new DateTime(2023, 10, 23) },

                new BookCopies { CopyID = "C073", BookID = 25, Status = "1", Location = "Shelf G5", DateAdded = new DateTime(2023, 11, 1) },
                new BookCopies { CopyID = "C074", BookID = 25, Status = "0", Location = "Shelf H5", DateAdded = new DateTime(2023, 11, 3) },
                new BookCopies { CopyID = "C075", BookID = 25, Status = "-1", Location = "Shelf I5", DateAdded = new DateTime(2023, 11, 5) },

                new BookCopies { CopyID = "C076", BookID = 26, Status = "1", Location = "Shelf G6", DateAdded = new DateTime(2023, 11, 7) },
                new BookCopies { CopyID = "C077", BookID = 26, Status = "0", Location = "Shelf H6", DateAdded = new DateTime(2023, 11, 9) },
                new BookCopies { CopyID = "C078", BookID = 26, Status = "1", Location = "Shelf I6", DateAdded = new DateTime(2023, 11, 11) },

                new BookCopies { CopyID = "C079", BookID = 27, Status = "-1", Location = "Shelf G7", DateAdded = new DateTime(2023, 11, 13) },
                new BookCopies { CopyID = "C080", BookID = 27, Status = "1", Location = "Shelf H7", DateAdded = new DateTime(2023, 11, 15) },
                new BookCopies { CopyID = "C081", BookID = 27, Status = "0", Location = "Shelf I7", DateAdded = new DateTime(2023, 11, 17) },

                new BookCopies { CopyID = "C082", BookID = 28, Status = "1", Location = "Shelf G8", DateAdded = new DateTime(2023, 11, 19) },
                new BookCopies { CopyID = "C083", BookID = 28, Status = "0", Location = "Shelf H8", DateAdded = new DateTime(2023, 11, 21) },
                new BookCopies { CopyID = "C084", BookID = 28, Status = "-1", Location = "Shelf I8", DateAdded = new DateTime(2023, 11, 23) },

                new BookCopies { CopyID = "C085", BookID = 29, Status = "1", Location = "Shelf G9", DateAdded = new DateTime(2023, 12, 1) },
                new BookCopies { CopyID = "C086", BookID = 29, Status = "0", Location = "Shelf H9", DateAdded = new DateTime(2023, 12, 3) },
                new BookCopies { CopyID = "C087", BookID = 29, Status = "1", Location = "Shelf I9", DateAdded = new DateTime(2023, 12, 5) },

                new BookCopies { CopyID = "C088", BookID = 30, Status = "-1", Location = "Shelf G10", DateAdded = new DateTime(2023, 12, 7) },
                new BookCopies { CopyID = "C089", BookID = 30, Status = "1", Location = "Shelf H10", DateAdded = new DateTime(2023, 12, 9) },
                new BookCopies { CopyID = "C090", BookID = 30, Status = "0", Location = "Shelf I10", DateAdded = new DateTime(2023, 12, 11) },

                new BookCopies { CopyID = "C091", BookID = 31, Status = "1", Location = "Shelf J1", DateAdded = new DateTime(2024, 1, 2) },
                new BookCopies { CopyID = "C092", BookID = 31, Status = "0", Location = "Shelf J1", DateAdded = new DateTime(2024, 1, 4) },
                new BookCopies { CopyID = "C093", BookID = 31, Status = "-1", Location = "Shelf J1", DateAdded = new DateTime(2024, 1, 6) },

                new BookCopies { CopyID = "C094", BookID = 32, Status = "1", Location = "Shelf J2", DateAdded = new DateTime(2024, 1, 8) },
                new BookCopies { CopyID = "C095", BookID = 32, Status = "0", Location = "Shelf J2", DateAdded = new DateTime(2024, 1, 10) },
                new BookCopies { CopyID = "C096", BookID = 32, Status = "-1", Location = "Shelf J2", DateAdded = new DateTime(2024, 1, 12) },

                new BookCopies { CopyID = "C097", BookID = 33, Status = "1", Location = "Shelf J3", DateAdded = new DateTime(2024, 1, 14) },
                new BookCopies { CopyID = "C098", BookID = 33, Status = "0", Location = "Shelf J3", DateAdded = new DateTime(2024, 1, 16) },
                new BookCopies { CopyID = "C099", BookID = 33, Status = "-1", Location = "Shelf J3", DateAdded = new DateTime(2024, 1, 18) },

                new BookCopies { CopyID = "C100", BookID = 34, Status = "1", Location = "Shelf J4", DateAdded = new DateTime(2024, 1, 20) },
                new BookCopies { CopyID = "C101", BookID = 34, Status = "0", Location = "Shelf J4", DateAdded = new DateTime(2024, 1, 22) },
                new BookCopies { CopyID = "C102", BookID = 34, Status = "1", Location = "Shelf J4", DateAdded = new DateTime(2024, 1, 24) },

                new BookCopies { CopyID = "C103", BookID = 35, Status = "-1", Location = "Shelf J5", DateAdded = new DateTime(2024, 2, 1) },
                new BookCopies { CopyID = "C104", BookID = 35, Status = "1", Location = "Shelf J5", DateAdded = new DateTime(2024, 2, 3) },
                new BookCopies { CopyID = "C105", BookID = 35, Status = "0", Location = "Shelf J5", DateAdded = new DateTime(2024, 2, 5) },

                new BookCopies { CopyID = "C106", BookID = 36, Status = "1", Location = "Shelf J6", DateAdded = new DateTime(2024, 2, 7) },
                new BookCopies { CopyID = "C107", BookID = 36, Status = "0", Location = "Shelf J6", DateAdded = new DateTime(2024, 2, 9) },
                new BookCopies { CopyID = "C108", BookID = 36, Status = "-1", Location = "Shelf J6", DateAdded = new DateTime(2024, 2, 11) },

                new BookCopies { CopyID = "C109", BookID = 37, Status = "1", Location = "Shelf J7", DateAdded = new DateTime(2024, 2, 13) },
                new BookCopies { CopyID = "C110", BookID = 37, Status = "0", Location = "Shelf J7", DateAdded = new DateTime(2024, 2, 15) },
                new BookCopies { CopyID = "C111", BookID = 37, Status = "1", Location = "Shelf J7", DateAdded = new DateTime(2024, 2, 17) },

                new BookCopies { CopyID = "C112", BookID = 38, Status = "-1", Location = "Shelf J8", DateAdded = new DateTime(2024, 2, 19) },
                new BookCopies { CopyID = "C113", BookID = 38, Status = "1", Location = "Shelf J8", DateAdded = new DateTime(2024, 2, 21) },
                new BookCopies { CopyID = "C114", BookID = 38, Status = "0", Location = "Shelf J8", DateAdded = new DateTime(2024, 2, 23) },

                new BookCopies { CopyID = "C115", BookID = 39, Status = "1", Location = "Shelf J9", DateAdded = new DateTime(2024, 3, 1) },
                new BookCopies { CopyID = "C116", BookID = 39, Status = "0", Location = "Shelf J9", DateAdded = new DateTime(2024, 3, 3) },
                new BookCopies { CopyID = "C117", BookID = 39, Status = "-1", Location = "Shelf J9", DateAdded = new DateTime(2024, 3, 5) },

                new BookCopies { CopyID = "C118", BookID = 40, Status = "1", Location = "Shelf J10", DateAdded = new DateTime(2024, 3, 7) },
                new BookCopies { CopyID = "C119", BookID = 40, Status = "0", Location = "Shelf J10", DateAdded = new DateTime(2024, 3, 9) },
                new BookCopies { CopyID = "C120", BookID = 40, Status = "1", Location = "Shelf J10", DateAdded = new DateTime(2024, 3, 11) },

                // Books 41–60, 4 copies each: 20 books * 4 = 80 more (total 200)
                new BookCopies { CopyID = "C121", BookID = 41, Status = "1", Location = "Shelf K1", DateAdded = new DateTime(2024, 4, 1) },
                new BookCopies { CopyID = "C122", BookID = 41, Status = "0", Location = "Shelf K1", DateAdded = new DateTime(2024, 4, 3) },
                new BookCopies { CopyID = "C123", BookID = 41, Status = "-1", Location = "Shelf K1", DateAdded = new DateTime(2024, 4, 5) },
                new BookCopies { CopyID = "C124", BookID = 41, Status = "1", Location = "Shelf K1", DateAdded = new DateTime(2024, 4, 7) },

                new BookCopies { CopyID = "C125", BookID = 42, Status = "1", Location = "Shelf K2", DateAdded = new DateTime(2024, 4, 9) },
                new BookCopies { CopyID = "C126", BookID = 42, Status = "0", Location = "Shelf K2", DateAdded = new DateTime(2024, 4, 11) },
                new BookCopies { CopyID = "C127", BookID = 42, Status = "-1", Location = "Shelf K2", DateAdded = new DateTime(2024, 4, 13) },
                new BookCopies { CopyID = "C128", BookID = 42, Status = "1", Location = "Shelf K2", DateAdded = new DateTime(2024, 4, 15) },

                new BookCopies { CopyID = "C129", BookID = 43, Status = "1", Location = "Shelf K3", DateAdded = new DateTime(2024, 4, 17) },
                new BookCopies { CopyID = "C130", BookID = 43, Status = "0", Location = "Shelf K3", DateAdded = new DateTime(2024, 4, 19) },
                new BookCopies { CopyID = "C131", BookID = 43, Status = "-1", Location = "Shelf K3", DateAdded = new DateTime(2024, 4, 21) },
                new BookCopies { CopyID = "C132", BookID = 43, Status = "1", Location = "Shelf K3", DateAdded = new DateTime(2024, 4, 23) },

                new BookCopies { CopyID = "C133", BookID = 44, Status = "1", Location = "Shelf K4", DateAdded = new DateTime(2024, 5, 1) },
                new BookCopies { CopyID = "C134", BookID = 44, Status = "0", Location = "Shelf K4", DateAdded = new DateTime(2024, 5, 3) },
                new BookCopies { CopyID = "C135", BookID = 44, Status = "-1", Location = "Shelf K4", DateAdded = new DateTime(2024, 5, 5) },
                new BookCopies { CopyID = "C136", BookID = 44, Status = "1", Location = "Shelf K4", DateAdded = new DateTime(2024, 5, 7) },

                new BookCopies { CopyID = "C137", BookID = 45, Status = "1", Location = "Shelf K5", DateAdded = new DateTime(2024, 5, 9) },
                new BookCopies { CopyID = "C138", BookID = 45, Status = "0", Location = "Shelf K5", DateAdded = new DateTime(2024, 5, 11) },
                new BookCopies { CopyID = "C139", BookID = 45, Status = "-1", Location = "Shelf K5", DateAdded = new DateTime(2024, 5, 13) },
                new BookCopies { CopyID = "C140", BookID = 45, Status = "1", Location = "Shelf K5", DateAdded = new DateTime(2024, 5, 15) },

                new BookCopies { CopyID = "C141", BookID = 46, Status = "1", Location = "Shelf K6", DateAdded = new DateTime(2024, 5, 17) },
                new BookCopies { CopyID = "C142", BookID = 46, Status = "0", Location = "Shelf K6", DateAdded = new DateTime(2024, 5, 19) },
                new BookCopies { CopyID = "C143", BookID = 46, Status = "-1", Location = "Shelf K6", DateAdded = new DateTime(2024, 5, 21) },
                new BookCopies { CopyID = "C144", BookID = 46, Status = "1", Location = "Shelf K6", DateAdded = new DateTime(2024, 5, 23) },

                new BookCopies { CopyID = "C145", BookID = 47, Status = "1", Location = "Shelf K7", DateAdded = new DateTime(2024, 6, 1) },
                new BookCopies { CopyID = "C146", BookID = 47, Status = "0", Location = "Shelf K7", DateAdded = new DateTime(2024, 6, 3) },
                new BookCopies { CopyID = "C147", BookID = 47, Status = "-1", Location = "Shelf K7", DateAdded = new DateTime(2024, 6, 5) },
                new BookCopies { CopyID = "C148", BookID = 47, Status = "1", Location = "Shelf K7", DateAdded = new DateTime(2024, 6, 7) },

                new BookCopies { CopyID = "C149", BookID = 48, Status = "1", Location = "Shelf K8", DateAdded = new DateTime(2024, 6, 9) },
                new BookCopies { CopyID = "C150", BookID = 48, Status = "0", Location = "Shelf K8", DateAdded = new DateTime(2024, 6, 11) },
                new BookCopies { CopyID = "C151", BookID = 48, Status = "-1", Location = "Shelf K8", DateAdded = new DateTime(2024, 6, 13) },
                new BookCopies { CopyID = "C152", BookID = 48, Status = "1", Location = "Shelf K8", DateAdded = new DateTime(2024, 6, 15) },

                new BookCopies { CopyID = "C153", BookID = 49, Status = "1", Location = "Shelf K9", DateAdded = new DateTime(2024, 6, 17) },
                new BookCopies { CopyID = "C154", BookID = 49, Status = "0", Location = "Shelf K9", DateAdded = new DateTime(2024, 6, 19) },
                new BookCopies { CopyID = "C155", BookID = 49, Status = "-1", Location = "Shelf K9", DateAdded = new DateTime(2024, 6, 21) },
                new BookCopies { CopyID = "C156", BookID = 49, Status = "1", Location = "Shelf K9", DateAdded = new DateTime(2024, 6, 23) },

                new BookCopies { CopyID = "C157", BookID = 50, Status = "1", Location = "Shelf K10", DateAdded = new DateTime(2024, 7, 1) },
                new BookCopies { CopyID = "C158", BookID = 50, Status = "0", Location = "Shelf K10", DateAdded = new DateTime(2024, 7, 3) },
                new BookCopies { CopyID = "C159", BookID = 50, Status = "-1", Location = "Shelf K10", DateAdded = new DateTime(2024, 7, 5) },
                new BookCopies { CopyID = "C160", BookID = 50, Status = "1", Location = "Shelf K10", DateAdded = new DateTime(2024, 7, 7) },

                new BookCopies { CopyID = "C161", BookID = 51, Status = "1", Location = "Shelf L1", DateAdded = new DateTime(2024, 7, 9) },
                new BookCopies { CopyID = "C162", BookID = 51, Status = "0", Location = "Shelf L1", DateAdded = new DateTime(2024, 7, 11) },
                new BookCopies { CopyID = "C163", BookID = 51, Status = "-1", Location = "Shelf L1", DateAdded = new DateTime(2024, 7, 13) },
                new BookCopies { CopyID = "C164", BookID = 51, Status = "1", Location = "Shelf L1", DateAdded = new DateTime(2024, 7, 15) },

                new BookCopies { CopyID = "C165", BookID = 52, Status = "1", Location = "Shelf L2", DateAdded = new DateTime(2024, 7, 17) },
                new BookCopies { CopyID = "C166", BookID = 52, Status = "0", Location = "Shelf L2", DateAdded = new DateTime(2024, 7, 19) },
                new BookCopies { CopyID = "C167", BookID = 52, Status = "-1", Location = "Shelf L2", DateAdded = new DateTime(2024, 7, 21) },
                new BookCopies { CopyID = "C168", BookID = 52, Status = "1", Location = "Shelf L2", DateAdded = new DateTime(2024, 7, 23) },

                new BookCopies { CopyID = "C169", BookID = 53, Status = "1", Location = "Shelf L3", DateAdded = new DateTime(2024, 8, 1) },
                new BookCopies { CopyID = "C170", BookID = 53, Status = "0", Location = "Shelf L3", DateAdded = new DateTime(2024, 8, 3) },
                new BookCopies { CopyID = "C171", BookID = 53, Status = "-1", Location = "Shelf L3", DateAdded = new DateTime(2024, 8, 5) },
                new BookCopies { CopyID = "C172", BookID = 53, Status = "1", Location = "Shelf L3", DateAdded = new DateTime(2024, 8, 7) },

                new BookCopies { CopyID = "C173", BookID = 54, Status = "1", Location = "Shelf L4", DateAdded = new DateTime(2024, 8, 9) },
                new BookCopies { CopyID = "C174", BookID = 54, Status = "0", Location = "Shelf L4", DateAdded = new DateTime(2024, 8, 11) },
                new BookCopies { CopyID = "C175", BookID = 54, Status = "-1", Location = "Shelf L4", DateAdded = new DateTime(2024, 8, 13) },
                new BookCopies { CopyID = "C176", BookID = 54, Status = "1", Location = "Shelf L4", DateAdded = new DateTime(2024, 8, 15) },

                new BookCopies { CopyID = "C177", BookID = 55, Status = "1", Location = "Shelf L5", DateAdded = new DateTime(2024, 8, 17) },
                new BookCopies { CopyID = "C178", BookID = 55, Status = "0", Location = "Shelf L5", DateAdded = new DateTime(2024, 8, 19) },
                new BookCopies { CopyID = "C179", BookID = 55, Status = "-1", Location = "Shelf L5", DateAdded = new DateTime(2024, 8, 21) },
                new BookCopies { CopyID = "C180", BookID = 55, Status = "1", Location = "Shelf L5", DateAdded = new DateTime(2024, 8, 23) },

                new BookCopies { CopyID = "C181", BookID = 56, Status = "1", Location = "Shelf L6", DateAdded = new DateTime(2024, 9, 1) },
                new BookCopies { CopyID = "C182", BookID = 56, Status = "0", Location = "Shelf L6", DateAdded = new DateTime(2024, 9, 3) },
                new BookCopies { CopyID = "C183", BookID = 56, Status = "-1", Location = "Shelf L6", DateAdded = new DateTime(2024, 9, 5) },
                new BookCopies { CopyID = "C184", BookID = 56, Status = "1", Location = "Shelf L6", DateAdded = new DateTime(2024, 9, 7) },

                new BookCopies { CopyID = "C185", BookID = 57, Status = "1", Location = "Shelf L7", DateAdded = new DateTime(2024, 9, 9) },
                new BookCopies { CopyID = "C186", BookID = 57, Status = "0", Location = "Shelf L7", DateAdded = new DateTime(2024, 9, 11) },
                new BookCopies { CopyID = "C187", BookID = 57, Status = "-1", Location = "Shelf L7", DateAdded = new DateTime(2024, 9, 13) },
                new BookCopies { CopyID = "C188", BookID = 57, Status = "1", Location = "Shelf L7", DateAdded = new DateTime(2024, 9, 15) },

                new BookCopies { CopyID = "C189", BookID = 58, Status = "1", Location = "Shelf L8", DateAdded = new DateTime(2024, 9, 17) },
                new BookCopies { CopyID = "C190", BookID = 58, Status = "0", Location = "Shelf L8", DateAdded = new DateTime(2024, 9, 19) },
                new BookCopies { CopyID = "C191", BookID = 58, Status = "-1", Location = "Shelf L8", DateAdded = new DateTime(2024, 9, 21) },
                new BookCopies { CopyID = "C192", BookID = 58, Status = "1", Location = "Shelf L8", DateAdded = new DateTime(2024, 9, 23) },

                new BookCopies { CopyID = "C193", BookID = 59, Status = "1", Location = "Shelf L9", DateAdded = new DateTime(2024, 10, 1) },
                new BookCopies { CopyID = "C194", BookID = 59, Status = "0", Location = "Shelf L9", DateAdded = new DateTime(2024, 10, 3) },
                new BookCopies { CopyID = "C195", BookID = 59, Status = "-1", Location = "Shelf L9", DateAdded = new DateTime(2024, 10, 5) },
                new BookCopies { CopyID = "C196", BookID = 59, Status = "1", Location = "Shelf L9", DateAdded = new DateTime(2024, 10, 7) },

                new BookCopies { CopyID = "C197", BookID = 60, Status = "1", Location = "Shelf L10", DateAdded = new DateTime(2024, 10, 9) },
                new BookCopies { CopyID = "C198", BookID = 60, Status = "0", Location = "Shelf L10", DateAdded = new DateTime(2024, 10, 11) },
                new BookCopies { CopyID = "C199", BookID = 60, Status = "-1", Location = "Shelf L10", DateAdded = new DateTime(2024, 10, 13) },
                new BookCopies { CopyID = "C200", BookID = 60, Status = "1", Location = "Shelf L10", DateAdded = new DateTime(2024, 10, 15) }
            );
        }
    }
}
