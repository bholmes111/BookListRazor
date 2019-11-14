﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
            //Book = _db.Book.Where(b => b.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bookFromDb = await _db.Book.FindAsync(Book.Id);
            bookFromDb.Name = Book.Name;
            bookFromDb.Author = Book.Author;
            bookFromDb.ISBN = Book.ISBN;

            await _db.SaveChangesAsync();
            Message = "Book has been updated successfully.";
            return RedirectToPage("Index");
        }
    }
}