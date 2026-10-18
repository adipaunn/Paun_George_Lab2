using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paun_George_Lab2.Models;

namespace Paun_George_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Paun_George_Lab2Context _context;

    public IndexModel(Paun_George_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Book = await _context.Book.Include(b => b.Author).Include(b => b.Publisher).ToListAsync();
    }
}
