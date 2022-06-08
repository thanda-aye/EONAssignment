using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EONAssignmentProj.Models;

namespace EONAssignmentProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListApiController : ControllerBase
    {
        private EONAssignmentDBContext _context { get; }

        public UserListApiController(EONAssignmentDBContext context)
        {
            _context = context;
        }

        [Route("GetUsers")]
        [HttpGet]      
        public ActionResult GetUsers()       
        {
            var users = _context.UserTbls.ToList();
             
            return Ok(users);
        }


    }
}
