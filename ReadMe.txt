

1) Start File >  
public void ConfigureServices(IServiceCollection services)
{
	services.AddDbContext<AppDbContext>(options =>
		options.UseInMemoryDatabase("name")
);

2) AppDbContext >
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}

3) Customer >
public class Customer
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }
}

4) Create Razor Page "Create" in Page folder >

4) Add Form to "Create" Razor Page >

<form method="post" >
    <div asp-validation-summary="All"></div>
    <div>Name: <input asp-for="Customer.Name" /></div>
	<span asp-validation-for="Customer.Name"/>
    <input type="submit" />
</form>

6) Bind to a Customer > DI AppDbContext and bind >

private readonly AppDbContext _context;

public CreateModel(AppDbContext context)
{
    _context = context;
}

[BindProperty]
public Customer Customer { get; set; }

public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid){return Page();}

    _context.Customers.Add(Customer);
    await _context.SaveChangesAsync();
    return RedirectToPage("/Index");
}

Create DONE!
**********************************************
Start List Customers on Index Page
**********************************************

