using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnSight.RealtimeData.Models
{
  public class DeviceChangeBatch
  {
    public List<DeviceSensorChangeDto> v { get; set; }
  }
  public class DeviceSensorChangeDto
  {
    [JsonPropertyName("d")]
    public Guid DeviceId { get; set; }

    [JsonPropertyName("t")]
    public DateTimeOffset Received { get; set; }

    [JsonPropertyName("gw")]
    public Guid Gateway { get; set; }

    [JsonPropertyName("z")]
    public Guid ZoneId { get; set; }

    [JsonPropertyName("sd")]
    public List<DeviceChangeValueDto> SensorDeviceValues { get; set; }
  }

  public class DeviceChangeValueDto
  {
    [JsonPropertyName("sdt")]
    public EnumSensorType SensorDataType { get; set; }

    [JsonPropertyName("vt")]
    public EnumValueType ValueType { get; set; }

    [JsonPropertyName("v")]
    public double Value { get; set; }

    [JsonPropertyName("cl")]
    public decimal? ValueCalibrationLow { get; set; }

    [JsonPropertyName("cr")]
    public decimal? ValueCalibrationRange { get; set; }

    [JsonPropertyName("rl")]
    public decimal? ValueReferenceLow { get; set; }

    [JsonPropertyName("rr")]
    public decimal? ValueReferenceRange { get; set; }
  }
}
