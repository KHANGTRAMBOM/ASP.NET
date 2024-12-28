using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnASP.Data;
using DoAnASP.Models;

namespace DoAnASP.Controllers
{
    public class FollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Follows.Include(f => f.Followed).Include(f => f.Follower);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Follows/Details/5/10 (FollowerID và FollowedID)
        public async Task<IActionResult> Details(string followerId, string followedId)
        {
            if (followerId == null || followedId == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .Include(f => f.Follower)
                .Include(f => f.Followed)
                .FirstOrDefaultAsync(m => m.FollowerID == followerId && m.FollowedID == followedId);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }


        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["FollowedID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName");
            ViewData["FollowerID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FollowerID,FollowedID,CreatedAt")] Follow follow)
        {
            ModelState.Remove("Follower");
            ModelState.Remove("Followed");

            if (ModelState.IsValid)
            {
                _context.Add(follow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FollowedID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", follow.FollowedID);
            ViewData["FollowerID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", follow.FollowerID);
            return View(follow);
        }

        // GET: Follows/Edit/5/10 (FollowerID và FollowedID)
        public async Task<IActionResult> Edit(string followerId, string followedId)
        {
            if (followerId == null || followedId == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerID == followerId && f.FollowedID == followedId);
            if (follow == null)
            {
                return NotFound();
            }

            ViewData["FollowedID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", follow.FollowedID);
            ViewData["FollowerID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", follow.FollowerID);
            return View(follow);
        }

        // POST: Follows/Edit/5/10 (FollowerID và FollowedID)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string followerId, string followedId, [Bind("FollowerID,FollowedID,CreatedAt")] Follow follow)
        {
            ModelState.Remove("Follower");
            ModelState.Remove("Followed");

            if (followerId != follow.FollowerID || followedId != follow.FollowedID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(follow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(follow.FollowerID, follow.FollowedID))
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

            ViewData["FollowedID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", follow.FollowedID);
            ViewData["FollowerID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", follow.FollowerID);
            return View(follow);
        }


        // GET: Follows/Delete/5/10 (FollowerID và FollowedID)
        public async Task<IActionResult> Delete(string followerId, string followedId)
        {
            if (followerId == null || followedId == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .Include(f => f.Follower)
                .Include(f => f.Followed)
                .FirstOrDefaultAsync(m => m.FollowerID == followerId && m.FollowedID == followedId);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // POST: Follows/Delete/5/10 (FollowerID và FollowedID)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string followerId, string followedId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerID == followerId && f.FollowedID == followedId);
            if (follow != null)
            {
                _context.Follows.Remove(follow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FollowExists(string followerId, string followedId)
        {
            return _context.Follows.Any(e => e.FollowerID == followerId && e.FollowedID == followedId);
        }

    }
}
