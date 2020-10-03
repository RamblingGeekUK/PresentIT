using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentIT.Models;
using PresentIT.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PresentIT.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly PITContext _context;
        private readonly UserService _userservice;

        public CandidatesController(PITContext context, UserService userService)
        {
            _context = context;
            _userservice = userService;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            string UserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (User.IsInRole("admin"))
            {
                return View(await _context.Candidate.ToListAsync());
            }

            if (await _userservice.UserExistsAsync(UserID))
            {
                var id = await _userservice.GetUserIDAsync(UserID);
                return RedirectToAction("Details", "Candidates", new { id });
            }
            else
            {
                return RedirectToAction("Create", "Candidates");
            }
        }


        public IActionResult Thanks()
        {
            return View();
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Auth0,Firstname,Surname,Created,Accepted,VideoURL")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                candidate.Id = Guid.NewGuid();
                candidate.Auth0 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                _context.Add(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Thanks));
            }
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }

        // Check if Auth0 User Already Exits
        // GET: 
        public async Task<IActionResult> UserExists(string Auth0)
        {
            if (Auth0 == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate.FindAsync(Auth0);
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }


        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Auth0,Firstname,Surname,Created,Accepted,VideoURL")] Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.Id))
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
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var candidate = await _context.Candidate.FindAsync(id);
            _context.Candidate.Remove(candidate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(Guid id)
        {
            return _context.Candidate.Any(e => e.Id == id);
        }
    }
}