using Microsoft.AspNetCore.Mvc;

namespace BeerBreweryBar.Common
{
    public static class Helper
    {
        public static ContentResult DuplicateRecord()
        {
            return new ContentResult
            {
                Content = "Duplicate record found!",
                ContentType = "text/plain",
                StatusCode = 400
            };
        }
        public static ContentResult RecordNotFound()
        {
            return new ContentResult
            {
                Content = "Record not found!",
                ContentType = "text/plain",
                StatusCode = 400    
            };
        }
    }
}
