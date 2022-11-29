using Lite_Ceep_Store.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Lite_Ceep_Store.Assets
{
    public static class Current_Global
    {
        public static List<User> CurrentUser { get; set; } = new List<User>();
    }
}
