using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiMarketWebApp.Models;
using DigiMarketWebApp.Areas.Identity.Data;
using DigiMarketWebApp.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DigiMarketWebApp.Controllers
{
    [Authorize]
    public class UserAccessesController : Controller
    {
        private readonly DigiMarketDbContext _context;

        public UserAccessesController(DigiMarketDbContext context)
        {
            _context = context;
        }

        // GET: UserAccesses
        public async Task<IActionResult> Index(int id)
        {
            if (0 != id)
            {
                HttpContext.Session.SetInt32("photoAccess", id);
            }
            if (0 == id)
            {
                id = (int)HttpContext.Session.GetInt32("photoAccess");
            }
            var profileContext = _context.UserAccesses.Include(u => u.Photo).Include(u => u.WebAppUser).Where(p => p.PhotoID == id);
            return View(await profileContext.ToListAsync());
        }

        // GET: UserAccesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccess = await _context.UserAccesses
                .Include(u => u.Photo)
                .Include(u => u.WebAppUser)
                .FirstOrDefaultAsync(m => m.UserAccessID == id);
            if (userAccess == null)
            {
                return NotFound();
            }

            return View(userAccess);
        }

        // GET: UserAccesses/Create
        public IActionResult Create()
        {
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID");
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id");
            return View();
        }

        // POST: UserAccesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserAccessID,UserEmail,PhotoID,Id")] UserAccess userAccess)
        {
            
            if (ModelState.IsValid)
            {
                int photoId = (int)HttpContext.Session.GetInt32("photoAccess");
                // Set user id
                var user = _context.Users.Where(u => u.Email == userAccess.UserEmail).SingleOrDefault();
                userAccess.Id = user.Id;

                // Set photo id
                userAccess.PhotoID = photoId;

                _context.Add(userAccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", userAccess.PhotoID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", userAccess.Id);
            return View(userAccess);
        }

        // GET: UserAccesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccess = await _context.UserAccesses.FindAsync(id);
            if (userAccess == null)
            {
                return NotFound();
            }
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", userAccess.PhotoID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", userAccess.Id);
            return View(userAccess);
        }

        // POST: UserAccesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAccessID,UserEmail,PhotoID,Id")] UserAccess userAccess)
        {
            if (id != userAccess.UserAccessID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccessExists(userAccess.UserAccessID))
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
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", userAccess.PhotoID);
            ViewData["Id"] = new SelectList(_context.Set<WebAppUser>(), "Id", "Id", userAccess.Id);
            return View(userAccess);
        }

        // GET: UserAccesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccess = await _context.UserAccesses
                .Include(u => u.Photo)
                .Include(u => u.WebAppUser)
                .FirstOrDefaultAsync(m => m.UserAccessID == id);
            if (userAccess == null)
            {
                return NotFound();
            }

            return View(userAccess);
        }

        // POST: UserAccesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccess = await _context.UserAccesses.FindAsync(id);
            _context.UserAccesses.Remove(userAccess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Shared Photos
        public async Task<IActionResult> Shared()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var sharedPhoto = _context.UserAccesses.Include(u => u.Photo).Include(u => u.WebAppUser).Where(p => p.UserEmail == email);
            return View(await sharedPhoto.ToListAsync());
        }

        private bool UserAccessExists(int id)
        {
            return _context.UserAccesses.Any(e => e.UserAccessID == id);
        }
    }
}
