using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http; 
using System.Net.Http.Formatting; 
using Newtonsoft.Json;
using System.Web; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using EONAssignmentProj.Models; 


namespace EONAssignmentProj.Controllers
{
    public class UserController : Controller
    {
        private readonly EONAssignmentDBContext _context;

        public UserController(EONAssignmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {            
            var users = await _context.UserTbls.ToListAsync();
             
            return View(users);
        }
        
        private static List<UserTbl> UserList()
        {
            List<UserTbl> users = new List<UserTbl>();
            string apiUrl = "https://localhost:44323/api/UserListapi/GetUsers";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/GetUsers")).Result;
            if (response.IsSuccessStatusCode)
            {
                users = JsonConvert.DeserializeObject<List<UserTbl>>(response.Content.ReadAsStringAsync().Result);
            }

            return users;
        }         
 
        public async System.Threading.Tasks.Task<ActionResult> ConsumeExternalAPI()
        {

            string strurl = string.Format("https://betaapi.eon-xr.com//External/GetDemoLibraryCategories");

            WebRequest requestObjPost = WebRequest.Create(strurl);
            requestObjPost.Credentials = CredentialCache.DefaultCredentials;
            requestObjPost.ContentType = "application/json";
            requestObjPost.Method = "POST";


            string json = "{\"avrDemoId\":2}";
            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)requestObjPost.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }

            return View();

            //string url = "https://betaapi.eon-xr.com//External/GetDemoLibraryCategories";

            //using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            //{
            //    client.BaseAddress = new Uri(url);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    //int demoID = 2;
            //    var requestBody = new { avrDemoId = 2 };

            //    System.Net.Http.HttpResponseMessage apiResponse = await client.PostAsJsonAsync(url, requestBody.avrDemoId);
            //    if (apiResponse.IsSuccessStatusCode)
            //    {
            //        var documentResponse = await apiResponse.Content.ReadAsStringAsync();
            //        dynamic response = JsonConvert.DeserializeObject(documentResponse);

            //        //var data = await response.Content.ReadAsStringAsync();
            //        //var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

            //        //System.Web.UI.WebControls.dro gView = new System.Web.UI.WebControls.GridView();
            //        //gView.DataSource = table;
            //        //gView.DataBind();
            //        //using (System.IO.StringWriter sw = new System.IO.StringWriter())
            //        //{
            //        //    using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
            //        //    {
            //        //        gView.RenderControl(htw);
            //        //        ViewBag.ReturnedData = sw.ToString();
            //        //    }
            //        //}
            //    }
            //}
        }
        
        //private static List<UserTbl> GetWebApiddl()
        //    {
        //        List<UserTbl> users = new List<UserTbl>();
        //        string apiUrl = "https://betaapi.eon-xr.com//External/GetDemoLibraryCategories";
            
        //        HttpClient client = new HttpClient();
        //        HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/GetUsers")).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            users = JsonConvert.DeserializeObject<List<UserTbl>>(response.Content.ReadAsStringAsync().Result);
        //        }

        //        return users;
        //    }


        // User Details
        public async Task<IActionResult> Details(int? Id)
            {
                if (Id == null)
                {
                    return NotFound();
                }
                var user = await _context.UserTbls.FirstOrDefaultAsync(m => m.Id == Id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }

            //Add Get Method
            public async Task<IActionResult> Create()
            {
                return View();
            }

            //Add Post Method
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(string setDays, [Bind("Id,Name,Email,Gender,DateReg,SelectedDays,AreaOfInterest,AddRequest")] UserTbl userData)
            {
                if (ModelState.IsValid)
                {

                    UserTbl user = new UserTbl();

                    try
                    {
                        DateTime dt = Convert.ToDateTime(userData.DateReg);
                        string getRegDate = dt.ToString("dd/MM/yyyy");
                        string getDays = setDays;

                        //var getuser = _context.UserTbls.OrderByDescending(u => u.Id).FirstOrDefault();

                        //if (getuser == null)
                        //{
                        //    user.Id = 1;
                        //}                     
                        //else
                        //{
                        //    user.Id = getuser.Id + 1;
                        //}

                        user.Name = userData.Name;
                        user.Email = userData.Email;
                        if (userData.Gender == "Male")
                        {
                            user.Gender = "M";
                        }
                        else
                        {
                            user.Gender = "F";
                        }
                        user.DateReg = getRegDate;
                        user.AreaOfInterest = "";
                        user.SelectedDays = getDays;
                        user.AddRequest = userData.AddRequest;

                        _context.Add(user);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(userData);
            }


            //Edi Get Method
            public async Task<IActionResult> Edit(int? Id)
            {
                var user = await _context.UserTbls.FindAsync(Id);

                return View(user);
            }

            //Edit Post Method
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int userId, [Bind("Id,Name,Email,Gender,DateReg,SelectedDays,AreaOfInterest,AddRequest")] UserTbl userData)
            {
                UserTbl user = await _context.UserTbls.FindAsync(userId);

                if (ModelState.IsValid)
                {
                    try
                    {
                        user.Name = userData.Name;
                        user.Email = userData.Email;
                        user.Gender = userData.Gender;
                        user.DateReg = userData.DateReg;
                        user.AreaOfInterest = userData.AreaOfInterest;
                        user.SelectedDays = userData.SelectedDays;
                        user.AddRequest = userData.AddRequest;

                        _context.Update(user);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(userData);
            }


            // GET: User/Delete/1
            public async Task<IActionResult> Delete(int? Id)
            {
                if (Id == null)
                {
                    return NotFound();
                }
                var user = await _context.UserTbls.FirstOrDefaultAsync(m => m.Id == Id);

                return View(user);
            }

            // POST: User/Delete/1
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int Id)
            {
                var user = await _context.UserTbls.FindAsync(Id);
                _context.UserTbls.Remove(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
}
