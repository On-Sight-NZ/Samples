namespace OnSight.RealtimeData.Models
{
  public enum EnumSensorType
  {
    Acc = 0,
    Temperature = 1,
    Humidity = 2,
    Gps = 3,
    Rssi = 4,
    Battery = 5,
    Zone = 6,
    BeaconProximity = 7,

    Co2 = 20,
    Lux = 22,
    InfraredPct = 24,
    Pressure = 26,
    Tvoc = 28,

    Button1 = 101,
    Button2 = 102,
    Button3 = 103,
    Button4 = 104,
    Button5 = 105,
    Button6 = 106,
    Button7 = 107,
    Button8 = 108,
    Button9 = 109,
    Button10 = 110,
    Button11 = 111,
    Button12 = 112,
    Button13 = 113,
    Button14 = 114,
    Button15 = 115,
  }

  public enum EnumValueType
  {
    AccX = 0,
    AccY = 1,
    AccZ = 2,
    Temperature = 3,
    Humidity = 4,
    Battery = 5,
    Rssi = 6,
    Zone = 7,
    TransmissionPower = 8,
    Distance = 9,
    Latitude = 10,
    Longitude = 11,
    GpsAccuracy = 12,

    Co2 = 20,
    Lux = 22,
    InfraredPct = 24,
    Pressure = 26,
    Tvoc = 28,

    ButtonLongPress = 100,
    ButtonPress = 101,
    ButtonDoublePress = 102,
    ButtonTriplePress = 103,
  }

}
