using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HRMath.Models;
using System.Threading.Tasks;

namespace HRMath.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View();

    }


}