using System.Collections.Generic;
using System.Linq;

namespace Dashboard.API.Controllers
{
    internal class PaginateResponse<T>
    {
        private IOrderedQueryable<Order> data;
        private PaginateResponse<Order> page;
        private int pageIndex;
        private object pageSizeRequested;

        public PaginateResponse(IEnumerable<T> data,  int i, int len)
        {
            Data = data.Skip((i - 1) * len).Take(len).ToList();
            Total = data.Count();
        }

        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}