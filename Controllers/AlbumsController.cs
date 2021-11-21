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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DigiMarketWebApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly DigiMarketDbContext _context;

        public AlbumsController(DigiMarketDbContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index(int id)
        {
            if (0 != id)
            {
                HttpContext.Session.SetInt32("photoAlbum", id);
            }
            if (0 == id)
            {
                id = (int)HttpContext.Session.GetInt32("photoAlbum");
            }
            var profileContext = _context.Albums.Include(a => a.AlbumName).Include(a => a.Photo).Include(a => a.WebAppUser).Where(p => p.PhotoID == id);
            return View(await profileContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.AlbumName)
                .Include(a => a.Photo)
                .Include(a => a.WebAppUser)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            string userId = user.Id;
            ViewData["AlbumNameId"] = new SelectList(_context.AlbumNames.Where(u => u.WebAppUser.Id == userId), "AlbumNameID", "Name");
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID");
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumID,AlbumNameId,PhotoID,Id")] Album album)
        {
            if (ModelState.IsValid)
            {
                // Set photo id
                int photoId = (int)HttpContext.Session.GetInt32("photoAlbum");
                album.PhotoID = photoId;

                // Set user id
                string email = User.FindFirstValue(ClaimTypes.Email);
                var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
                string userId = user.Id;
                album.Id = userId;

                // Insert record
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumNameId"] = new SelectList(_context.AlbumNames, "AlbumNameID", "Name", album.AlbumNameId);
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", album.PhotoID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", album.Id);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            string userId = user.Id;
            ViewData["AlbumNameId"] = new SelectList(_context.AlbumNames.Where(u => u.WebAppUser.Id == userId), "AlbumNameID", "Name");
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", album.PhotoID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", album.Id);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,AlbumNameId,PhotoID,Id")] Album album)
        {
            if (id != album.AlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
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
            ViewData["AlbumNameId"] = new SelectList(_context.AlbumNames, "AlbumNameID", "Name", album.AlbumNameId);
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", album.PhotoID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", album.Id);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.AlbumName)
                .Include(a => a.Photo)
                .Include(a => a.WebAppUser)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: View Album
        public async Task<IActionResult> AlbumIndex(int id)
        {
            var profileContext = _context.Albums.Include(a => a.AlbumName).Include(a => a.Photo).Include(a => a.WebAppUser).Where(p => p.AlbumNameId == id);
            return View(await profileContext.ToListAsync());
        }

        // GET: Shared Albums
        public async Task<IActionResult> Shared(int id)
        {
            var sharedAlbum = _context.Albums.Include(a => a.AlbumName).Include(a => a.Photo).Include(a => a.WebAppUser).Where(p => p.AlbumNameId == id);
            return View(await sharedAlbum.ToListAsync());
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumID == id);
        }
    }
}
