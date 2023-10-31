using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino.Mobile.Models
{
    public static class Context
    {
        public static ApiClient.ApiClient apiClient = new ApiClient.ApiClient("https://36f4-95-26-78-230.ngrok-free.app/api/");
        public static ApiClient.Dto.UserDto? СurrentUser = null;
        public static int CurrentMovieId { get; set; }
    }
}
