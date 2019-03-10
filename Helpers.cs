using System;
using System.Collections.Generic;

namespace Dashboard.API
{
  public class Helpers
  {
    private static Random _rand = new Random();
    private static string GetRandom(IList<string> items)
    {

      return items[_rand.Next(items.Count)];
    }
    public static string MakeUniqueCustomerName(List<string> names)
    {

      var maxNames = bizPrefix.Count * bizSuffix.Count;
      if (names.Count >= maxNames)
      {
        throw new System.InvalidOperationException("maximum number of unique names exceeded");
      }
      var prefix = GetRandom(bizPrefix);
      var suffix = GetRandom(bizSuffix);
      var bizName = prefix + suffix;

      if (names.Contains(bizName))
      {
        MakeUniqueCustomerName(names);
      }
      return prefix + suffix;
    }

    internal static string MakeCustomerEmail(string customerName)
    {
      return $"contatc@{customerName.ToLower()}.com";
    }
    internal static string GetRandomState()
    {
      return GetRandom(usStates);
    }
    private static readonly List<string> usStates = new List<string>
    {
      "AZ","AL","MD","VA","DC","WY"
    };
    private static readonly List<string> bizPrefix = new List<string>()
    {
      "ABC",
      "XYZ",
      "Shiva",
      "John",
      "Magic",
      "Johnson"
    };
    private static readonly List<string> bizSuffix = new List<string>()
    {
      "Corporation",
      "Test",
      "zxy",
      "johnson",
      "test",
      "Nestle"
    };

    internal static decimal GetRandoOrderTotal()
    {
      return _rand.Next(100, 5000);
    }
    internal static DateTime GetRandomOrderPlaced()
    {
      var end = DateTime.Now;
      var start = end.AddDays(-90);

      TimeSpan possibleSpan = end - start;

      TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);
      return start + newSpan;
    }
    internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
    {
      var now = DateTime.Now;
      var minLeadTime = TimeSpan.FromDays(7);
      var timePassed = now - orderPlaced;

      if (timePassed < minLeadTime)
      {
        return null;
      }

      return orderPlaced.AddDays(_rand.Next(7, 14));

    }
  }
}
