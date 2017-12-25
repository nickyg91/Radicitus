using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.SqlProviders;
using Radicitus.Web.Models;

namespace Radicitus.Web.ViewComponents
{
    public class RenderAllGridsViewComponent : ViewComponent
    {
        private readonly IRadSqlProvider _radSql;

        public RenderAllGridsViewComponent(IRadSqlProvider radSql)
        {
            _radSql = radSql;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new RaidEffortsModel
            {
                Grids = await _radSql.GetAllGridsAsync()
            };
            return View(model);
        }
    }
}
