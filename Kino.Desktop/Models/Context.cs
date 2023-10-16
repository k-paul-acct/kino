using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kino.ApiClient;

namespace Kino.Desktop.Models
{
    public static class Context
    {
        public static ApiClient.ApiClient apiClient = new ApiClient.ApiClient("http://localhost:5012/api/");
        public static ApiClient.Dto.UserDto? СurrentUser = null;

    }
}
