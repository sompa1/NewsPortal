using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.ViewRender
{
    public interface IViewRender
    {
        string Render<TModel>(string name, TModel model);
    }
}
