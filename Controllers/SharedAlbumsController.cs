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
using Microsoft.AspNetCore.Http;

namespace DigiMarketWebApp.Controllers
{
    public class SharedAlbumsController : Controller
    {
        private readonly DigiMarketDbContext _context;

        public SharedAlbumsController(DigiMarketDbContext context)
        {
            _context = context;
        }

        // GET: SharedAlbums
        public async Task<IActionResult> Index(int id)
        {
            if (0 != id)
            {
                HttpContext.Session.SetInt32("albumAccess", id);
            }
            if (0 == id)
            {
                id = (int)HttpContext.Session.GetInt32("albumAccess");
            }
            var profileContext = _context.SharedAlbums.Include(s => s.AlbumName).Include(s => s.WebAppUser).Where(p => p.AlbumNameID == id);
            return View(await profileContext.ToListAsync());
        }

        // GET: SharedAlbums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedAlbum = await _context.SharedAlbums
                .Include(s => s.AlbumName)
                .Include(s => s.WebAppUser)
                .FirstOrDefaultAsync(m => m.SharedAlbumID == id);
            if (sharedAlbum == null)
            {
                return NotFound();
            }

            return View(sharedAlbum);
        }

        // GET: SharedAlbums/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumID", "AlbumID");
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID");
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id");
            return View();
        }

        // POST: SharedAlbums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SharedAlbumID,UserEmail,AlbumNameId,Id")] SharedAlbum sharedAlbum)
        {
            if (ModelState.IsValid)
            {
                // Set album id
                int albumId = (int)HttpContext.Session.GetInt32("albumAccess");
                sharedAlbum.AlbumNameID = albumId;

                // Set user id
                string email = User.FindFirstValue(ClaimTypes.Email);
                var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
                string userId = user.Id;
                sharedAlbum.Id = userId;

                // Set record
                _context.Add(sharedAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumID", "AlbumID", sharedAlbum.AlbumNameID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", sharedAlbum.Id);
            return View(sharedAlbum);
        }

        // GET: SharedAlbums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedAlbum = await _context.SharedAlbums.FindAsync(id);
            if (sharedAlbum == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumID", "AlbumID", sharedAlbum.AlbumNameID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", sharedAlbum.Id);
            return View(sharedAlbum);
        }

        // POST: SharedAlbums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SharedAlbumID,UserEmail,AlbumId,PhotoID,Id")] SharedAlbum sharedAlbum)
        {
            if (id != sharedAlbum.SharedAlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharedAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharedAlbumExists(sharedAlbum.SharedAlbumID))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumID", "AlbumID", sharedAlbum.AlbumNameID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", sharedAlbum.Id);
            return View(sharedAlbum);
        }

        // GET: SharedAlbums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedAlbum = await _context.SharedAlbums
                .Include(s => s.AlbumNameID)
                .Include(s => s.WebAppUser)
                .FirstOrDefaultAsync(m => m.SharedAlbumID == id);
            if (sharedAlbum == null)
            {
                return NotFound();
            }

            return View(sharedAlbum);
        }

        // POST: SharedAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sharedAlbum = await _context.SharedAlbums.FindAsync(id);
            _context.SharedAlbums.Remove(sharedAlbum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Shared Albums
        public async Task<IActionResult> Shared()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var sharedAlbum = _context.SharedAlbums.Include(s => s.AlbumName).Include(s => s.WebAppUser).Where(p => p.UserEmail == email);
            return View(await sharedAlbum.ToListAsync());
        }

        private bool SharedAlbumExists(int id)
        {
            return _context.SharedAlbums.Any(e => e.SharedAlbumID == id);
        }
    }
}
