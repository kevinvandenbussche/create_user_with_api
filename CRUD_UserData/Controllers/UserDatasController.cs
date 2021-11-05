using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_UserData.Data;
using CRUD_UserData.Models;
using CRUD_UserData.Service;
using Microsoft.AspNetCore.Authorization;


namespace CRUD_UserData.Controllers
{
    public class UserDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDatasController(ApplicationDbContext context)
        {
            _context = context;
        }
/*        [Authorize]*/
        // GET: UserDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserData.ToListAsync());
        }

        // GET: UserDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // GET: UserDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<List<Repository>> ResponseApi()
        {
            UserWithApi test = new UserWithApi();
            var datas = await UserWithApi.ProcessRepositories();
            Console.WriteLine(datas);
            return datas;
            
        }

        //GET: UserDatas/UserFromAPI
        public async Task<IActionResult> UserFromAPI()
        {
            List<Repository> datas =  await this.ResponseApi();
            
            foreach (var repo in datas)
            {
                var userData = new UserData {Name = repo.Name };
                _context.Add<UserData>(userData);
                _context.SaveChanges();
            }


            return View(_context.UserData);
        }
        

        // GET: UserDatas/FormSearchUserData
        public async Task<IActionResult> FormSearchUserData()
        {
            return View();
        }

        //regarder comment faire la recherche dans la base de donnée
        // post: UserDatas/ShowSearchResultsUserData
        public async Task<IActionResult> ShowSearchResultsUserData(string ResultSearch)
        {
            //regarder pour faire des requetes personnalisé
            return View("Index", await _context.UserData.Where(u => u.Name.Contains(ResultSearch)).ToListAsync());
        }


        // POST: UserDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FirstName")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userData);
        }

        // GET: UserDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }
            return View(userData);
        }

        // POST: UserDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FirstName")] UserData userData)
        {
            if (id != userData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.Id))
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
            return View(userData);
        }

        // GET: UserDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: UserDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userData = await _context.UserData.FindAsync(id);
            _context.UserData.Remove(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserFromAPI));
        }

        private bool UserDataExists(int id)
        {
            return _context.UserData.Any(e => e.Id == id);
        }
    }
}
