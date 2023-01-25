using Core.DbContext;
using Core.Models;
using Logic.IHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class DropdownHelper : IDropdownHelper
    {
        private readonly AppDBContext _context;
        public DropdownHelper(AppDBContext context)
        {
            _context = context;
           
        }
        public List<Dropdown> DropdownOfCategories()
        {
            try
            {
                var common = new Dropdown()
                {
                    Id = 0,
                    Name = "Select Category"
                };

                var listOfCategories = _context.Categories.Where(x => x.Id > 0 && x.Active && !x.Deleted).ToList();
                var drp = listOfCategories.Select(x => new Dropdown
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();


                drp.Insert(0, common);
                return drp;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
