using Knjiznica.Presentation.Common;
using Knjiznica.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Knjiznica.Presentation.Controllers
{
    public class HTMLHelperController : Controller
    {
        private const int PAGE_SIZE = 10;
        public IActionResult Number()
        {
            return View();
        }

        public IActionResult ShowPaging(ShowPaging model,
                             int page = 1, int inputNumber = 1)
        {
            if (ModelState.IsValid)
            {
                var displayResult = new List<string>();
                string message;

                //set model.pageinfo
                model.PageInfo = new PageInfo();
                model.PageInfo.CurrentPage = page;
                model.PageInfo.ItemsPerPage = PAGE_SIZE;
                model.PageInfo.TotalItems = inputNumber;

                //Set model.displayresult - numbers list
                for (int count = model.PageInfo.PageStart;
                         count <= model.PageInfo.PageEnd; count++)
                {
                    message = count.ToString();
                    displayResult.Add(message.Trim());
                }
                model.DisplayResult = displayResult;
            }
            //return view model
            return View(model);
        }
    }
}
