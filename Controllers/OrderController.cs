using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1NotGuid.Data;
using WebApplication1NotGuid.Models;

namespace WebApplication1NotGuid.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders.Include(o => o.User).Include(o => o.MenuItem);
            return View(await orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.Include(o => o.User).Include(o => o.MenuItem).FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users.Where(dbRow => dbRow.Role == "Customer"), "UserID", "Username");
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "MenuItemID", "Name");
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserID,MenuItemID,Quantity")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users.Where(dbRow => dbRow.Role == "Customer"), "UserID", "Username", order.UserID);
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "MenuItemID", "Name", order.MenuItemID);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users.Where(dbRow => dbRow.Role == "Customer"), "UserID", "Username", order.UserID);
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "MenuItemID", "Name", order.MenuItemID);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserID,MenuItemID,Quantity")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users.Where(dbRow => dbRow.Role == "Customer"), "UserID", "Username", order.UserID);
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "MenuItemID", "Name", order.MenuItemID);
            return View(order);
        }

        

        //get: Order/Delete/5
      
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }

}
