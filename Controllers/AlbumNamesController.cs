using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiMarketWebApp.Models;
using DigiMarketWebApp.Data;
using DigiMarketWebApp.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DigiMarketWebApp.Controllers
{
    [Authorize]
    public class AlbumNamesController : Controller
    {
        private readonly DigiMarketDbContext _context;

        public AlbumNamesController(DigiMarketDbContext context)
        {
            _context = context;
        }

        // GET: AlbumNames
        public async Task<IActionResult> Index()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            string userId = user.Id;
            var profileContext = _context.AlbumNames.Include(a => a.WebAppUser).Where(u => u.WebAppUser.Id == userId);
            return View(await profileContext.ToListAsync());
        }

        // GET: AlbumNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumName = await _context.AlbumNames
                .Include(a => a.WebAppUser)
                .FirstOrDefaultAsync(m => m.AlbumNameID == id);
            if (albumName == null)
            {
                return NotFound();
            }

            return View(albumName);
        }

        // GET: AlbumNames/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id");
            return View();
        }

        // POST: AlbumNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumNameID,Name,Id")] AlbumName albumName)
        {
            if (ModelState.IsValid)
            {
                // Set User id for album
                string email = User.FindFirstValue(ClaimTypes.Email);
                var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
                string userId = user.Id;
                albumName.Id = userId;

                _context.Add(albumName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", albumName.Id);
            return View(albumName);
        }

        // GET: AlbumNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumName = await _context.AlbumNames.FindAsync(id);
            if (albumName == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", albumName.Id);
            return View(albumName);
        }

        // POST: AlbumNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumNameID,Name,Id")] AlbumName albumName)
        {
            if (id != albumName.AlbumNameID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string email = User.FindFirstValue(ClaimTypes.Email);
                    var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
                    string userId = user.Id;
                    albumName.Id = userId;
                    _context.Update(albumName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumNameExists(albumName.AlbumNameID))
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
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", albumName.Id);
            return RedirectToAction(nameof(Index));
        }

        // GET: AlbumNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumName = await _context.AlbumNames
                .Include(a => a.WebAppUser)
                .FirstOrDefaultAsync(m => m.AlbumNameID == id);
            if (albumName == null)
            {
                return NotFound();
            }

            return View(albumName);
        }

        // POST: AlbumNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumName = await _context.AlbumNames.FindAsync(id);
            _context.AlbumNames.Remove(albumName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumNameExists(int id)
        {
            return _context.AlbumNames.Any(e => e.AlbumNameID == id);
        }
    }
}
