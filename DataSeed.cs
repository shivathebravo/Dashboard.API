using Dashboard.API.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.API
{
  public class DataSeed
  {
    private readonly APIContext _ctx;

    public DataSeed(APIContext ctx)
    {
      _ctx = ctx;
    }

    public void SeedData(int nCustomers, int nOrders)
    {

      if (!_ctx.Customers.Any())
      {
        SeedCustomers(nCustomers);
        _ctx.SaveChanges();
      }
      if (!_ctx.orders.Any())
      {
        SeedOrders(nOrders);
        _ctx.SaveChanges();

      }
      if (!_ctx.Servers.Any())
      {
        SeedServers();
        _ctx.SaveChanges();

      }


    }

    private void SeedCustomers(int n)
    {
      var customers = BuildCustomerList(n);
      foreach (var customer in customers)
      {
        _ctx.Customers.Add(customer);
      }
    }


    private void SeedOrders(int n)
    {
      var orders = BuildOrderList(n);
      foreach (var order in orders)
      {
        _ctx.orders.Add(order);
      }
    }


    private void SeedServers()
    {
      var servers = BuildServerList();
      foreach (var server in servers)
      {
        _ctx.Servers.Add(server);
      }
    }

    private List<Server> BuildServerList()
    {

      return new List<Server>()
      {
        new Server{Id = 1,Name = "Dev-web",IsOnline = true},
        new Server{Id = 2,Name = "Dev-Mail",IsOnline = true},
        new Server{Id = 3,Name = "Dev-services",IsOnline = false},
        new Server{Id = 4,Name = "Prod-web",IsOnline = true},
        new Server{Id = 5,Name = "Prod-Mail",IsOnline = true},
        new Server{Id =6,Name = "Prod-services",IsOnline = true},
      };

    }

    private List<Order> BuildOrderList(int nOrders)
    {
      var orders = new List<Order>();
      var rand = new Random();
      for (int i = 1; i < nOrders; i++)
      {
        var randCustomerId = rand.Next(1, _ctx.Customers.Count());
        var placed = Helpers.GetRandomOrderPlaced();
        var completed = Helpers.GetRandomOrderCompleted(placed);
        orders.Add(new Order
        {
          Id = i,
          Customer = _ctx.Customers.First(c => c.Id == randCustomerId),
          Total = Helpers.GetRandoOrderTotal(),
          Placed = placed,
          Completed = completed
        });
      }

      return orders;
    }

    private List<Customer> BuildCustomerList(int nCustomers)
    {
      var customers = new List<Customer>();
      var names = new List<string>();
      for (var i = 1; i <= nCustomers; i++)
      {
        var name = Helpers.MakeUniqueCustomerName(names);
        names.Add(name);

        customers.Add(new Customer
        {
          Id = i,
          Name = name,
          Email = Helpers.MakeCustomerEmail(name),
          State = Helpers.GetRandomState()

        });
      }
      return customers;
    }


  }
}
