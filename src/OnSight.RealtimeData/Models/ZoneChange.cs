using System;

namespace OnSight.RealtimeData.Models
{
  public class CoreDeviceZoneChange
  {
    public Guid DeviceId { get; set; }
    public Guid ZoneId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset? End { get; set; }
  }
}
