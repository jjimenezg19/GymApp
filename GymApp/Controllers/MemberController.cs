﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers;

public class MeController : Controller
{
   
    public IActionResult MemberPage()
    {
        return View();
    }
}

