using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Customer> Customers { get; private set; }

        public async Task OnGetAsync()
        {
            Customers = await _context.Customers
                .AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
