using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly APIContext _ctx;
        public OrderController(APIContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex,int pageSize)
        {
            var data = _ctx.orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);

            var page = new PaginateResponse<Order>(data,  pageIndex, pageSize);
            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _ctx.orders.Include(o => o.Customer).ToList();
            var groupedResult = orders.GroupBy(o => o.Customer.State).ToList().Select(grp => new
            {
                State = grp.Key,
                Total = grp.Sum(x => x.Total)
            }).OrderByDescending(res => res.Total)
            .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("GetOrder/{id}", Name ="GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _ctx.orders.Include(o => o.Customer).First(o => o.Id == id);
            return Ok(order);
               
        }
    }
}