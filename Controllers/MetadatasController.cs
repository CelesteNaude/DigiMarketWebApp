using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiMarketWebApp.Models;
using DigiMarketWebApp.Data;
using Microsoft.AspNetCore.Routing;

namespace DigiMarketWebApp.Controllers
{
    public class MetadatasController : Controller
    {
        private readonly DigiMarketDbContext _context;

        public MetadatasController(DigiMarketDbContext context)
        {
            _context = context;
        }


        // GET: Metadatas
        public async Task<IActionResult> Index(int id)
        {
            // Get only metadata related to the photo
            var profileContext = _context.Metadatas.Include(m => m.Photo).Where(p => p.PhotoID == id);
            TempData["photoId"] = id;
            return View(await profileContext.ToListAsync());
        }

        // GET: Metadatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var metadata = await _context.Metadatas
                .Include(m => m.Photo)
                .FirstOrDefaultAsync(m => m.MetadataID == id);
            if (metadata == null)
            {
                return NotFound();
            }


            return View(metadata);
        }

        // GET: Metadatas/Create
        public IActionResult Create()
        {
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID");
            return View();
        }

        // POST: Metadatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetadataID,Longtitude,Latitude,Tag,Date,Owner,PhotoID")] Metadata metadata)
        {
            int photoId = (int)TempData["photoId"];
            if (ModelState.IsValid)
            {
                metadata.PhotoID = photoId;

                _context.Add(metadata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            TempData["createId"] = photoId;
            //ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", metadata.PhotoID);
            return LocalRedirect("~/Metadatas/Index");
            //return View(metadata);
        }

        // GET: Metadatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metadata = await _context.Metadatas.FindAsync(id);
            if (metadata == null)
            {
                return NotFound();
            }
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", metadata.PhotoID);
            return View(metadata);
        }

        // POST: Metadatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetadataID,Longtitude,Latitude,Tag,Date,Owner,PhotoID")] Metadata metadata)
        {
            if (id != metadata.MetadataID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metadata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetadataExists(metadata.MetadataID))
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
            ViewData["PhotoID"] = new SelectList(_context.Photos, "PhotoID", "PhotoID", metadata.PhotoID);
            return View(metadata);
        }

        // GET: Metadatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metadata = await _context.Metadatas
                .Include(m => m.Photo)
                .FirstOrDefaultAsync(m => m.MetadataID == id);
            if (metadata == null)
            {
                return NotFound();
            }

            return View(metadata);
        }

        // POST: Metadatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metadata = await _context.Metadatas.FindAsync(id);
            _context.Metadatas.Remove(metadata);
            await _context.SaveChangesAsync();
            return Redirect("https://localhost:44348/Photos");
            //return RedirectToAction(nameof(Index));
        }

        private bool MetadataExists(int id)
        {
            return _context.Metadatas.Any(e => e.MetadataID == id);
        }
    }
}
