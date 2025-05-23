using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index(string SearchString)
        {
            // This method is responsible for displaying the list of users in the Index view.
            // It retrieves all users from the userlist and passes them to the view.
            // If a search string is provided, it filters the users based on the search criteria.
            // The filtered list is then passed to the view for display.
            // The SearchString parameter is used to filter the users based on their names.

           var users = userlist.AsQueryable();

    if (!String.IsNullOrEmpty(SearchString))
    {
        users = users.Where(u => !string.IsNullOrEmpty(u.Name) && u.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
        ViewBag.SearchString = SearchString;
    }
    else
    {
        ViewBag.SearchString = "";
    }

    return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
            // Assign a new Id (auto-increment)
            user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
             // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
        
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
           }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            if (ModelState.IsValid)
            {
                var existingUser = userlist.FirstOrDefault(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                
                // Add other properties as needed
                return RedirectToAction(nameof(Index));
            }
            return View(user);
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
            return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
            return NotFound();
            }
            userlist.Remove(user);
            return RedirectToAction(nameof(Index));
        }

    // GET: User/Search
    public ActionResult Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return View(userlist);
        }
        var results = userlist
            .Where(u => (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(query, StringComparison.OrdinalIgnoreCase)))
            .ToList();
        return View(results);
    }
}