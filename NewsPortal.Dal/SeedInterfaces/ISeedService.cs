using System;
using System.Collections.Generic;
using System.Text;
using NewsPortal.Dal.Entities;

namespace NewsPortal.Dal.SeedInterfaces
{
    public interface ISeedService
    {
        IDictionary<string, Author> Authors { get; }
        IDictionary<string, Category> Categories { get; }
        IList<News> News { get; }
    }
}
