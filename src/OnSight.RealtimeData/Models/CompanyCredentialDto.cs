using System;
using System.Threading;

namespace OnSight.RealtimeData.Models
{
  public class CompanyCredentialDto
  {
    public string Url { get; internal set; }
    public string Token { get; internal set; }
    public Guid OnSightId { get; internal set; }
  }
}
