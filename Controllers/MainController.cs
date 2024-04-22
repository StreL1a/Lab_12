using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MainController : Controller
{
    AppContext database;

    public MainController(AppContext context)
    {
        database = context;
    }

    public async Task<IActionResult> Index()
    {
        if (database.Users.ToList().Count == 0)
        {
            database.Users.Add(new User
            {
                Name = "Ivan",
                Surname = "Strelchenko",
                Age = 20
            });
            database.Users.Add(new User
            {
                Name = "Mike",
                Surname = "Taison",
                Age = 65
            });
        }

        if (database.Companies.ToList().Count == 0)
        {
            database.Companies.Add(new Company
            {
                Name = "АТБ",
                Revenue = 250000
            });
            database.Companies.Add(new Company
            {
                Name = "Нова Пошта",
                Revenue = 500000
            });
            database.Companies.Add(new Company
            {
                Name = "Сільпо",
                Revenue = 43000000
            });
            database.Companies.Add(new Company
            {
                Name = "Пепсіко",
                Revenue = 23000000
            });
            database.Companies.Add(new Company
            {
                Name = "Лімо",
                Revenue = 8000000
            });
        }

        await database.SaveChangesAsync();
        return View(new IndexModel
        {
            Users = await database.Users.ToListAsync(),
            Companies = await database.Companies.ToListAsync()
        });
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        Console.WriteLine(user.Name + " " + user.Name + " " + user.Age);
        database.Users.Add(user);

        await database.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            User? user = await database.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (user != null) return View(user);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        database.Entry(user).State = EntityState.Modified;

        try
        {
            await database.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            User? user = await database.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user != null)
            {
                database.Users.Remove(user);
                await database.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        return NotFound();
    }
}