using System;
using System.Collections.Generic;
using System.Text;
using NewsPortal.Model;

namespace NewsPortal.Dal.SeedInterfaces
{
    public interface ISeedService
    {
        IDictionary<string, Category> Categories { get; }
        IList<News> News { get; }
    }
}
