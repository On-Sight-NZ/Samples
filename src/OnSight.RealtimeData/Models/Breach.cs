using System.Collections.Generic;
using System;

namespace OnSight.RealtimeData.Models
{

  public class Breach
  {
    public Guid AlertId { get; set; }
    public DateTimeOffset Occurred { get; set; }
    public DateTimeOffset? Ended { get; set; }
    public Guid CorrelationId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public EnumSensorType Type { get; set; }
    public IEnumerable<BreachCondition> AlertConditions { get; set; }
    public Guid DeviceId { get; set; }
    public Guid GatewayId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid LogicGroupId { get; set; }
  }

  public class BreachCondition
  {
    public EnumValueType Type { get; set; }
    public EnumAlertOperation Comparison { get; set; }
    public double BreachValue { get; set; }
    public double ActualValue { get; set; }
  }

  public enum EnumAlertOperation
  {
    Above = 0,
    Below = 1,
    Equals = 2,
  }
}
